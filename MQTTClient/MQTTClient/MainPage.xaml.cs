using MQTTClient.ViewModels;

namespace MQTTClient;

public partial class MainPage : ContentPage
{
	MainViewModel _viewModel;

	public MainPage(MainViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = _viewModel = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

		_viewModel.CreateClient().ContinueWith(t => { });
    }
}