namespace MedicalStudentHelper.WPF.Services.Authentication;
public class GoogleAuthenticationConfiguration
{
    public string ClientID { get; set; } = "575166688189-q5ivuqkju7sh62mlaf9bkhdn02kuktvh.apps.googleusercontent.com";
    public string ClientSecret { get; set; } = "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";
    public string AuthorizationEndpoint { get; set; } = "https://accounts.google.com/o/oauth2/v2/auth";
    public string TokenEndpoint { get; set; } = "https://www.googleapis.com/oauth2/v4/token";
    public string UserInfoEndpoint { get; set; } = "https://www.googleapis.com/oauth2/v3/userinfo";
}
