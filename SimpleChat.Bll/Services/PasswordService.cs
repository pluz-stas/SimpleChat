using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SimpleChat.Bll.Interfaces;

namespace SimpleChat.Bll.Services
{
    public class PasswordService : IPasswordService
    {
        private const int SaltSize = 16;
        private const int KeySize = 32;
        private const int Iterations = 1000;
        
        public string Hash(string password)
        {
           using var algorithm = new Rfc2898DeriveBytes(
                password,
                SaltSize,
                Iterations,
                HashAlgorithmName.SHA512);
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return $"{Iterations}.{salt}.{key}";
        }

        public bool Verify(string passwordHash, string password)
        {
            var parts = passwordHash.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format. " + 
                                          "Should be formatted as `{iterations}.{salt}.{hash}`");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using var algorithm = new Rfc2898DeriveBytes(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA512);
            var keyToCheck = algorithm.GetBytes(KeySize);

            var verified = keyToCheck.SequenceEqual(key);

            return verified;
        }
    }
}