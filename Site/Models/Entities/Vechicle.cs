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
        public string Name { get; set; }
        public string Model { get; set; }
        public string Country { get; set; }
        public VechicleType VechicleType { get; set; }
        public DateTime Manufactured { get; set; }
        public VechicleState VechicleState { get; set; }
        public long Mileage { get; set; }
        public float Price { get; set; }
        public User User { get; set; }
    }
}
