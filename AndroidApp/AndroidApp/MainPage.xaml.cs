using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using AndroidApp.Pages;

namespace AndroidApp
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            //Adding the other pages to the tabbed page
            this.Children.Add(new ProgramPage());
            this.Children.Add(new ORMPage());
            this.Children.Add(new StopwatchPage());
        }
    }
}
