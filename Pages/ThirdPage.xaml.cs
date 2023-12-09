using System.Diagnostics;

namespace SmartCAN;

public partial class ThirdPage : ContentPage
{
    FireHandle FH = new FireHandle();
	public ThirdPage(User user, int role)
    {
		InitializeComponent();
        //InitializePageAsync();

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
            // Handle or log the exception
            Debug.WriteLine("Exception in OnAppearing: " + ex.Message);
        }

    }
    private async Task<bool> InitializePageAsync()
    {
        try
        {
            // If Subscribe involves asynchronous operations, await it
            await FH.Subscribe();

            // Populate notificationListView
            notificationListView.ItemsSource = FH.notificationCollection;
            return true;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"An error occurred during initialization: {ex.Message}");
            // Handle or log the exception as needed
            return false;
        }
    }
}