using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidApp.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AndroidApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Shoulders : ContentPage
    {
        List<Program> programShoulders;
        public Shoulders()
        {
            InitializeComponent(); Initialise();
        }
        public async void Initialise()
        {
            programShoulders = await App.ProgramRepo.GetProgramAsync();
            ShouldersList.ItemsSource = programShoulders.Where(e => e.Day == "Shoulders").OrderBy(e => e.ExerciseOrder).ToList();
        }

        private async void ShouldersList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Program;
            await Navigation.PushAsync(new ProgramDetailPage(item.ExerciseName), true);
        }
        protected override void OnAppearing()
        {
            Initialise();
        }
    }
}