using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Maps;
using RandomFactApp.Domain.Clients;
using RandomFactApp.Domain.Models;
using System.Collections.ObjectModel;

namespace RandomFactApp.ViewModels
{
    public partial class PositioningViewModel : ObservableObject
    {
        private readonly IGeolocation geolocation;
        private readonly IRandomFactClient randomFactClient;

        [ObservableProperty]
        public string currentPosition;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(StartListeningCommand))]
        [NotifyCanExecuteChangedFor(nameof(StopListeningCommand))]
        private bool isListening = false;

        [ObservableProperty]
        public MapSpan currentMapSpan;

        public bool isFetchingRandomFact = false;

        [ObservableProperty]
        private ObservableCollection<RandomFactPinViewModel> mappedRandomFacts;

        [ObservableProperty]
        private RandomFactPinViewModel currentMappedRandomFact;

        public PositioningViewModel(IRandomFactClient randomFactClient, IGeolocation geolocation)
        {
            this.geolocation = geolocation;
            this.geolocation.LocationChanged += LocationChanged;

            this.randomFactClient = randomFactClient;
            this.mappedRandomFacts = new ObservableCollection<RandomFactPinViewModel>();
            
        }


        [RelayCommand(CanExecute = nameof(CanStartListening))]
        public async Task StartListening()
        {
            IsListening = true;
            await this.geolocation.StartListeningForegroundAsync(new GeolocationListeningRequest
            {
                MinimumTime = TimeSpan.FromSeconds(10),
                DesiredAccuracy = GeolocationAccuracy.Default
            });
        }


        [RelayCommand(CanExecute = nameof(CanStopListening))]
        public void StopListening()
        {
            IsListening = false;
            this.geolocation.StopListeningForeground();

        }

        public bool CanStartListening() => !IsListening;

        public bool CanStopListening() => IsListening;

        private void LocationChanged(object? sender, GeolocationLocationChangedEventArgs e)
        {
            if (e!.Location != null)
            {
                this.CurrentPosition = e.Location.ToString();
                this.CurrentMapSpan = MapSpan.FromCenterAndRadius(e.Location, Distance.FromMeters(500));
            }
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
                await ProcessNewRandomFactAsync(fact);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
            finally
            {
                isFetchingRandomFact = false;
            }
        }

        public bool CanFetchRandomFact() => !isFetchingRandomFact;

        private async Task ProcessNewRandomFactAsync(RandomFact? fact)
        {
            Location? fact_location = fact.Location != null ? new Location(fact.Location.Latitude, fact.Location.Longitude) : null;
            var fact_summary = $"{fact.Text}";

            if (fact_location != null)
            {
                // Try to calculate the distance between the user and the random fact
                try
                {
                    var user_location = await this.geolocation.GetLastKnownLocationAsync();
                    fact_summary += $" (from {fact!.Location.Name}, that's {Location.CalculateDistance(fact_location, user_location, DistanceUnits.Kilometers):N0} km from you)";
                }
                catch
                // Remember, there a a lot of possible errors here. Decide if you would like to point your user to
                // the desired solution (allow permissions, turn on gps, etc.)
                {

                }
                finally
                {
                    var mapped_random_fact = new RandomFactPinViewModel(fact_summary, fact_location);

                    // Finally update our view model
                    this.CurrentMapSpan = MapSpan.FromCenterAndRadius(fact_location, Distance.FromKilometers(100));
                    this.CurrentMappedRandomFact = mapped_random_fact;
                    this.MappedRandomFacts.Add(mapped_random_fact);
                }
            }
        }
    }
}
