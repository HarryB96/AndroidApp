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
    public partial class Legs : ContentPage
    {
        List<Program> programLegs;
        public Legs()
        {
            InitializeComponent();
            Initialise();
        }
        public async void Initialise()
        {
            programLegs = await App.ProgramRepo.GetProgramAsync();
            LegsList.ItemsSource = programLegs.Where(e => e.Day == "Legs").OrderBy(e => e.ExerciseOrder).ToList();
        }

        private async void LegsList_ItemTapped(object sender, ItemTappedEventArgs e)
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