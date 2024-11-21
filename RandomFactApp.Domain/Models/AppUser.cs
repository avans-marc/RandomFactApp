using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomFactApp.Domain.Models
{
    public class AppUser
    {
        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Country Country { get; set; }


        // TODO: Toddler logic here
        public bool IsToddler(DateTime now)
        {
            var age = GetAge(now);

            return age < 5;

        }

        public bool IsToddler() => IsToddler(DateTime.Now); 

        public bool IsAllowedExplicitRandomFacts(DateTime now)
        {
            if(Country == null || this.DateOfBirth == null) 
                return false;

            int age = this.GetAge(now);


            return age >= this.Country.MinimumAgeForExplicitContent;
        }

        public bool IsAllowedExplicitRandomFacts() => IsAllowedExplicitRandomFacts(DateTime.Now);

        private int GetAge(DateTime onDate)
        {
            if(DateOfBirth == null)
                throw new ArgumentNullException(nameof(DateOfBirth));

            // Save today's date.
            var today = onDate.Date;

            // Calculate the age.
            var age = today.Year - this.DateOfBirth.Year;

            // Go back to the year in which the person was born in case of a leap year
            if (this.DateOfBirth.Date > today.AddYears(-age)) age--;

            return age;
        }
    }
}
