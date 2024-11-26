
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Maps;
using RandomFactApp.Domain.Clients;
using RandomFactApp.Domain.Models;
using System.Collections.ObjectModel;

namespace RandomFactApp.ViewModels
{
    /// <summary>
    /// The ViewModel is completely separate from the view
    /// </summary>
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IRandomFactClient randomFactClient;


        [ObservableProperty]
        private ObservableCollection<MappedRandomFactViewModel> mappedRandomFacts;

        [ObservableProperty]
        public string randomFact;

        [ObservableProperty]
        public Microsoft.Maui.Maps.MapSpan currentMapSpan;

        public bool isFetchingRandomFact = false;


        public MainPageViewModel(IRandomFactClient randomFactClient)
        {
            this.randomFactClient = randomFactClient;
            this.mappedRandomFacts = new ObservableCollection<MappedRandomFactViewModel>();
            
        }

        [RelayCommand(CanExecute = nameof(CanFetchRandomFact))]
        public async Task FetchRandomFact()
        {
            if (isFetchingRandomFact)
                return;

            try
            {
                isFetchingRandomFact = true;
                var fact = await this.randomFactClient.GetRandomFactAsync();
                this.RandomFact = fact!.Text;

                if(fact.Location != null)
                {
                    System.Diagnostics.Debug.WriteLine($"Got a fact from {fact.Location.City}");

                    var location = new Location(fact.Location.Latitude, fact.Location.Longitude);

                    this.CurrentMapSpan = MapSpan.FromCenterAndRadius(location, Distance.FromKilometers(100));
                    this.MappedRandomFacts.Add(new MappedRandomFactViewModel(fact!.Text, location ));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                this.RandomFact = "Developers generally don't expose why something failed since it might contain sensitive information.";
            }
            finally
            { 
                isFetchingRandomFact = false; 
            }
        }

        
        /// <summary>
        /// This line makes sure the button is disabled (greyed out) when the ViewModel is currently 
        /// processing a request. This prevents the user from tapping the button repeatedly and firing requests to the backend
        /// </summary>
        public bool CanFetchRandomFact() => !isFetchingRandomFact;



        private class MappedRandomFact
        {
            
            Location Location;


        }
    }
}
