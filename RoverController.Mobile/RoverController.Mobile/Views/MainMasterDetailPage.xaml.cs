using System.Threading.Tasks;
using Xamarin.Forms;

namespace RoverController.Mobile.Views
{
    public partial class MainMasterDetailPage : MasterDetailPage
    {
        public MainMasterDetailPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        private async void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if (sender is Layout)
            {
                (sender as Layout).BackgroundColor = (Color)Application.Current.Resources["primaryColor"];
                await Task.Delay(100);
                (sender as Layout).BackgroundColor = Color.Transparent;
            }
        }
    }
}