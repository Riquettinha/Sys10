using System;
using System.Security.Cryptography;
using System.Text;

namespace Sys10.Services.Services
{
    public class CryptService : ICryptService
    {
        private readonly HashAlgorithm _hashAlgorithm;

        public CryptService()
        {
            _hashAlgorithm = HashAlgorithm.Create("SHA-256"); 
        }

        public string Crypt(string password)
        {
            var encodedValue = Encoding.UTF8.GetBytes(password);
            var encryptedPassword = _hashAlgorithm.ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var character in encryptedPassword)
                sb.Append(character.ToString("X2"));

            return sb.ToString();
        }

        public bool IsValid(string typedPassword, string correctPassword)
        {
            var encryptedPassword = _hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(typedPassword));

            var sb = new StringBuilder();
            foreach (var caractere in encryptedPassword)
                sb.Append(caractere.ToString("X2"));

            return sb.ToString() == correctPassword;
        }
    }
}
