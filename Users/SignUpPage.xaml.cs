namespace SmartCAN;

using Firebase.Database;
using Firebase.Database.Query;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public partial class SignUpPage : ContentPage
{
    FireHandle FH = new FireHandle();

	public SignUpPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	private async void TapGestureRecognizer_Tapped_For_SignIn(object sender, EventArgs e)
	{

        var navigationStack = Navigation.NavigationStack;

        if (navigationStack != null)
        {
            foreach (var page in navigationStack)
            {
                if (page != null)
                {
                    Debug.WriteLine($"Page Type: {page.GetType().Name}");
                    // You can add more information as needed, such as page titles or other properties
                }
            }
        }
        await Navigation.PopModalAsync();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        string Nom = nom.Text;
		string Mail = mail.Text;
		string Tel = tel.Text;
		string MDP = pass.Text;


        if (string.IsNullOrEmpty(Nom) || string.IsNullOrWhiteSpace(Mail) || string.IsNullOrEmpty(Tel) || string.IsNullOrWhiteSpace(MDP))
        {
            await DisplayAlert("Erreur", "Veuillez remplir tous les champs !", "OK");
        }
        else
        {
            if (!Regex.IsMatch(Nom, @"^[a-zA-Z -]+$") || !Regex.IsMatch(Mail, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$") || !Regex.IsMatch(Tel, @"^[0-9]+$"))
            {
                await DisplayAlert("Erreur", "Veuillez verifier les données tapeées ! Format non accepté !", "OK");
                return;
            }


            var emplacement = await FH.FirebaseClient.Child("Users/Nombre_Users").OnceSingleAsync<int>();
            emplacement++;
            await FH.FirebaseClient.Child("Users/Nombre_Users").PutAsync(emplacement);
            FH.AddNewClient(new User
            {
                Id = emplacement,
                NotApproved = true,
                Approved = false,
                Email = Mail,
                mdp = MDP,
                Nom = Nom,
                Tel = int.Parse(Tel)
            });

            await DisplayAlert("OK", "Votre demande est bien enregistré et elle sera traité par administrateur ! ", "OK");
            await Navigation.PopModalAsync();



        }

    }
}