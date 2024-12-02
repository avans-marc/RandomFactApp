using RandomFactApp.ViewModels;

namespace RandomFactApp;

public partial class ApiSocketsPage : ContentPage
{
    private readonly IDispatcherTimer timer;
    private readonly ApiSocketsViewModel mainPageViewModel;

    // We inject the viewModel becasue this class depends on it.
    public ApiSocketsPage(ApiSocketsViewModel mainPageViewModel)
    {
        InitializeComponent();

        this.mainPageViewModel = mainPageViewModel;

        // Binding the viewModel to this page
        BindingContext = mainPageViewModel;

        timer = Application.Current.Dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.Tick += (s, e) => MainThread.InvokeOnMainThreadAsync(mainPageViewModel.CheckIfNotificationReceived);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        MainThread.InvokeOnMainThreadAsync(mainPageViewModel.StopListeningForNotifications);
        timer.Start();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        timer.Stop();
    }
}