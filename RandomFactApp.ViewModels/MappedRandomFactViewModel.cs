using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomFactApp.ViewModels
{
    public partial class MappedRandomFactViewModel : ObservableObject
    {
        [ObservableProperty]
        private string fact;

        [ObservableProperty]
        private Location location;

        public MappedRandomFactViewModel(string fact, Location location)
        {
            this.fact = fact;
            this.location = location;
        }
    }
}
