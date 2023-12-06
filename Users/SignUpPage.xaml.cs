namespace SmartCAN;

using Firebase.Database;
using Firebase.Database.Query;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public partial class SignUpPage : ContentPage
{

	public SignUpPage()
	{
		InitializeComponent();
		BindingContext = this;
	}

	private async void TapGestureRecognizer_Tapped_For_SignIn(object sender, EventArgs e)
	{
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


            FirebaseClient client = new FirebaseClient("https://projet-iot-2ing2-default-rtdb.europe-west1.firebasedatabase.app/");

            var emplacement = await client.Child("Users/Nombre_Users").OnceSingleAsync<int>();
            emplacement++;
            await client.Child("Users/Nombre_Users").PutAsync(emplacement);
            await client.Child($"Users/Normal/{emplacement}").PutAsync(new User { 
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