using MedicalStudentHelper.WPF.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace MedicalStudentHelper.WPF.Services.Authentication;
public class GoogleAuthenticator
{
    private readonly GoogleAuthenticationConfiguration _configuration;
    private const string CodeChallengeMethod = "S256";

    public GoogleAuthenticator(IOptions<GoogleAuthenticationConfiguration> configuration)
    {
        _configuration = configuration.Value;
    }

    private static int GetRandomUnusedPort()
    {
        var listener = new TcpListener(IPAddress.Loopback, 0);
        listener.Start();
        var port = ((IPEndPoint)listener.LocalEndpoint).Port;
        listener.Stop();
        return port;
    }

    public async Task<GoogleAuthenticationResult> StartAuthentication(CancellationToken cancellationToken = default)
    {
        // Generates state and PKCE values.
        string state = RandomDataBase64Url(32);
        string codeVerifier = RandomDataBase64Url(32);
        string codeChallenge = Base64UrlEncodeNoPadding(Sha256(codeVerifier));

        // Creates a redirect URI using an available port on the loopback address.
        string redirectURI = string.Format("http://{0}:{1}/", IPAddress.Loopback, GetRandomUnusedPort());

        // Creates an HttpListener to listen for requests on that redirect URI.
        var http = new HttpListener();
        http.Prefixes.Add(redirectURI);
        http.Start();

        // Creates the OAuth 2.0 authorization request.
        string authorizationRequest = string.Format("{0}?response_type=code&scope=openid%20profile&redirect_uri={1}&client_id={2}&state={3}&code_challenge={4}&code_challenge_method={5}",
            _configuration.AuthorizationEndpoint,
            Uri.EscapeDataString(redirectURI),
            _configuration.ClientID,
            state,
            codeChallenge,
            CodeChallengeMethod);

        // Opens request in the browser.
        Process.Start(new ProcessStartInfo
        {
            FileName = authorizationRequest,
            UseShellExecute = true
        });

        // Waits for the OAuth authorization response.
        var context = await http.GetContextAsync();

        // Sends an HTTP response to the browser.
        var response = context.Response;
        string responseString = string.Format("<html><head><meta http-equiv='refresh' content='10;url=https://google.com'></head><body>Please return to the app.</body></html>");
        var buffer = Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        var responseOutput = response.OutputStream;
        Task responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length, cancellationToken).ContinueWith((task) =>
        {
            responseOutput.Close();
            http.Stop();
        });

        // Checks for errors.
        if (context.Request.QueryString.Get("error") != null)
        {
            throw new Exception("OAuth authorization error: " + context.Request.QueryString.Get("error"));
        }
        if (context.Request.QueryString.Get("code") == null
            || context.Request.QueryString.Get("state") == null)
        {
            throw new Exception("Missing OAuth authorization code or state.");
        }

        // Extracts the code
        var code = context.Request.QueryString.Get("code");
        var incomingState = context.Request.QueryString.Get("state");

        // Compares the received state to the expected value, to ensure that
        // this app made the request which resulted in authorization.
        if (incomingState != state)
        {
            throw new Exception("Invalid state parameter received.");
        }

        // Starts the code exchange at the Token Endpoint.
        return await PerformCodeExchange(code, codeVerifier, redirectURI, cancellationToken);
    }

    private async Task<GoogleAuthenticationResult> PerformCodeExchange(string code, string codeVerifier, string redirectURI, CancellationToken cancellationToken)
    {
        // Builds the request URI and body
        string tokenRequestURI = _configuration.TokenEndpoint;
        var tokenRequestBody = new Dictionary<string, string>
        {
            { "code", code },
            { "redirect_uri", redirectURI },
            { "client_id", _configuration.ClientID },
            { "code_verifier", codeVerifier },
            { "client_secret", _configuration.ClientSecret },
            { "scope", "" },
            { "grant_type", "authorization_code" }
        };

        // Creates an instance of HttpClient
        using (var httpClient = new HttpClient())
        {
            try
            {
                // Sends the POST request
                var content = new FormUrlEncodedContent(tokenRequestBody);
                HttpResponseMessage response = await httpClient.PostAsync(tokenRequestURI, content, cancellationToken);

                // Throws an exception if the response status code indicates failure
                response.EnsureSuccessStatusCode();

                // Reads the response content
                string responseText = await response.Content.ReadAsStringAsync(cancellationToken);

                // Converts the response to a dictionary
                Dictionary<string, string> tokenEndpointDecoded = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseText);

                // Extracts the access token and makes the UserInfo call
                string accessToken = tokenEndpointDecoded["access_token"];
                return await UserInfoCall(accessToken, cancellationToken);
            }
            catch (HttpRequestException ex)
            {
                // Handles protocol errors and other HTTP-related exceptions
                throw new Exception("Error during token exchange", ex);
            }
        }
    }

    private async Task<GoogleAuthenticationResult> UserInfoCall(string accessToken, CancellationToken cancellationToken)
    {
        // Builds the request URI
        string userInfoRequestURI = _configuration.UserInfoEndpoint;

        // Creates an instance of HttpClient
        using (var httpClient = new HttpClient())
        {
            // Adds the authorization header
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // Sends the GET request
            HttpResponseMessage response = await httpClient.GetAsync(userInfoRequestURI, cancellationToken);

            // Throws an exception if the response status code indicates failure
            response.EnsureSuccessStatusCode();

            // Reads the response content
            string userInfoResponseText = await response.Content.ReadAsStringAsync(cancellationToken);

            // Deserializes the response into the GoogleAuthenticationResult object
            var userInfo = JsonConvert.DeserializeObject<GoogleAuthenticationResult>(userInfoResponseText);

            return userInfo;
        }
    }


    private static string RandomDataBase64Url(uint length)
    {
        byte[] bytes = new byte[length];
        RandomNumberGenerator.Fill(bytes);
        return Base64UrlEncodeNoPadding(bytes);
    }

    private static byte[] Sha256(string inputString)
    {
        byte[] bytes = Encoding.ASCII.GetBytes(inputString);

        // Using 'SHA256.Create()' to create an instance of SHA256
        using (SHA256 sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(bytes);
        }
    }

    private static string Base64UrlEncodeNoPadding(byte[] buffer)
    {
        string base64 = Convert.ToBase64String(buffer);

        // Converts base64 to base64url.
        base64 = base64.Replace("+", "-");
        base64 = base64.Replace("/", "_");
        // Strips padding.
        base64 = base64.Replace("=", "");

        return base64;
    }
}
