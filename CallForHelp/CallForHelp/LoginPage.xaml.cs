using CallForHelp.Utils;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CallForHelp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this;
            IsBusy = false;
        }

        private async void btnLogin_Clicked(object sender, EventArgs e)
        {
            IsBusy = true;

            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtEmail.Text))
            {
                await Utils.Storage.PersistPerson(txtName.Text, txtEmail.Text);

                Application.Current.MainPage = new MainPage();
            }
            else
            {
                await DisplayAlert("Erro", Messages.RequiredFields, "OK");
            }

            IsBusy = false;
        }
    }
}