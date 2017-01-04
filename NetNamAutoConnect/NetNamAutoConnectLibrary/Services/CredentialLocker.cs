using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.Security.Credentials;

namespace NetNamAutoConnectLibrary.Services
{
    public class CredentialLocker
    {
        public const string RESOURCE_NAME = "NetNamAutoConnect";

        public static void Save(string username, string password)
        {
            var vault = new PasswordVault();
            try
            {
                // clear vault
                var storedCredentials = vault.FindAllByResource(RESOURCE_NAME);
                if (storedCredentials.Count > 0)
                {
                    foreach (var credential in storedCredentials)
                    {
                        vault.Remove(credential);
                    }
                }
            }
            catch { }
            try
            {
                vault.Add(new PasswordCredential(
                    RESOURCE_NAME, username, password));
            }
            catch { }
        }

        public static PasswordCredential Retrieving()
        {
            PasswordCredential credential = null;
            var vault = new PasswordVault();
            try
            {
                var storedCredentials = vault.FindAllByResource(RESOURCE_NAME);
                if (storedCredentials.Count > 0)
                {
                    credential = storedCredentials.Last();      //get the lastest
                    credential.RetrievePassword();
                }
            }
            catch { }
            return credential;
        }
    }
}