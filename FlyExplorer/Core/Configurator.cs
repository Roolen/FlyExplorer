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
        static void WriteRegistry(string keyName, object value)
        {
            try
            {
                RegistryKey keyForRegistry = Registry.CurrentUser;

                RegistryKey subKey = keyForRegistry.CreateSubKey("HKEY_CURRENT_USER\\Software\\FlyExplorer");

                subKey.SetValue(keyName, value);
            }
            catch (Exception e)
            {

                Log.Write($"{e} Writing registry {keyName}");
            }
        }

        static string ReadRegistry(string keyName)
        {
            RegistryKey keyForRegistry = Registry.CurrentUser;

            RegistryKey subKey = keyForRegistry.OpenSubKey("HKEY_CURRENT_USER\\Software\\FlyExplorer");

            if (subKey == null)
            {
                return null;
            }
            else
            {
                try
                {
                    return (string)subKey.GetValue(keyName);
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
