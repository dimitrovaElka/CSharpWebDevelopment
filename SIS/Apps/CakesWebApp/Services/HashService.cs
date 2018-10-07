﻿
namespace CakesWebApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using System.Text;

    public class HashService : IHashService
    {
        public string StrongHash(string stringToHash)
        {
            var result = stringToHash;
            for (int i = 0; i < 10; i++)
            {
                result = Hash(result);
            }

            return result;
        }

        public string Hash(string stringToHash)
        {
            stringToHash = stringToHash + "myAppSalt1394567#";
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(stringToHash));
                // Get the hashed string.  
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash;
            }
        }
    }

    public interface IHashService
    {
        string Hash(string stringToHash);
    }
}
