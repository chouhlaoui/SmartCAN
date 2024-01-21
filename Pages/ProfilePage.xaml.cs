
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


    private async void Save(object sender, EventArgs e)
    {
        FireHandle FH = new FireHandle();
        await Task.Run(() =>
        {
            if (string.IsNullOrEmpty(pass.Text) && string.IsNullOrEmpty(passNew.Text))
            {
                if (!string.IsNullOrEmpty(tel.Text))
                {
                    Profil.Tel = int.Parse(tel.Text);
                }
                if (!string.IsNullOrEmpty(nom.Text))
                {
                    Profil.Nom = nom.Text;
                }
                if (!string.IsNullOrEmpty(mail.Text))
                {
                    Profil.Email = mail.Text;
                }
                FH.ModifyClient(Profil.Id, Profil);
            }
            else if ((!string.IsNullOrEmpty(pass.Text) && !string.IsNullOrEmpty(passNew.Text)))
            {
                if ((passNew.Text != Profil.mdp) && (pass.Text == Profil.mdp))
                {
                    if (!string.IsNullOrEmpty(tel.Text))
                    {
                        Profil.Tel = int.Parse(tel.Text);
                    }
                    if (!string.IsNullOrEmpty(nom.Text))
                    {
                        Profil.Nom = nom.Text;
                    }
                    if (!string.IsNullOrEmpty(mail.Text))
                    {
                        Profil.Email = mail.Text;
                    }
                    Profil.mdp = passNew.Text;
                    FH.ModifyClient(Profil.Id, Profil);
                }
            }
        });
        
    }

    public event EventHandler DisconnectButtonClicked;

    public async void Disconnect(object sender, EventArgs e)
    {
        DisconnectButtonClicked?.Invoke(this, EventArgs.Empty);
    }




}