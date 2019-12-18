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
    public partial class OneRepMaxDetailPage : ContentPage
    {
        //Constructors for the details page
        public OneRepMaxDetailPage()
        {
            InitializeComponent();
        }
        public OneRepMaxDetailPage(string exerciseName)
        {
            InitializeComponent();
            Title = exerciseName;
            oneRepMaxDetails(exerciseName);
            ORMOutPut.Text = thisExercise.OneRep.ToString();
        }

        OneRepMax thisExercise;

        //Getting the details of the exercise selection from the SQLite database
        public async void oneRepMaxDetails(string exerciseName)
        {
            List<OneRepMax> oneRepMaxes = await App.ORMRepo.GetOneRepMaxesAsync();

            foreach (var exercise in oneRepMaxes)
            {
                if (exercise.ExerciseName == exerciseName)
                {
                    thisExercise = exercise;
                }
            }
            WeightEditor.Text = thisExercise.Weight.ToString();
            RepsEditor.Text = thisExercise.Reps.ToString();
        }

        //Button click event for editing the details
        private async void EditButton_Clicked(object sender, EventArgs e)
        {
            if (EditButton.Text == "Edit")
            {
                WeightEditor.IsReadOnly = false;
                RepsEditor.IsReadOnly = false;
                EditButton.Text = "Save";
            }
            else
            {
                int.TryParse(RepsEditor.Text, out int reps);
                thisExercise.Reps = reps;
                double.TryParse(WeightEditor.Text, out double weight);
                thisExercise.Weight = weight;
                double oneRepMax = Math.Round((weight * 36) / (37 - reps), 1);
                thisExercise.OneRep = oneRepMax;
                ORMOutPut.Text = oneRepMax.ToString();
                EditButton.Text = "Edit";
                await App.ORMRepo.EditORMAsync(thisExercise);
            }
        }

        //Button click event for deleting an entry
        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Delete?", "Are you sure?", "Yes", "No");
            if (answer)
            {
                await App.ORMRepo.RemoveORMAsync(thisExercise);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}