using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Notifications;

namespace NetNamAutoConnectLibrary
{
    public static class ToastServices
    {
        public static async Task PopToast(ToastContent content)
        {
            await Task.Yield();
            var xml = content.GetXml();
            ToastNotificationManager.CreateToastNotifier().Show(new ToastNotification(xml));
        }

        public static ToastContent GenerateLoggedInToastContent()
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
                                Text = Localization.GetLocalString("Logged in")
                            },
                        }
                    }
                }
            };
        }

        public static ToastContent GenerateLoggedInFailToastContent()
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
                                Text = Localization.GetLocalString("Login fail")
                            },
                        }
                    }
                }
            };
        }
    }
}
