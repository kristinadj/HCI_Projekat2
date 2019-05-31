using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndangeredSpeciesMap.Model
{
    public class SpecieType
    {
        public string Label { get; set; } // unique

        public string Name { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }
    }
}
