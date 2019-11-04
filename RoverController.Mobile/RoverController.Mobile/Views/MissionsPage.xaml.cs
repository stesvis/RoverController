using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RoverController.Mobile.Views
{
    public partial class MissionsPage : ContentPage
    {
        public MissionsPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (sender is Layout layout)
                {
                    layout.BackgroundColor = Color.LightGray;
                    await Task.Delay(250);
                    layout.BackgroundColor = Color.White;
                }
            });
        }
    }
}