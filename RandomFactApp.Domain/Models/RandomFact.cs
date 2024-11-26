/*
    Remember: Edit => Paste Special => Paste JSON as Classes
    This class has been changed later to adhere to the default C# code styling and to remove properties that have no use in our 'domain'
 */

namespace RandomFactApp.Domain.Models
{
    
    /// <summary>
    /// The random fact only contains properties we're interested in. 
    /// </summary>
    public class RandomFact
    {

        public string Text { get; set; }

        public GpsLocation Location { get; set; }
    }
}
