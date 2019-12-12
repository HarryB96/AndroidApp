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
        public ORMPage()
        {
            InitializeComponent();
            Initialise();
            this.OnAppearing();
        }
        
        private void ClaculateButton_Clicked(object sender, EventArgs e)
        {
                double weight = 0;
                double.TryParse(WeightEditor.Text, out weight);
                int reps = 0;
                int.TryParse(RepsEditor.Text, out reps);
                double oneRepMax = Math.Round((weight * 36) / (37 - reps), 1);
                ORMEditor.Text = oneRepMax.ToString();
                ClearButton.IsEnabled = true;
        }

        private void ClearButton_Clicked(object sender, EventArgs e)
        {
            Clear();
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            double.TryParse(WeightEditor.Text, out double weight);
            int.TryParse(RepsEditor.Text, out int reps);
            double oneRepMax = Math.Round((weight * 36) / (37 - reps), 1);
            await App.ORMRepo.AddNewORMAsync(exerciseEntry.Text, oneRepMax, weight, reps);
            await DisplayAlert("Added", "Your new one rep max has been added", "OK");
            Initialise();
        }

        public async void Initialise()
        {
            exerciseEntry.Text = string.Empty;
            oneRepMaxList.ItemsSource = null;
            List<OneRepMax> oneRepMaxes = await App.ORMRepo.GetOneRepMaxesAsync();
            oneRepMaxList.ItemsSource = oneRepMaxes;
            Clear();
        }

        private void Clear()
        {
            ORMEditor.Text = string.Empty;
            WeightEditor.Text = string.Empty;
            RepsEditor.Text = string.Empty;
            ClearButton.IsEnabled = false;
        }

        private async void oneRepMaxList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as OneRepMax;
            await Navigation.PushAsync(new OneRepMaxDetailPage(item.ExerciseName), true);
        }
        
        protected override void OnAppearing()
        {
            Initialise();
        }
    }
}