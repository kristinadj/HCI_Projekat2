using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EndangeredSpeciesMap.Model
{
    public enum StatusOfEndangerment
    {
        CriticallyEndangered, Endangered, Vulnerable, DependentOnTheHabitat, CloseToRisk, SmallestRisk
    }

    public enum TouristStatus
    {
        Isolated, PartiallyHabituate, Habituated
    }
    public class Specie
    {
        public string ID { get; set; } // unique
        public string Name { get; set; } 
        public string Description { get; set; }
        public string SpecieType { get; set; }
        public StatusOfEndangerment Endangerment { get; set; }
        public string Icon { get; set; }
        public bool DangerousForPeople { get; set; }
        public bool OnIUCNList { get; set; }
        public bool InRegionWithPeople { get; set; }
        public TouristStatus TouristStatus { get; set; }
        public float TouristIncome { get; set; }
        public DateTime DiscoveryDate { get; set; }

        public List<String> Tags = new List<String>(); // String - Tag unique label
    }
}
