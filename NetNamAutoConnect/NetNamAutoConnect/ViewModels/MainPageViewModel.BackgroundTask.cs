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
                await Task.Delay(2000);/// TODO: Remove this line
                if (currentState)
                {
                    //unregister
                    await UnregisterLoginBackgroundTask();
                }
                else
                {
                    //register
                    await RegisterLoginBackgroundTask();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                    InnerLogin();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
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
            BackgroundTaskHelper.Register(typeof(NetNamAutoConnectRuntimeComponent.LoginBackgroundTask), new SystemTrigger(SystemTriggerType.NetworkStateChange, false));
        }

        private async Task UnregisterLoginBackgroundTask()
        {
            await Task.Yield();
            BackgroundTaskHelper.Unregister(typeof(NetNamAutoConnectRuntimeComponent.LoginBackgroundTask));
        }
    }
}