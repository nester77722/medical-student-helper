using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThirdParty.Json.LitJson;

namespace MedicalStudentHelper.UserData.Models.CreateModels;
public class CreateUserFromGoogleAccountModel
{
    public string GoogleId { get; set; }
    public string Name { get; set; }
    public string GivenName { get; set; }
    public string FamilyName { get; set; }
    public string Picture { get; set; }
}
