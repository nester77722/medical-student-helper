using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStudentHelper.LocalData.Entities;
public class LocalUser
{
    public string Id {  get; set; }
    public string GoogleId {  get; set; }
    public DateTime LastLoginTime { get; set; }
}
