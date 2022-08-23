using Mapsui;
using Mapsui.Tiling;
using Mapsui.UI;
using Mapsui.Projections;
using Mapsui.Extensions;
using Mapsui.UI.Maui;

namespace FlightTracker.MAUI;

public partial class MainPage : ContentPage
{	
	public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;

        var map = new Mapsui.Map
        {
            CRS = "EPSG:3857"
        };

        map.Layers.Add(OpenStreetMap.CreateTileLayer());

        var spaceNeedleLocation = new MPoint(-122.3493, 47.6205);
        var sphericalMercatorCoordinate = SphericalMercator.FromLonLat(spaceNeedleLocation.X, spaceNeedleLocation.Y).ToMPoint();
        

        map.Home = n => n.NavigateTo(sphericalMercatorCoordinate, map.Resolutions[9]);

        mapView.Map = map;

        viewModel.MapView = mapView;        
    }

    protected override void OnAppearing()
    {
        mapView.IsVisible = true;
        mapView.Refresh();
    }
}


