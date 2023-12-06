using System.Data;
using System.Diagnostics;

namespace SmartCAN;

public partial class FourthPage : ContentPage
{
    FireHandle FH = new FireHandle();
    protected override void OnAppearing()
    {
        base.OnAppearing();
        FH.DownloadAllCans();
        PoubelleListView.ItemsSource = FH.Cans;
    }
    public bool Activate { get; set; }
    public FourthPage(User user, int role)
	{
        Debug.WriteLine("This is poubelle : ", user.ToString());

        if (role == 0)
        {
            Activate = false;
        }
        else
        {
            Activate = true;
        }
		InitializeComponent();
        ListePoubelles.VerticalOptions = LayoutOptions.FillAndExpand;
        BindingContext = this;
    }
    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        Can V = button?.BindingContext as Can;
        if (V != null)
        {
            bool ok = await DisplayAlert("Supprimer", "Etes-vous sûr ?", "OK", "Annuler");
            if (ok)
            {
                bool deletionResult = false;

                if (deletionResult)
                {
                    await DisplayAlert("Done", "Suppression effectuée avec succès", "OK");
                }
                else
                {
                    // Handle the case where deletion failed
                    await DisplayAlert("Error", "Erreur lors de la suppression", "OK");
                }
            }
        }
    }
}