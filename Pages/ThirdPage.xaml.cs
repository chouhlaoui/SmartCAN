using System.Diagnostics;

namespace SmartCAN;

public partial class ThirdPage : ContentPage
{
	public ThirdPage(User user, int role)
    {
		InitializeComponent();
        Debug.WriteLine("This is Notification : ", user.ToString());

    }
}