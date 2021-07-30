using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Models.Entities
{
    public class MainViewModel
    {
        public IList<Vechicle> Vechicles { get; set; }
        public FilterViewModel Filter { get; set; }
        public IList<User> Users { get; set; }
    }
}
