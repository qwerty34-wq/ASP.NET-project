using Site.Models.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Site.Models.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Login { get; set; }
        public string Hash { get; set; }
        public bool isAdmin { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }

        private readonly IHashDataManager _hashDataManager = new HashDataManager();

        public override string ToString()
        {
            return $"{this.Name} {this.Surname}";
        }

        public void SetGuid()
        {
            this.Id = Guid.NewGuid();
        }

        public void SetHash()
        {
            string hash = this.Hash;
            this.Hash = GetHashByString(hash);
        }

        private string GetHashByString(string password)
        {
            string hash = String.Empty;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                hash = _hashDataManager.GetHash(sha256Hash, password);
            }
            return hash;
        }


    }
}
