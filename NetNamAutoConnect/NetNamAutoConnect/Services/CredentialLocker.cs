﻿using System.Linq;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace NetNamAutoConnect.Services
{
    public static class CredentialLocker
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
            vault.Add(new PasswordCredential(
            RESOURCE_NAME, username, password));
        }

        public static PasswordCredential RetRetrieving()
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