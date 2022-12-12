using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace MusicBackend.Security
{
    public class PasswordAuthentication : IPasswordAuthentication
    {
        private readonly string devider = ".";

        public string hashingPass(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password!,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8)); ;
        }

        public string generateNewPassword(string password)
        {
            //Generate a random number that will be used for salt IE. it will be put before the hashing pass part, so its hard to decrypt.
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            return Convert.ToBase64String(salt) + devider + hashingPass(password, salt);
        }

        public bool verifyPassword(string hashedPassword, string passForVerification)
        {
            string[] saltAndHashPass = hashedPassword.Split(devider);
            byte[] saltFromPassword = Convert.FromBase64String(saltAndHashPass[0]);
            string passwordFromDb = saltAndHashPass[1];
            string hashToVerify = hashingPass(passForVerification, saltFromPassword);

            //Compared the 2 encoded password and see if they are the same - they are using the same salt, so the comparison is possible.
            return passwordFromDb == hashToVerify;
        }
    }
}
