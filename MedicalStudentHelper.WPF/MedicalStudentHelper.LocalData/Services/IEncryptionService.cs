using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalStudentHelper.LocalData.Services;
public interface IEncryptionService
{
    string Decrypt(string cipherText);
    string Encrypt(string plainText);
}
