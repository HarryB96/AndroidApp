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
        public ProgramPage()
        {
            InitializeComponent();
            Initialise();
        }
        public async void Initialise()
        {
            List<Program> program = await App.ProgramRepo.GetProgramAsync();
            programList.ItemsSource = program;
        }
    }
}