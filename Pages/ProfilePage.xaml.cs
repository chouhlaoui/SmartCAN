
using System.Diagnostics;

namespace SmartCAN.Pages;

public partial class ProfilePage : ContentPage
{
    private User _profil;
    public User Profil
    {
        get { return _profil; }
        set
        {
            if (_profil != value)
            {
                _profil = value;
                OnPropertyChanged(nameof(Profil));
            }
        }
    }

    private string _profilPassAnc;
    public string ProfilPassAnc
    {
        get { return _profilPassAnc; }
        set
        {
            if (_profilPassAnc != value)
            {
                _profilPassAnc = value;
                OnPropertyChanged(nameof(ProfilPassAnc));
            }
        }
    }

    private string _profilPassNou;
    public string ProfilPassNou
    {
        get { return _profilPassNou; }
        set
        {
            if (_profilPassNou != value)
            {
                _profilPassNou = value;
                OnPropertyChanged(nameof(ProfilPassNou));
            }
        }
    }


    public ProfilePage(User user, int role)
	{
		InitializeComponent();
        Debug.WriteLine("This is profilepage : ", user.ToString());
        Profil = user; 

        BindingContext = this;

    }


    private void Save(object sender, EventArgs e)
    {
        if(string.IsNullOrEmpty(pass.Text) && string.IsNullOrEmpty(passNew.Text))
        {
            if(string.IsNullOrEmpty(tel.Text))
            {
                Profil.Tel = int.Parse(tel.Text);
            }
            if (string.IsNullOrEmpty(nom.Text))
            {
                Profil.Nom = nom.Text;
            }
            if (string.IsNullOrEmpty(mail.Text))
            {
                Profil.Email = mail.Text;
            }
            FireHandle FH = new FireHandle();
            FH.addClient(Profil.Id,Profil);
        }
    }
    public async void Disconnect(object sender, EventArgs e)
    {

        if (Navigation != null)
        {
            await Navigation.PopModalAsync();
        }
    }
}