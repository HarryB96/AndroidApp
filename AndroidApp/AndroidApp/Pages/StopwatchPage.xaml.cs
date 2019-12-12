using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AndroidApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StopwatchPage : ContentPage
    {
        //Initialising a new stopwatch
        public static Stopwatch s = new Stopwatch();

        //Constructor for the stopwatch page
        public StopwatchPage()
        {
            InitializeComponent();
            Output.Text = "00:00:00.00";
        }

        //Button click event for the start button
        private void StartButton_Clicked(object sender, EventArgs e)
        {
            //Timer to update the screen when the stopwatch ticks
            Device.StartTimer(TimeSpan.FromMilliseconds(100), () =>
            {
                TimeSpan ts = s.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds,ts.Milliseconds / 10);
                Output.Text = elapsedTime;
                return true;
            });

            //If and else statement to change the start button from start to stop
            if (StartButton.Text =="Start")
            {
                s.Start();
                StartButton.Text = "Stop";
                ResetButton.IsEnabled = false;
            }
            else
            {
                StartButton.Text = "Start";
                s.Stop();
                ResetButton.IsEnabled = true;
            }
        }

        //Button click event to reset the stopwatch
        private void ResetButton_Clicked(object sender, EventArgs e)
        {
            s.Reset();
            ResetButton.IsEnabled = false;
        }
        
    }
}