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
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "2 < Name < 50")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "2 < Surname < 50")]
        public string Surname { get; set; }
        [Required]
        [Range(1, 120, ErrorMessage = "1 < Age < 120")]
        public int Age { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "2 < Login < 100")]
        public string Login { get; set; }
        public string Hash { get; set; }
        public bool isAdmin { get; set; }
        [StringLength(150, MinimumLength = 2, ErrorMessage = "2 < Country < 50")]
        public string Country { get; set; }
        [StringLength(150, MinimumLength = 2, ErrorMessage = "2 < City < 50")]
        public string City { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        [EmailAddress]
        public string Email { get; set; }

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
