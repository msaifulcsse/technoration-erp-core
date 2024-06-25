using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Helpers.Interfaces
{
    public interface ICryptoHelperService
    {
        string Encrypt(string encryptString);
        string Decrypt(string cipherText);
        string Generate();
        string Generate(int length);
        string Generate(int minLength, int maxLength);
    }
}
