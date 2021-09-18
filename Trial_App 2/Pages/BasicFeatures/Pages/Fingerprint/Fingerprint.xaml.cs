using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Trial_App_2.Pages.BasicFeatures.Pages.Fingerprint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Fingerprint : ContentPage
    {
        public Fingerprint()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var isAvailable = await CrossFingerprint.Current.IsAvailableAsync();
            if (isAvailable)
            {
                var authenticate = await CrossFingerprint.Current.AuthenticateAsync(new AuthenticationRequestConfiguration("Hi there!", "Please scan here for continuing!")
                {
                    CancelTitle = "Authentication canceled",
                    AllowAlternativeAuthentication = true
                });
                if (authenticate.Status == FingerprintAuthenticationResultStatus.TooManyAttempts)
                {
                    await DisplayAlert("Error", "Please use passcode!", "ok");
                }
                if (authenticate.Status == FingerprintAuthenticationResultStatus.Succeeded)
                {
                    await DisplayAlert("Success", "Scan successful", "ok");
                }
            }
        }
    }
}