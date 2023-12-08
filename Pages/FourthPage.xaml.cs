using System.Data;
using System.Diagnostics;

namespace SmartCAN;

public partial class FourthPage : ContentPage
{
    FireHandle FH = new FireHandle();
    int Role;
    protected override void OnAppearing()
    {
        base.OnAppearing();
        FH.DownloadAllCans();
        PoubelleListView.ItemsSource = FH.Cans;
        if (Role == 0)
        {
            Activate = false;
        }
        else
        {
            Activate = true;
        }
    }
    public bool Activate {
        get { return _Activate; }
        set
        {
            if (_Activate != value)
            {
                _Activate = value;
                OnPropertyChanged(nameof(Activate));
            }
        }
    }
    private bool _Activate;

    public FourthPage(User user, int role)
	{
        Role = role;

        if (Role == 0)
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
            bool ok = await DisplayAlert("Confirmation", $"Voulez-vous vraiment supprimer la Poubelle {V.Id} ?", "Oui", "Non");
            if (ok)
            {
                bool deletionResult = await FH.DeleteCan(V);

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

    private void OnLocationClicked(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        Can V = button?.BindingContext as Can;
        if (V != null)
        {
            string latitude = V.Latitude;
            string longitude = V.Langitude;

            if (!string.IsNullOrEmpty(latitude) && !string.IsNullOrEmpty(longitude))
            {
                // Construire l'URI pour ouvrir la localisation dans Google Maps
                string uri = $"geo:{latitude},{longitude}?q={latitude},{longitude}({Uri.EscapeDataString("Label de la localisation")})";

                // Ouvrir l'URI avec le launcher
                Launcher.OpenAsync(new Uri(uri));
            }
        }
    }
}