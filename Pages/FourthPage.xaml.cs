using System.Data;
using System.Diagnostics;

namespace SmartCAN;

public partial class FourthPage : ContentPage
{
    FireHandle FH = new FireHandle();
    int Role;

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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Task.Run(() => FH.DownloadAllCans()).Wait();
        PoubelleListView.ItemsSource = FH.Cans;
        Activate = Role != 0;
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
                    await DisplayAlert("Done", "Suppression effectu�e avec succ�s", "OK");
                }
                else
                {
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
                string uri = $"geo:{latitude},{longitude}?q={latitude},{longitude}({Uri.EscapeDataString("Label de la localisation")})";
                Launcher.OpenAsync(new Uri(uri));
            }
        }
    }
}