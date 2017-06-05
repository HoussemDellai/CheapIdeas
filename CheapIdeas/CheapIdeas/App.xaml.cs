using System;
using CheapIdeas.Helpers;
using CheapIdeas.ViewModels;
using CheapIdeas.Views;
using Xamarin.Forms;

namespace CheapIdeas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            SetMainPage();
            //MainPage = new NavigationPage(new RegisterPage());
            //MainPage = new CheapIdeas.MainPage();
        }

        private void SetMainPage()
        {
            if (!string.IsNullOrEmpty(Settings.AccessToken))
            {
                if (Settings.AccessTokenExpirationDate < DateTime.UtcNow.AddHours(1))
                {
                    var loginViewModel = new LoginViewModel();
                    loginViewModel.LoginCommand.Execute(null);
                }
                MainPage = new NavigationPage(new IdeasPage());
            }
            else if (!string.IsNullOrEmpty(Settings.Username) 
                  && !string.IsNullOrEmpty(Settings.Password))
            {
                MainPage = new NavigationPage(new LoginPage());
            }
            else
            {
                MainPage = new NavigationPage(new RegisterPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
