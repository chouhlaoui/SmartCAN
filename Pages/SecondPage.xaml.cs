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
        Debug.WriteLine("This is Users page : ", user.ToString());


        BindingContext = this;

    }


    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        var button = (ImageButton)sender;
        User V = button?.BindingContext as User;
        if (V != null)
        {
            bool ok = await DisplayAlert("Supprimer", "Etes-vous s�r ?", "OK", "Annuler");
            if (ok)
            {
                bool deletionResult = FH.DeleteUser(V.Id);

                if (deletionResult)
                {
                    await DisplayAlert("Done", "Suppression effectu�e avec succ�s", "OK");
                }
                else
                {
                    // Handle the case where deletion failed
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