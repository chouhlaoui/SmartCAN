using Microsoft.Maui.Platform;
using SmartCAN.Handlers;

namespace SmartCAN;

public partial class App : Application
{
    public App()
	{
		InitializeComponent();
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (handler, view) =>
        {
            if (view is BorderlessEntry)
            {
#if __ANDROID__
                handler.PlatformView.SetBackgroundColor(Colors.Transparent.ToPlatform());
#elif __IOS__
                handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            }
        });

        MainPage = new Microsoft.Maui.Controls.NavigationPage(new SignInPage());

        /*
         *  MainPage = new Microsoft.Maui.Controls.NavigationPage(new AppShell(1,new User
            { Id = 0,Approved=true,Email="a",mdp="jn",Nom="kd",NotApproved=false,Tel=5615}
        )
            );
         */
    }
}
