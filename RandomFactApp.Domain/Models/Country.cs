using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomFactApp.Domain.Models
{
    public class Country
    {
        public static Country Netherlands = new Country { IsoCode = "NL", MinimumAgeForExplicitContent = 18 };
        public static Country UnitedStates = new Country { IsoCode = "NL", MinimumAgeForExplicitContent = 21 };

        public string IsoCode { get; set; }

        public int MinimumAgeForExplicitContent { get; set; }
    }

    
}
