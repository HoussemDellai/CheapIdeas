using System;
using CheapIdeas.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CheapIdeas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IdeasPage : ContentPage
    {
        public IdeasPage()
        {
            InitializeComponent();
        }

        private async void NavigateToAddNewIdea_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddIdeaPage());
        }

        private async void IdeasListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var idea = e.Item as Idea;
            await Navigation.PushAsync(new EditOrDeleteIdeaPage(idea));
        }

        private async void NavigateToSearchIdea_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }
    }
}