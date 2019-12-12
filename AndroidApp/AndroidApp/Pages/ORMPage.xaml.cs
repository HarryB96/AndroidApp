using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidApp.Models;
using System.ComponentModel;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AndroidApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ORMPage : ContentPage
    {
        //Constructor for the One Rep Max page
        public ORMPage()
        {
            InitializeComponent();
            Initialise();
            this.OnAppearing();
        }

        //Button click event for calculating One Rep Maxes
        private void ClaculateButton_Clicked(object sender, EventArgs e)
        {
                double.TryParse(WeightEditor.Text, out double weight);
                int.TryParse(RepsEditor.Text, out int reps);
                double oneRepMax = Math.Round((weight * 36) / (37 - reps), 1);
                ORMEditor.Text = oneRepMax.ToString();
                ClearButton.IsEnabled = true;
        }

        //Button click event for clearing all information on the page
        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            Clear();
        }

        //Button click event for saving a One Rep Max
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            double.TryParse(WeightEditor.Text, out double weight);
            int.TryParse(RepsEditor.Text, out int reps);
            double oneRepMax = Math.Round((weight * 36) / (37 - reps), 1);
            await App.ORMRepo.AddNewORMAsync(exerciseEntry.Text, oneRepMax, weight, reps);
            await DisplayAlert("Added", "Your new one rep max has been added", "OK");
            Initialise();
        }

        //Initialising the source for the list view  and clearing all data from the page
        public async void Initialise()
        {
            exerciseEntry.Text = string.Empty;
            oneRepMaxList.ItemsSource = null;
            List<OneRepMax> oneRepMaxes = await App.ORMRepo.GetOneRepMaxesAsync();
            oneRepMaxList.ItemsSource = oneRepMaxes;
            Clear();
        }

        //Clear method preventing repeat code
        private void Clear()
        {
            ORMEditor.Text = string.Empty;
            WeightEditor.Text = string.Empty;
            RepsEditor.Text = string.Empty;
            ClearButton.IsEnabled = false;
        }

        //Select item event
        private async void oneRepMaxList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as OneRepMax;
            await Navigation.PushAsync(new OneRepMaxDetailPage(item.ExerciseName), true);
        }
        
        //Method for refreshing the page when an entry is deleted
        protected override void OnAppearing()
        {
            Initialise();
        }
    }
}