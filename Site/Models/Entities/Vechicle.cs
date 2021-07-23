using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Models.Entities
{
    public class Vechicle
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "2 < Name < 50")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "2 < Model < 50")]
        public string Model { get; set; }
        [Required]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "2 < Country < 50")]
        public string Country { get; set; }
        [Required]
        public VechicleType VechicleType { get; set; }
        [Required]
        public DateTime Manufactured { get; set; }
        [Required]
        public VechicleState VechicleState { get; set; }
        [Required]  
        public long Mileage { get; set; }
        [Required]
        public float Price { get; set; }
        public Guid UserId { get; set; }
        public IList<FileModel> Files { get; set; }

        public Vechicle()
        {
            Files = new List<FileModel>();
        }
    }
}
