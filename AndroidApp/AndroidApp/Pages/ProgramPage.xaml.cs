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
    public partial class ProgramPage : ContentPage
    {
        List<Program> program;
        public ProgramPage()
        {
            InitializeComponent();
            Initialise();
        }
        public async void Initialise()
        {
            program = await App.ProgramRepo.GetProgramAsync();
            SortList();
        }

        public void SortList()
        {
            LegsList.ItemsSource = program.Where(e => e.Day == "Legs").OrderBy(e => e.ExerciseOrder).ToList();
            ChestList.ItemsSource = program.Where(e => e.Day == "Chest").OrderBy(e => e.ExerciseOrder).ToList();
            BackList.ItemsSource = program.Where(e => e.Day == "Back").OrderBy(e => e.ExerciseOrder).ToList();
            ShouldersList.ItemsSource = program.Where(e => e.Day == "Shoulders").OrderBy(e => e.ExerciseOrder).ToList();
        }
    }
}