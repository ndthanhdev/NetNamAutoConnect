using Microsoft.Toolkit.Uwp;
using NetNamAutoConnect.ViewModels.Base;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace NetNamAutoConnect.ViewModels
{
    public partial class MainPageViewModel : BindableBase
    {
        public DelegateCommand SwitchStateCommand => new DelegateCommand(async () =>
        {
            await Task.Yield();
            try
            {
                IsStarting = true;
                IsStarted = true;
                /// Get state
                var currentState = IsLoginBackgroundTaskRegistered();
                if (currentState)
                {
                    //unregister
                    await UnregisterLoginBackgroundTask();
                }
                else
                {
                    //register
                    await RegisterLoginBackgroundTask();
                }
                IsStarted = IsLoginBackgroundTaskRegistered();
            }
            finally
            {
                IsStarting = false;
            }
        }, () => !IsStarting);

        private bool IsLoginBackgroundTaskRegistered()
        {
            return BackgroundTaskHelper.IsBackgroundTaskRegistered(typeof(NetNamAutoConnectRuntimeComponent.LoginBackgroundTask));
        }

        private async Task RegisterLoginBackgroundTask()
        {
            await Task.Yield();
            try
            {
                BackgroundTaskHelper.Register(typeof(NetNamAutoConnectRuntimeComponent.LoginBackgroundTask),
    new SystemTrigger(SystemTriggerType.NetworkStateChange, false));
            }
            catch (Exception) { }
        }

        private async Task UnregisterLoginBackgroundTask()
        {
            await Task.Yield();
            BackgroundTaskHelper.Unregister(typeof(NetNamAutoConnectRuntimeComponent.LoginBackgroundTask));
        }
    }
}