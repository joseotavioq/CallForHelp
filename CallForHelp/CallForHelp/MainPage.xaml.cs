using CallForHelp.DTO;
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
        HttpClient _httpClient = null;
        public MainPage()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
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

        private async void btnHelp_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;

            var location = await GetLocation();
            var persistedPerson = await Utils.Storage.GetPersistedPerson();

            var request = new Request
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                RequestorId = persistedPerson.Email,
                Name = persistedPerson.Name
            };

            var stringContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("<URL>", stringContent);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Sucesso", "Solicitação efetuada com sucesso! Aguarde até que alguem entre em contato com você!", "OK");
            }
            else
            {
                await DisplayAlert("Erro", "Ocorreu um erro ao efetuar sua solicitação. Tente novamente mais tarde!", "OK");
            }

            IsBusy = false;
        }
    }
}