using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FlightTracker.Models;
using Mapsui.UI.Maui;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;

namespace FlightTracker.MAUI
{
	public partial class MainPageViewModel : BaseViewModel
	{
		HubConnection hubConnection;

		public Mapsui.UI.Maui.MapView MapView { get; set; }

		public ObservableCollection<FlightInfo> Flights { get; } = new();

		public MainPageViewModel()
		{
			Title = "Flights!";

			hubConnection = new HubConnectionBuilder()
				.WithUrl("https://maui-flight-tracker.azurewebsites.net/api")
				.WithAutomaticReconnect()
				.Build();

			hubConnection.On<object>("newMessage", (obj) =>
			{
				if (IsBusy)
					return;

				try
				{
					if (obj == null)
						return;

					IsBusy = true;

					var results = JsonConvert.DeserializeObject<FlightResults>(obj.ToString());

					if (Flights.Count != 0)
					{
						Flights.Clear();
						MapView.Drawables.Clear();
					}

					foreach (var flight in results.NearbyPlanes)
					{
						if (!string.IsNullOrEmpty(flight.Operator))
						{
							Flights.Add(flight);
							
                            var circle = new Circle
                            {
                                Center = new Position(Double.Parse(flight.Lat), Double.Parse(flight.Lon)),
                                StrokeColor = Colors.Red,
                                FillColor = Colors.Red,
                                StrokeWidth = 10
                            };

							MapView.Drawables.Add(circle);
                        }
					}
					
				}
				finally
				{
					IsBusy = false;
				}
			});
		}

		[RelayCommand]
		async Task LoadFlights()
		{
			if (hubConnection.State == HubConnectionState.Connected)
				await hubConnection.StopAsync();

			await hubConnection.StartAsync();			
        }
	}
}

