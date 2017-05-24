using System.Windows.Input;
using CheapIdeas.Helpers;
using CheapIdeas.Models;
using CheapIdeas.Services;
using Xamarin.Forms;

namespace CheapIdeas.ViewModels
{
    public class EditIdeaViewModel
    {
        ApiServices _apiServices = new ApiServices();
        public Idea Idea { get; set; }

        public ICommand PutCommand
        {
            get
            {
                return new Command(async() =>
                {
                    await _apiServices.PutIdeaAsync(Idea, Settings.AccessToken);
                });
            }
        }
    }
}
