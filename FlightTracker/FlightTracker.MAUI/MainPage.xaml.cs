using Microsoft.AspNetCore.SignalR.Client;

namespace FlightTracker.MAUI;

public partial class MainPage : ContentPage
{	
	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
    }
}


