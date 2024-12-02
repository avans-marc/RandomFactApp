using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomFactApp.ViewModels
{
    public partial class RandomFactPinViewModel : ObservableObject
    {
        [ObservableProperty]
        private string label;

        [ObservableProperty]
        private Location location;

        public RandomFactPinViewModel(string fact, Location location)
        {
            this.label = fact;
            this.location = location;
        }
    }
}
