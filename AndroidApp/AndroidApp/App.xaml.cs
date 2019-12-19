using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AndroidApp
{
    public partial class App : Application
    {
        //File path for SQLite database
        string dbPath => FileAccessHelper.GetLocalFilePath("gym.db3");
        internal static ORMRepository ORMRepo { get; private set; }
        internal static ProgramRepository ProgramRepo { get; private set; }
        public App()
        {
            //Initialising Repository for One Rep Maxes
            InitializeComponent();
            ORMRepo = new ORMRepository(dbPath);
            ProgramRepo = new ProgramRepository(dbPath);
            //Setting the main page as a navigation page
            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
