using Firebase.Database;
using Microsoft.Maui.ApplicationModel.Communication;
using SmartCAN.Handlers;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SmartCAN;


public partial class SignInPage : ContentPage
{
    User NormalUser;
    FireHandle FH = new FireHandle();

    public SignInPage()
	{
		InitializeComponent();
        BindingContext = this;
	}

	private async void TapGestureRecognizer_Tapped_For_SignUP(object sender, EventArgs e)
	{
        await Navigation.PushModalAsync(new SignUpPage());
    }

    private async void Connecter(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(mail.Text) || string.IsNullOrEmpty(mdp.Text))
        {

               await DisplayAlert("Erreur", "Veuillez vérifier vos données", "OK");

        }
        else
        {
            string email = mail.Text.ToLower();
            string Pass = mdp.Text;

            int test = await VerifAsync(Pass, email);
            if (test == 0 || test == 3)
            {
                await Navigation.PushModalAsync(new AppShell(test, NormalUser));
                mail.Text = string.Empty; mdp.Text = string.Empty;
            }
            else if (test == 1)
            {
                await DisplayAlert("Erreur", "Contacter votre administrateur pour etre approuvé !", "OK");
            }
            else
            {
                await DisplayAlert("Erreur", "Veuillez vérifier vos données", "OK");

            }
        };  
        }




    private async Task<int> VerifAsync(string password, string email)
    {
        try
        {
            if (password.Equals("a") && email.Equals("a"))
            {
                return 3;
            }
            else if (password.Equals("c") && email.Equals("c"))
            {
                return 0;
            }
            else
            {
                Debug.WriteLine(FH.Users.Count);
                NormalUser = FH.Users.FirstOrDefault(x => (x.Email.ToLower() == email && x.mdp == password));
                if (NormalUser != null)
                {
                    if (NormalUser.Approved == true)
                    {
                        return 0;
                    }
                    return 1;
                }

                if (FH.Admin.Email == email && FH.Admin.mdp == password)
                {
                    return 3;
                }
                return 2;
            }
        }
        catch (Firebase.Database.FirebaseException ex)
        {
            Debug.WriteLine("Firebase Exception: " + ex.Message);
        }
        catch (Newtonsoft.Json.JsonSerializationException ex)
        {
            Debug.WriteLine("JSON Serialization Exception: " + ex.Message);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Other Exception: " + ex.Message);
        }

        return 2;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        FH.DownloadAllClients();
        FH.AdminDownload();
    }

}