using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Site.Models.Entities
{
    public class FilterViewModel
    {
        public string Search { get; set; }
        public VechicleState? VechicleState { get; set; }
        public VechicleType? VechicleType { get; set; }
    }
}
