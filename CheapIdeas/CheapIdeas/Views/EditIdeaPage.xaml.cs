using CheapIdeas.Models;
using CheapIdeas.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheapIdeas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditIdeaPage : ContentPage
    {
        public EditIdeaPage(Idea idea)
        {
            InitializeComponent();

            var editIdeaViewModel = new EditIdeaViewModel();
            //var editIdeaViewModel = BindingContext as EditIdeaViewModel;

            editIdeaViewModel.Idea = idea;

            BindingContext = editIdeaViewModel;
        }
    }
}