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
    public partial class Back : ContentPage
    {
        List<Program> programBack;
        public Back()
        {
            InitializeComponent(); 
            Initialise();
        }
        public async void Initialise()
        {
            programBack = await App.ProgramRepo.GetProgramAsync();
            BackList.ItemsSource = programBack.Where(e => e.Day == "Back").OrderBy(e => e.ExerciseOrder).ToList();
        }

        protected override void OnAppearing()
        {
            Initialise();
        }
        private async void BackList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Program;
            await Navigation.PushAsync(new ProgramDetailPage(item.ExerciseName), true);
        }
    }
}