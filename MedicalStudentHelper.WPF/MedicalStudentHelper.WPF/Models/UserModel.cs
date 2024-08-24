using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStudentHelper.WPF.Models;
public class UserModel
{
    public string Id {  get; set; }
    public string GoogleId {  get; set; }
    public string Name { get; set; }
    public string GivenName { get; set; }
    public string FamilyName { get; set; }
    public string PictureUrl { get; set; }
}
