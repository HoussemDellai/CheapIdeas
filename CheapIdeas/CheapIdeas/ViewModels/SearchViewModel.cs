using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CheapIdeas.Helpers;
using CheapIdeas.Models;
using CheapIdeas.Services;
using Xamarin.Forms;

namespace CheapIdeas.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        private readonly ApiServices _apiServices = new ApiServices();
        private List<Idea> _ideas;

        public List<Idea> Ideas
        {
            get { return _ideas; }
            set
            {
                _ideas = value; 
                OnPropertyChanged();
            }
        }

        public string Keyword { get; set; }

        public ICommand SearchCommand
        {
            get
            {
                return new Command(async() =>
                {
                    Ideas = await _apiServices.SearchIdeasAsync(Keyword, Settings.AccessToken);
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
