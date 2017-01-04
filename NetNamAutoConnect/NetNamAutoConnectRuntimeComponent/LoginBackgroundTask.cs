using NetNamAutoConnectLibrary;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace NetNamAutoConnectRuntimeComponent
{
    public sealed class LoginBackgroundTask : IBackgroundTask
    {
        private BackgroundTaskDeferral _deferral; // Note: defined at class scope so we can mark it complete inside the OnCancel() callback if we choose to support cancellation

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            _deferral = taskInstance.GetDeferral();
            await Task.Yield();

            var credential = CredentialLocker.Retrieving();

            await WifiServices.Login(credential.UserName, credential.Password);

            _deferral.Complete();
        }
    }
}