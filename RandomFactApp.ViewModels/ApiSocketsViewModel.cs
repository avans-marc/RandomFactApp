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
    public partial class ApiSocketsViewModel : ObservableObject
    {
        private readonly IRandomFactClient randomFactClient;
        private readonly IWebSocketClient websocketClient;


        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SendFactToSocketCommand))]
        public string randomFact;

 

        [ObservableProperty]
        public string notification;

        

        public bool isFetchingRandomFact = false;

        public ApiSocketsViewModel(IRandomFactClient randomFactClient, IWebSocketClient websocketClient)
        {
            this.randomFactClient = randomFactClient;
            this.websocketClient = websocketClient;
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