using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace SmartCAN;

public partial class SecondPage : ContentPage
{
    FireHandle FH = new FireHandle();
    public SecondPage(User user, int role)
	{
		InitializeComponent();
        ListeUsers.VerticalOptions = LayoutOptions.FillAndExpand;
        BindingContext = this;
    }

    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        User userToDelete = button?.BindingContext as User;

        if (userToDelete != null)
        {
            bool userConfirmedDeletion = await DisplayAlert("Confirmation", $"Voulez-vous vraiment supprimer l'utilisateur {userToDelete.Nom} ?", "Oui", "Non");

            if (userConfirmedDeletion)
            {
                bool deletionResult = await FH.DeleteUser(userToDelete);

                if (deletionResult)
                {
                    await DisplayAlert("Done", "Suppression effectuée avec succès", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Erreur lors de la suppression", "OK");
                }
            }
        }
    }

  
    private async void OnApproveClicked(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        User V = button?.BindingContext as User;

        if (V != null)
        {
            FH.Users.FirstOrDefault(x => x.Id == V.Id).Approved = true;
            FH.Users.FirstOrDefault(x => x.Id == V.Id).NotApproved = false;
            userListView.ItemsSource = null;
            userListView.ItemsSource = FH.Users;
            await FH.FirebaseClient.Child($"Users/Normal/{V.Id.ToString()}/Approved").PutAsync(FH.Users.FirstOrDefault(x => x.Id == V.Id).Approved);
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        FH.DownloadAllClients();
        userListView.ItemsSource = FH.Users;
    }
}