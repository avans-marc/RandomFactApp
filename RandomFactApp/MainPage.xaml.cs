using RandomFactApp.ViewModels;

namespace RandomFactApp;

public partial class MainPage : ContentPage
{
    // We inject the viewModel becasue this class depends on it.
    public MainPage(MainPageViewModel mainPageViewModel)
    {
        InitializeComponent();
        // Binding the viewModel to this page
        BindingContext = mainPageViewModel;

        
    }
}

