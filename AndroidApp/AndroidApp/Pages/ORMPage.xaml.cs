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
            await App.ORMRepo.AddNewORMAsync(exerciseEntry.Text);
            exerciseEntry.Text = string.Empty;
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

        private void oneRepMaxList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}