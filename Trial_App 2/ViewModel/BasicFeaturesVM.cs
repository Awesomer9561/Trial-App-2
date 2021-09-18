using Plugin.Calendars.Abstractions;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Trial_App_2.Pages.BasicFeatures.Pages.BarCode;
using Trial_App_2.Pages.BasicFeatures.Pages.Calender;
using Trial_App_2.Pages.BasicFeatures.Pages.Fingerprint;
using Trial_App_2.Pages.BasicFeatures.Pages.Media_Player;
using Trial_App_2.Pages.BasicFeatures.Pages.Recorder;
using Xamarin.Forms;

namespace Trial_App_2.ViewModel
{
    class BasicFeaturesVM : BaseViewModel
    {
        public bool _isBusy;
        public Calendar _selectedItem;
        public ICommand _Calender { get; set; }
        public ICommand _Recorder { get; set; }
        public ICommand _Barcode { get; set; }
        public ICommand _Fingerprint { get; set; }
        public ICommand _Mediaplayer { get; set; }
        public ICommand _AddEvent { get; set; }

        public bool isBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChange("isBusy");
            }
        }
        public Calendar selectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChange("selectedItem");
                //AddMyEvent();
            }
        }

        public ObservableCollection<string> _Picker;
        public ObservableCollection<string> Picker
        {
            get { return _Picker; }
            set
            {
                _Picker = value;
                OnPropertyChange("Picker");
            }
        }

        public BasicFeaturesVM()
        {
            _Calender = new Command(Calender);
            _Recorder = new Command(Recorder);
            _Barcode = new Command(Barcode);
            _Fingerprint = new Command(Fingerprint);
            _Mediaplayer = new Command(MediaPlayer);

            _isBusy = false;
            Picker = new ObservableCollection<string>();

        }

        private void MediaPlayer(object obj)
        {
            //MessagingCenter.Send(this, AppSettings.TransitionMessage, TransitionType.Fade);
            App.Current.MainPage.Navigation.PushModalAsync(new MediaPlayer());
        }

        private void Fingerprint(object obj)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new Fingerprint());
        }

        private void Barcode(object obj)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new BarCodeScanner());
        }

        private void Recorder(object obj)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new AudioRecorder());
        }

        private void Calender(object obj)
        {
            App.Current.MainPage.Navigation.PushModalAsync(new CalenderPages());
        }
    }

}
