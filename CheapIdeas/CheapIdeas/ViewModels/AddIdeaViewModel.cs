using System.Windows.Input;
using CheapIdeas.Helpers;
using CheapIdeas.Models;
using CheapIdeas.Services;
using Xamarin.Forms;

namespace CheapIdeas.ViewModels
{
    public class AddIdeaViewModel
    {
        ApiServices _apiServices = new ApiServices();
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public ICommand AddCommand
        {
            get
            {
                return new Command(async() =>
                {
                    var idea = new Idea
                    {
                        Title = Title,
                        Description = Description,
                        Category = Category
                    };
                    await _apiServices.PostIdeaAsync(idea, Settings.AccessToken);
                });
            }
        }
    }
}
