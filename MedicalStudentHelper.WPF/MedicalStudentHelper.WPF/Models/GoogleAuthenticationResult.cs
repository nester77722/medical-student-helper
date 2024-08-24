using Newtonsoft.Json;

namespace MedicalStudentHelper.WPF.Models;
public class GoogleAuthenticationResult
{
    public string Sub { get; set; }
    public string Name { get; set; }
    [JsonProperty("given_name")]
    public string GivenName { get; set; }
    [JsonProperty("family_name")]
    public string FamilyName { get; set; }
    public string Picture { get; set; }
}
