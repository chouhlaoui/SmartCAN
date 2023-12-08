using SmartCAN.Pages;
using System.Diagnostics;

namespace SmartCAN;

public partial class AppShell : Shell
{
    public AppShell(int role, User user)
    {
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
    private async void OnDisconnectButtonClicked(object sender, EventArgs e)
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

        if (Navigation != null)
        {
            
            await Navigation.PopModalAsync();
            
        }
    }
    void AddTab(string title, string pageName, string image, User user, int role, Type pageType)
    {
        var tab = new Tab
        {
            Title = title,
            Route = title,
            Icon = image
        };

        Page page;

        if (pageType == typeof(ProfilePage))
        {
            var profilePage = new ProfilePage(user, role);
            profilePage.DisconnectButtonClicked += OnDisconnectButtonClicked;
            page = profilePage;
        }
        else
        {
            page = Activator.CreateInstance(pageType, user, role) as Page;
        }

        var shellContent = new ShellContent
        {
            Content = page,
            Route = title
        };

        tab.Items.Add(shellContent);
        AppTabs.Items.Add(tab);
    }

    
}

