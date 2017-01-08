using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;

namespace NetNamAutoConnectLibrary
{
    public class Localization
    {
        public static string GetLocalString(string key)
        {
            var loader = new ResourceLoader();
            return loader.GetString(key);
        }
    }
}
