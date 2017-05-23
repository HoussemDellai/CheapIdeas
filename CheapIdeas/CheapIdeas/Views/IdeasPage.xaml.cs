using System;
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
    }
}