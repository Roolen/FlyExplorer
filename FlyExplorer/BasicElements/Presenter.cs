using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyExplorer.BasicElements;
using FlyExplorer.Core;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.IO;
using FlyExplorer.ControlElements;

namespace FlyExplorer.BasicElements
{
    public delegate void NewTab(string path);

    static class Presenter
    {
        /// <summary>
        /// Событие вызывается, когда требуется создание новой вкладки.
        /// </summary>
        static public event NewTab NewTabHandler;

        /// <summary>
        /// Возвращает массив элементов дерева, заполненый элементами дерева файловой системы.
        /// </summary>
        /// <returns>Массив элементов дерева</returns>
        static public TreeViewButton[] GetDirectorysTree()
        {
            return MakeTreeViewButtonsForLogicalDrives();
        }

        static public TreeViewButton[] GetFavoritesTree()
        {
            return MakeTreeViewButtonsForFavorites();
        }

        /// <summary>
        /// Возвращает панель заполненную файлами и папками по указанной позиции АФС.
        /// </summary>
        /// <param name="numberPosition">Позиция АФС</param>
        /// <returns>Панель заполненную элементами</returns>
        static public WrapPanel GetPanelWithFoldersAndFilesForContentArea(sbyte numberPosition)
        {
            string[] namesDirectories = AnalyzerFileSystem.GetDirectoriesNameFromPosition(numberPosition);
            string[] namesFiles = AnalyzerFileSystem.GetFilesNameFromPosition(numberPosition);

            WrapPanel panelWithFoldersAndFiles = new WrapPanel();

            #region Filling the panel with elements

            for (int i = 0; i < namesDirectories.Length; i++)
            {
                panelWithFoldersAndFiles.Children.Add(new FolderButton(numberPosition) { TextFolder = namesDirectories[i], typeFolder = "folder", PathFolder = $@"{AnalyzerFileSystem.GetPosition(numberPosition)}\{namesDirectories[i]}" });
            }

            for (int i = 0; i < namesFiles.Length; i++)
            {
                panelWithFoldersAndFiles.Children.Add(new FolderButton(numberPosition) { TextFolder = namesFiles[i], typeFolder = "file" });
            }
            #endregion

            return panelWithFoldersAndFiles;
        }

        /// <summary>
        /// Вызывает окно с сообщением.
        /// </summary>
        /// <param name="title">Заголовок окна</param>
        /// <param name="message">Текст сообщения</param>
        static public void CallWindowMessage(string title, string message)
        {
            WindowMessage winMessage = new WindowMessage(title, message);
            winMessage.ShowDialog();
        }

        static public Button[] GetButtonsForDriveSwitcher()
        {
            DriveInfo[] disks = AnalyzerFileSystem.GetAllLogicDisk();
            Button[] buttonItems = new Button[disks.Length];

            for (int i = 0; i < disks.Length; i++)
            {
                buttonItems[i] = new Button() { Content = disks[i].Name };
            }

            return buttonItems;
        }

        /// <summary>
        /// Возвращает текстовый блок с заданными в аргументах свойствами.
        /// </summary>
        /// <param name="text">Текст в текстовом блоке</param>
        /// <param name="fontSize">Размер шрифта</param>
        /// <param name="fontWeight">Тип шрифта</param>
        /// <returns>Текстовый блок</returns>
        static public TextBlock GetNewTextBox(string text, int fontSize, FontWeight fontWeight) => new TextBlock { Text = text, FontSize = fontSize, FontWeight = fontWeight };

        /// <summary>
        /// Возвращает массив элементов дерева, заполненый названиями логических дисков.
        /// </summary>
        /// <returns>Массив элементов дерева</returns>
        static private TreeViewButton[] MakeTreeViewButtonsForLogicalDrives()
        {
            DriveInfo[] disks = AnalyzerFileSystem.GetAllLogicDisk();

            TreeViewButton[] items = new TreeViewButton[disks.Length];

            for (int i = 0; i < disks.Length; i++)
            {
                items[i] = new TreeViewButton(disks[i].Name) { Width = 150 };
                items[i].ButtonForTreeView.Content = disks[i] + disks[i].VolumeLabel;
            }

            return items;
        }

        static private TreeViewButton[] MakeTreeViewButtonsForFavorites()
        {
            Dictionary<string, string> favorites = Configurator.GetDictionaryFavoritesValueRegistry();

            TreeViewButton[] items = new TreeViewButton[favorites.Count];

            int i = 0;
            foreach (var item in favorites)
            {
                items[i] = new TreeViewButton(item.Value);
                items[i].ButtonForTreeView.Content = item.Key;
                i++;
            }

            return items;
        }

        /// <summary>
        /// Вызывает событие создания новой вкладки.
        /// </summary>
        /// <param name="path">Путь новой вкладки</param>
        static public void SetNewTab(string path)
        {
            NewTabHandler?.Invoke(path);

            Log.Write($"A new tab is created along the path: {path}");
        }

    }
}
