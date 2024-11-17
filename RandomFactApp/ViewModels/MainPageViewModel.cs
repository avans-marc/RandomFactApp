
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RandomFactApp.Domain.Services;

namespace RandomFactApp.ViewModels
{
    /// <summary>
    /// The ViewModel is completely separate from the view
    /// </summary>
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IRandomFactClient randomFactClient;

        [ObservableProperty]
        public string randomFact;

        public bool isFetchingRandomFact = false;

        public MainPageViewModel(IRandomFactClient randomFactClient)
        {
            this.randomFactClient = randomFactClient;
        }

        [RelayCommand(CanExecute = nameof(CanFetchRandomFact))]
        public async Task FetchRandomFact(string name)
        {
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


    }
}
