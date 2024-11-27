using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Maps;
using RandomFactApp.Domain.Clients;
using RandomFactApp.Domain.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace RandomFactApp.ViewModels
{
    /// <summary>
    /// The ViewModel is completely separate from the view
    /// </summary>
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IRandomFactClient randomFactClient;
        private readonly IWebSocketClient websocketClient;
        private readonly IGeolocation geolocation;

        [ObservableProperty]
        private ObservableCollection<MappedRandomFactViewModel> mappedRandomFacts;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SendFactToSocketCommand))]
        public string randomFact;

        [ObservableProperty]
        public MapSpan currentMapSpan;

        [ObservableProperty]
        public string notification;

        public bool isFetchingRandomFact = false;

        public MainPageViewModel(IRandomFactClient randomFactClient, IWebSocketClient websocketClient, IGeolocation geolocation)
        {
            this.randomFactClient = randomFactClient;
            this.websocketClient = websocketClient;
            this.geolocation = geolocation;

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

                await ProcessNewRandomFactAsync(fact);

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
                    fact_summary += $" (from {fact!.Location.City}, that's {Location.CalculateDistance(fact_location, user_location, DistanceUnits.Kilometers):N0} km from you)";
                }
                catch
                // Remember, there a a lot of possible errors here. Decide if you would like to point your user to
                // the desired solution (allow permissions, turn on gps, etc.)
                {

                }
                finally
                {
                    // Finally update our view model
                    this.CurrentMapSpan = MapSpan.FromCenterAndRadius(fact_location, Distance.FromKilometers(100));
                    this.MappedRandomFacts.Add(new MappedRandomFactViewModel(fact_summary, fact_location));
                }
            }


            this.RandomFact = fact_summary;
        }


        /// <summary>
        /// This line makes sure the button is disabled (greyed out) when the ViewModel is currently
        /// processing a request. This prevents the user from tapping the button repeatedly and firing requests to the backend
        /// </summary>
        public bool CanFetchRandomFact() => !isFetchingRandomFact;


        [RelayCommand(CanExecute = nameof(CanSendFactToSocket))]
        public async Task SendFactToSocket()
        {
            if(!string.IsNullOrEmpty(this.RandomFact))
                await this.websocketClient.SendDataAsync(RandomFact);
        }


        public bool CanSendFactToSocket() => !string.IsNullOrEmpty(this.RandomFact);

        public async Task StartListeningForNotifications()
        {
            await this.websocketClient.ConnectAsync();
        }

        public async Task StopListeningForNotifications()
        {
            await this.websocketClient.ConnectAsync();
        }


        public async Task CheckIfNotificationReceived()
        {
            var notification = await this.websocketClient.ReceiveDataAsync();

            if (notification != null)
                this.Notification = $"Notification: {notification}";
        }
    }
}