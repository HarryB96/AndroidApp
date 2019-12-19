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
    public partial class ProgramDetailPage : ContentPage
    {
        public ProgramDetailPage()
        {
            InitializeComponent();
        }
        public ProgramDetailPage(string exerciseName)
        {
            InitializeComponent();
            Title = exerciseName;
            exerciseDetails(exerciseName);
        }

        Program thisExercise;
        public async void exerciseDetails(string exerciseName)
        {
            List<Program> programList = await App.ProgramRepo.GetProgramAsync();
            foreach (var exercise in programList)
            {
                if (exercise.ExerciseName == exerciseName)
                {
                    thisExercise = exercise;
                }
            }
            PreviousEditor.Text = thisExercise.Weight.ToString();
        }

        private void AchievedEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            Save.IsEnabled = true;
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            double.TryParse(AchievedEditor.Text, out double achievedWeight);
            thisExercise.Weight = achievedWeight;
            await App.ProgramRepo.EditProgramAsync(thisExercise);
            await DisplayAlert("Saved", "Weight achieved for this session has been saved", "OK");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}