using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace EndangeredSpeciesMap.Model
{
    public enum StatusOfEndangerment
    {
        CriticallyEndangered = 1, Endangered = 2, Vulnerable = 3, DependentOnTheHabitat = 4, CloseToRisk = 5, SmallestRisk = 6
    }

    public enum TouristStatus
    {
        Isolated = 1, PartiallyHabituate = 2, Habituated = 3
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
        public double X { get; set; }
        public double Y { get; set; }
    }
}
