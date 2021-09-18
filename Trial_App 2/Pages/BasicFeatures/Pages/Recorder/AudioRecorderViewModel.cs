using MediaManager;
using Plugin.AudioRecorder;
using System.Windows.Input;
using Trial_App_2.Utilities;
using Trial_App_2.ViewModel;
using Xamarin.Forms;

namespace Trial_App_2.Pages.BasicFeatures.Pages.Recorder
{
    class AudioRecorderViewModel : BaseViewModel
    {
        private readonly AudioRecorderService service = new AudioRecorderService();
        //private readonly AudioPlayer player = new AudioPlayer();
        
        public bool isPlaying;
        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                isPlaying = value;
                OnPropertyChange("IsPlaying");
            }
        }

        public ICommand Play { get; }
        public ICommand Record { get; }
        public ICommand Pause { get; }
        public ICommand Stop { get; }
        public ICommand Recorder { get; }
        public AudioRecorderViewModel()
        {
            
            Play = new Command(playRecording);
            Pause = new Command(pauseRecording);
            Record = new Command(recordAudio);
            Stop = new Command(stopRecordingAudio);
            Recorder = new Command(recorder);
        }

        private async void recorder(object obj)
        {
            await Constants.CheckRecorderPermissions();
            await Constants.CheckStoragePermissions();
            
            
            if (service.IsRecording)
            {
                await service.StopRecording();
                if (service.GetAudioFilePath()!=null)
                {
                    
                    await CrossMediaManager.Current.Play(service.GetAudioFilePath());
                }
            }
            else
            {
                var recording = await service.StartRecording();
            }
        }

        private async void stopRecordingAudio(object obj)
        {
            IsPlaying = false;
            await service.StopRecording();

            //await CrossMediaManager.Current.Play(service.GetAudioFilePath());
            //pauseRecording(null);
        }

        private void recordAudio(object obj)
        {
            IsPlaying = true;
            service.StartRecording();
        }

        private void pauseRecording(object obj)
        {
            IsPlaying = false;
            CrossMediaManager.Current.Pause();
        }

        private void playRecording(object obj)
        {
            IsPlaying = true;
            CrossMediaManager.Current.Play(service.GetAudioFilePath());
        }
    }
}
