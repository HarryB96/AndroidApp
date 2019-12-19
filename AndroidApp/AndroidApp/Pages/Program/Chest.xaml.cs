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
    public partial class Chest : ContentPage
    {
        List<Program> programChest;
        public Chest()
        {
            InitializeComponent(); Initialise();
        }
        public async void Initialise()
        {
            programChest = await App.ProgramRepo.GetProgramAsync();
            ChestList.ItemsSource = programChest.Where(e => e.Day == "Chest").OrderBy(e => e.ExerciseOrder).ToList();
        }
        protected override void OnAppearing()
        {
            Initialise();
        }

        private async void ChestList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Program;
            await Navigation.PushAsync(new ProgramDetailPage(item.ExerciseName), true);
        }
    }
}