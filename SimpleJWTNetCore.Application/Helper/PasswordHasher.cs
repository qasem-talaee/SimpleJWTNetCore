using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleJWTNetCore.Application.Helper
{
    public static class PasswordHasher
    {
        private static byte[] salt = new byte[128 / 8];
        internal static string GetHashPss(this string pass)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: pass,
                 salt: salt,
                 prf: KeyDerivationPrf.HMACSHA256,
                 iterationCount: 100000,
                 numBytesRequested: 256 / 8
                ));
            return hashed;
        }
    }
}
