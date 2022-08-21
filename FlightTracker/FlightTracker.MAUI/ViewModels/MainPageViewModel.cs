using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.AspNetCore.SignalR.Client;

namespace FlightTracker.MAUI
{
	public partial class MainPageViewModel : BaseViewModel
	{
		HubConnection hubConnection;

		public MainPageViewModel()
		{
			Title = "Flights!";

			

			hubConnection = new HubConnectionBuilder()
				.WithUrl("https://maui-flight-tracker.azurewebsites.net/api")
				.ConfigureLogging((b) => { b.Services.AddLogging(); })
				.Build();

			hubConnection.On<string, string>("flightUpdate", HandleFlightUpdate);
		}

		[RelayCommand]
		async Task LoadFlights()
		{
			if (hubConnection.State == HubConnectionState.Connected)
				await hubConnection.StopAsync();

			await hubConnection.StartAsync();
		}

		void HandleFlightUpdate(string user, string message)
		{
			Console.WriteLine(message);
		}
	}
}

