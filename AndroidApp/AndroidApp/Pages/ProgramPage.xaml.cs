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
    public partial class ProgramPage : TabbedPage
    {
        public ProgramPage()
        {
            InitializeComponent();

            this.Children.Add(new Legs());
            this.Children.Add(new Chest());
            this.Children.Add(new Back());
            this.Children.Add(new Shoulders());
        }
    }
}