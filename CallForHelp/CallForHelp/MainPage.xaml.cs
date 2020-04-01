using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace CallForHelp
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //Test
            var location = GetLocation().Result;
        }

        private async Task<Location> GetLocation()
        {
            Location location = null;

            try
            {
                location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.High);
                    location = await Geolocation.GetLocationAsync(request);
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

            return location;
        }

        private void btnHelp_Clicked(object sender, EventArgs e)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                var location = GetLocation().Result;
                var persistedPerson = (Utils.Storage.GetPersistedPerson()).Result;

                var request = new Request
                {
                    Latitude = location.Latitude.ToString(),
                    Longitude = location.Longitude.ToString(),
                    RequestorId = persistedPerson.Email,
                    Name = persistedPerson.Name
                };

                var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

                var response = httpClient.PostAsync("<URL>", stringContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    DisplayAlert("Sucesso", "Solicitação efetuada com sucesso!", "OK").GetAwaiter().GetResult();
                }

            }
        }
    }
}