using Xamarin.Forms;

namespace CallForHelp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DefineWhichPageToOpen();
        }

        private async void DefineWhichPageToOpen()
        {
            bool nameAlreadyExists = await Utils.Storage.NameAlreadyExists();

            if (nameAlreadyExists)
                MainPage = new MainPage();
            else
                MainPage = new LoginPage();
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