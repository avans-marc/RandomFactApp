using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomFactApp.Infrastructure.UselessFactsJsphPIApi
{

    public class UselessFactsJsphPlApiResponse
    {
        public string id { get; set; }
        public string text { get; set; }
        public string source { get; set; }
        public string source_url { get; set; }
        public string language { get; set; }
        public string permalink { get; set; }
    }
}
