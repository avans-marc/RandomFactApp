using Moq;
using RandomFactApp.Domain.Clients;
using RandomFactApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomFactApp.ViewModels.Tests
{
    public static class TestHelper
    {
        public static RandomFact CreateRandomRandomFact()
        {
            return new RandomFact { Text = Guid.NewGuid().ToString(), Location = new City(0, 0, "City") };
        }


    }
}
