namespace MedicalStudentHelper.LocalData.Services;
public interface IEncryptionService
{
    string Decrypt(string cipherText);
    string Encrypt(string plainText);
}
