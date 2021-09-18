using MediaManager;
using Xamarin.Forms;

namespace Trial_App_2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            CrossMediaManager.Current.Init();

            //MainPage = new Pages.BasicFeatures.Pages.BarCode.BarCodeScanner();
            //MainPage = new Pages.BasicFeatures.Pages.Media_Player.MediaPlayer();
            MainPage = new NavigationPage(new Pages.BasicFeatures.BasicFeatures());
            //MainPage = new Pages.BasicFeatures.Pages.Calender();
            //MainPage = new Pages.BasicFeatures.Pages.Recorder.AudioRecorder();
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
