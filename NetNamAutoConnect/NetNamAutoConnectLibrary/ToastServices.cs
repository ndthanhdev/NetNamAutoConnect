using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace NetNamAutoConnectLibrary
{
    public static class ToastServices
    {
        public static async Task PopLoggedInToast()
        {
            await Task.Yield();
            var content = InnerGenerateLoggedInToastContent();
            var xml = content.GetXml();
            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(xml));
        }

        private static ToastContent InnerGenerateLoggedInToastContent()
        {
            return new ToastContent()
            {
                Scenario = ToastScenario.Default,
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = "NetNamAutoConnect"
                            },
                            new AdaptiveText()
                            {
                                Text = "NetNamAutoConnect sent login request."
                            },
                        }
                    }
                }
            };
        }
    }
}
