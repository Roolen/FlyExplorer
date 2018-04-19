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
        private static readonly string pathForFavorites = "Software\\FlyExplorer\\Favorites";

        /// <summary>
        /// Содержит путь для раздела реестра содержащего избранные директории.
        /// </summary>
        public static string PathForFavorites { get => pathForFavorites; }

        static Configurator()
        {
            CreateSubKeyForFavorites();
        }

        /// <summary>
        /// Возвращает словарь<name, path> для избранных директорий.
        /// </summary>
        /// <returns>Словарь избранных директорий</returns>
        public static Dictionary<string, string> GetDictionaryFavoritesValueRegistry()
        {
            return ReadRegistry(PathForFavorites);
        }

        /// <summary>
        /// Сохраняет в реестр избранные директории.
        /// </summary>
        /// <param name="newElements">Словарь новых элементов для сохранения</param>
        public static void SetFavoritesValueRegistry(Dictionary<string, string> newElements)
        {
            WriteRegistry(PathForFavorites, newElements);
        }

        /// <summary>
        /// Удаляет избранные директории из реестра.
        /// </summary>
        /// <param name="keyName">Имена директорий для удаления</param>
        public static void DeleteKeyFromFavorites(string keyName)
        {
            DeleteRegistryKey(keyName, PathForFavorites);
        }

        /// <summary>
        /// Проверяет создан ли подраздел реестра для избранных директорий, если нет, то создаёт.
        /// </summary>
        private static void CreateSubKeyForFavorites()
        {
            RegistryKey keyForRegistry = Registry.CurrentUser;

            if (keyForRegistry.OpenSubKey(PathForFavorites) == null)
            {
                keyForRegistry.CreateSubKey(PathForFavorites);
            }
            keyForRegistry.Close();
        }

        /// <summary>
        /// Записывает новые значения в раздел реестра.
        /// </summary>
        /// <param name="keyName">Имя подраздела</param>
        /// <param name="favotitesItems">Славарь со значениями</param>
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

                keyForRegistry.Close();
                subKey.Close();
            }
            catch (Exception e)
            {

                Log.Write($"{e} Writing registry {keyName}");
            }
        }

        /// <summary>
        /// Читает раздел реестра и возвращает словарь значений.
        /// </summary>
        /// <param name="keyName">Подраздел</param>
        /// <returns>Словарь значений</returns>
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
                        registryItems.Add(keys[i], (string)subKey.GetValue(keys[i]));
                    }
                    keyForRegistry.Close();
                    subKey.Close();

                    return registryItems;
                }
                catch (Exception e)
                {

                    Log.Write($"{e} Reading registry {keyName}");
                    return null;
                }
            }

            
        }

        /// <summary>
        /// Удаляет значения из раздела реестра.
        /// </summary>
        /// <param name="keyName">Подраздел</param>
        /// <param name="nameValue">Название значения</param>
        private static void DeleteRegistryKey(string keyName, string nameValue)
        {
            try
            {
                RegistryKey keyForRegistry = Registry.CurrentUser;

                RegistryKey subKey = keyForRegistry.CreateSubKey(keyName);

                if (subKey != null)
                {
                    subKey.DeleteValue(keyName);
                }

                keyForRegistry.Close();
                subKey.Close();
            }
            catch (Exception e)
            {
                Log.Write($"{e} Deleting SubKey {nameValue}");
            }
        }
    }
}
