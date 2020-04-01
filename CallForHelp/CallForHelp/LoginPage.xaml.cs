using System;
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

            if (!string.IsNullOrEmpty(txtName.Text))
            {
                await Utils.Storage.PersistName(txtName.Text);
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                await DisplayAlert("Erro", "O campo nome é obrigatório!", "OK");
            }

            IsBusy = false;
        }
    }
}