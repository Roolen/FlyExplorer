using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace FlyExplorer.Core
{
    static class Configurator
    {
        private static string pathForFavorites = "Software\\FlyExplorer\\Favorites";

        public static string PathForFavorites { get => pathForFavorites; set => pathForFavorites = value; }

        public static Dictionary<string, string> GetDictionaryValueRegistry()
        {
            return ReadRegistry(pathForFavorites);
        }

        public static void SetValueRegistry(Dictionary<string, string> newElements)
        {
            WriteRegistry(PathForFavorites, newElements);
        }

        private static void WriteRegistry(string keyName, Dictionary<string, string> favotitesItems)
        {
            try
            {
                RegistryKey keyForRegistry = Registry.CurrentUser;

                RegistryKey subKey = keyForRegistry.CreateSubKey(keyName);

                foreach (var item in favotitesItems)
                {
                    subKey.SetValue(item.Key, item.Value);
                }
            }
            catch (Exception e)
            {

                Log.Write($"{e} Writing registry {keyName}");
            }
        }

        private static Dictionary<string, string> ReadRegistry(string keyName)
        {
            RegistryKey keyForRegistry = Registry.CurrentUser;

            RegistryKey subKey = keyForRegistry.OpenSubKey(keyName);

            if (subKey == null)
            {
                return null;
            }
            else
            {
                try
                {
                    string[] keys = subKey.GetValueNames();
                    Dictionary<string, string> registryItems = new Dictionary<string, string>();

                    for (int i = 0; i < keys.Length; i++)
                    {
                        registryItems.Add(keys[i] ,(string)subKey.GetValue(keys[i]));
                    }
                    return registryItems;
                }
                catch (Exception e)
                {

                    Log.Write($"{e} Reading registry {keyName}");
                    return null;
                }
            }
        }
    }
}
