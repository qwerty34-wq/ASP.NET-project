using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Site.Models.Services
{
    public interface IHashDataManager
    {
        string GetHash(HashAlgorithm hashAlgorithm, string input);

        bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash);
    }
}
