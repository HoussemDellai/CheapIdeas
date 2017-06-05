using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CheapIdeas.Helpers;
using CheapIdeas.Models;
using CheapIdeas.Services;
using Xamarin.Forms;

namespace CheapIdeas.ViewModels
{
    public class IdeasViewModel : INotifyPropertyChanged
    {
        private readonly ApiServices _apiServices = new ApiServices();
        private List<Idea> _ideas;

        //public string AccessToken { get; set; }
        public List<Idea> Ideas
        {
            get { return _ideas; }
            set
            {
                _ideas = value;
                OnPropertyChanged();
            }
        }
        public ICommand GetIdeasCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var accessToken = Settings.AccessToken;
                    Ideas = await _apiServices.GetIdeasAsync(accessToken);
                });
            }
        }

        public ICommand LogoutCommand
        {
            get
            {
                return new Command(() =>
                {
                    Settings.AccessToken = string.Empty;
                    Debug.WriteLine(Settings.Username);
                    Settings.Username = string.Empty;
                    Debug.WriteLine(Settings.Password);
                    Settings.Password = string.Empty;

                    // navigate to LoginPage
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
