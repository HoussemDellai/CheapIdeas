using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheapIdeas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void NavigateToIdeasPage_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new IdeasPage());
        }
    }
}
