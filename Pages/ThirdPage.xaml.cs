using System.Diagnostics;

namespace SmartCAN;

public partial class ThirdPage : ContentPage
{
    FireHandle FH = new FireHandle();
	public ThirdPage(User user, int role)
    {
		InitializeComponent();

    }
    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();

            InitializePageAsync();
        }
        catch (Exception ex)
        {
        }

    }
    private async Task<bool> InitializePageAsync()
    {
        try
        {
            await FH.Subscribe();
            notificationListView.ItemsSource = FH.notificationCollection;
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred during initialization: {ex.Message}");
            return false;
        }
    }
}