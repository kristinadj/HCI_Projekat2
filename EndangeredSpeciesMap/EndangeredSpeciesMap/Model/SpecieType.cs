using System.Windows.Media;

namespace EndangeredSpeciesMap.Model
{
    public class SpecieType
    {
        public string Label { get; set; } // unique

        public string Name { get; set; }

        public string Description { get; set; }

        public string Icon { get; set; }

        public bool iconUploaded { get; set; }
    }
}
