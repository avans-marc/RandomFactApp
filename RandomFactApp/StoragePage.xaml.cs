using RandomFactApp.ViewModels;

namespace RandomFactApp;

public partial class StoragePage : ContentPage
{

    // We inject the viewModel becasue this class depends on it.
    public StoragePage(StorageViewModel storageViewModel)
    {
        InitializeComponent();
        this.BindingContext = storageViewModel;
    }

}