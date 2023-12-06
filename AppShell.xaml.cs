using SmartCAN.Pages;
using System.Diagnostics;

namespace SmartCAN;

public partial class AppShell : Shell
{
    public AppShell(int role, User user)
    {
        Debug.WriteLine("This is AppShell : ",user.ToString());
        InitializeComponent();
        
        if (role == 0)

        {
            AddTab("Maps", "Maps", "localisation.png", user, role, typeof(Maps));
            AddTab("Poubelles", "FourthPage", "email.png", user, role, typeof(FourthPage));
            AddTab("Notifications", "ThirdPage", "notification.png", user, role, typeof(ThirdPage));
            AddTab("Profil", "ProfilePage", "profile.png", user, role, typeof(ProfilePage));
        }
        else
        {
            Debug.WriteLine("USer is : ", user.Email, "+", user.Tel, "+", user.Nom);

            AddTab("Maps", "Maps", "localisation.png", user, role, typeof(Maps));

            AddTab("Employés", "SecondPage", "groupe.png", user, role, typeof(SecondPage));
            AddTab("Poubelles", "FourthPage", "email.png", user, role, typeof(FourthPage));
           
            AddTab("Notifications", "notification.png", "ThirdPage", user, role, typeof(ThirdPage));
            AddTab("Profil", "ProfilePage", "profile.png", user, role, typeof(ProfilePage));
        }


    }

    void AddTab(string title, string pageName,string image, User user, int role, Type pageType)
    {
        var tab = new Tab
        {
            Title = title,
            Route = title,
            Icon = image
        };


        var shellContent = new ShellContent
        {
            Content = Activator.CreateInstance(pageType, user, role) as Page, // Passer l'utilisateur au constructeur
            Route = title
        };

        tab.Items.Add(shellContent);
        AppTabs.Items.Add(tab);
    }
}

