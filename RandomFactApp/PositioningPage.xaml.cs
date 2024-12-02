using RandomFactApp.ViewModels;

namespace RandomFactApp;

public partial class PositioningPage : ContentPage
{
    // We inject the viewModel becasue this class depends on it.
    public PositioningPage(PositioningViewModel positioningViewModel)
    {
        InitializeComponent();
        this.BindingContext = positioningViewModel;
    }

}