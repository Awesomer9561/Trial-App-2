using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using Trial_App_2.Pages.ImageUpload;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Trial_App_2.ViewModel
{
    class ImageUploadVM : BaseViewModel
    {
        FirebaseStorageHelper firebaseStorageHelper = new FirebaseStorageHelper();

        MediaFile file;
        public string _Image;

        public ICommand _SelectFile { get; set; }
        public ICommand _UploadFile { get; set; }

        public string Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
                OnPropertyChange("Image");
            }
        }
        public bool _isLoading;
        public bool isLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChange("isLoading");
            }
        }

        
        public ImageUploadVM()
        {
            _SelectFile = new Command(SelectFile);
            _UploadFile = new Command(UploadFile);
            _isLoading = false;

        }

        private async void UploadFile(object obj)
        {
            if (file != null)
            {
                isLoading = true;

                Image = Path.GetFileName(file.Path); //Setting the filename
                await firebaseStorageHelper.UploadFile(file.GetStream(), "Trial Folder", Image);

                //Saving to local database
                //SaveImage();
                isLoading = false;
                await App.Current.MainPage.DisplayAlert("", "Uploaded " + Image + " successfully", "ok");
                resetFrame();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("", "Please select a file", "ok");
            }
        }

        private void resetFrame()
        {
            isLoading = false;
        }

        private async void SelectFile(object obj)
        {
            await CheckPermissions();
        }
        private async Task<PermissionStatus> CheckPermissions()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();

            if (status == PermissionStatus.Granted)
            {
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.iOS, new[] { "public.my.comic.extension" } }, // or general UTType values
                    { DevicePlatform.Android, new[] { "*/*" } }
                });

                var options = new PickOptions
                {
                    PickerTitle = "Please select a file",
                    FileTypes = customFileType,
                };
                PickImage();
                return status;
            }

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                return status;
            }

            if (Permissions.ShouldShowRationale<Permissions.StorageRead>())
            {
                // Prompt the user with additional information as to why the permission is needed
            }

            status = await Permissions.RequestAsync<Permissions.StorageRead>();

            return status;
        }

        private async void PickImage()
        {
            await CrossMedia.Current.Initialize();
            try
            {
                file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium
                });
                if (file == null)
                    return;
                else {
                    
                    _Image = ImageSource.FromStream(() => //Setting the image
                    {
                        var imageStream = file.GetStream();
                        return imageStream;
                    }).ToString();
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }
}
