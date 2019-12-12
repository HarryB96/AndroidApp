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
        string dbPath => FileAccessHelper.GetLocalFilePath("gym.db3");
        internal static ORMRepository ORMRepo { get; private set; }
        public App()
        {
            InitializeComponent();

            ORMRepo = new ORMRepository(dbPath);

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
