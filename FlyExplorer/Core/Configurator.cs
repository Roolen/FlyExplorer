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

        static Configurator()
        {
            CreateSubKeyForFavorites();
        }

        public static Dictionary<string, string> GetDictionaryValueRegistry()
        {
            return ReadRegistry(PathForFavorites);
        }

        public static void SetValueRegistry(Dictionary<string, string> newElements)
        {
            WriteRegistry(PathForFavorites, newElements);
        }

        public static void DeleteKeyFromFavorites(string keyName)
        {
            DeleteRegistryKey(keyName, PathForFavorites);
        }

        private static void CreateSubKeyForFavorites()
        {
            RegistryKey keyForRegistry = Registry.CurrentUser;

            if (keyForRegistry.OpenSubKey(PathForFavorites) == null)
            {
                keyForRegistry.CreateSubKey(PathForFavorites);
            }
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

        private static void DeleteRegistryKey(string keyName, string pathRegistry)
        {
            try
            {
                RegistryKey keyForRegistry = Registry.CurrentUser;

                RegistryKey subKey = keyForRegistry.CreateSubKey(pathRegistry);

                if (subKey != null)
                {
                    subKey.DeleteValue(keyName);
                }
            }
            catch (Exception e)
            {
                Log.Write($"{e} Deleting SubKey {pathRegistry}");
            }
        }
    }
}
