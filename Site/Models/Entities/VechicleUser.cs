using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Models.Entities
{
    public class VechicleUser
    {
        public Vechicle Vechicle { get; set; }
        public User User { get; set; }
    }
}
