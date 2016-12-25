using NetNamServices;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace NetNamAutoConnect
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LoadCredential();
        }

        private void _btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private async void LoadCredential()
        {
            await Task.Yield();
            var credential = Services.CredentialLocker.Retrieving();
            if (credential != null)
            {
                _tbUsername.Text = credential.UserName;
                _pbPassword.Password = credential.Password;
            }
            _tbUsername.IsEnabled = true;
            _pbPassword.IsEnabled = true;
        }

        private async void Login()
        {
            await Task.Yield();
            _btnLogin.IsEnabled = false;
            Services.CredentialLocker.Save(_tbUsername.Text, _pbPassword.Password);
            await WifiServices.Login(_tbUsername.Text, _pbPassword.Password);
            _btnLogin.IsEnabled = true;
        }

        private void _btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Logout();
        }
        private async void Logout()
        {
            _btnLogout.IsEnabled = false;
            await WifiServices.Logout();
            _btnLogout.IsEnabled = true;
        }

    }
}