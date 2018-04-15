using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyExplorer.BasicElements;
using FlyExplorer.Core;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using FlyExplorer.ControlElements;
using System.Runtime.InteropServices;
using Microsoft.Win32;

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
                panelWithFoldersAndFiles.Children.Add(new ContentElementButton(numberPosition)
                {
                    Text = namesDirectories[i], typeContentElement = TypeContentElement.folder,
                    PathContentElement = $@"{AnalyzerFileSystem.GetPosition(numberPosition)}\{namesDirectories[i]}"
                });
            }

            for (int i = 0; i < namesFiles.Length; i++)
            {
                panelWithFoldersAndFiles.Children.Add(new ContentElementButton(numberPosition)
                {
                    Text = namesFiles[i], typeContentElement = TypeContentElement.file,
                    PathContentElement = $@"{AnalyzerFileSystem.GetPosition(numberPosition)}\{namesFiles[i]}"
                });
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

        static public ComboBoxItem[] GetButtonsForDriveSwitcher()
        {
            DriveInfo[] disks = AnalyzerFileSystem.GetAllLogicDisk();
            ComboBoxItem[] buttonItems = new ComboBoxItem[disks.Length];

            for (int i = 0; i < disks.Length; i++)
            {
                buttonItems[i] = new ComboBoxItem() { Content = disks[i].Name };
            }

            return buttonItems;
        }

        /// <summary>
        /// Выводит окно информации о файле или папке.
        /// </summary>
        /// <param name="pathFile">Путь файла или папки</param>
        /// <param name="type">Тип</param>
        public static void OpenWindowInformationOfFile(string pathFile, TypeContentElement type)
        {
            CreateNewWindowInformation(pathFile, type)?.Show();
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
        /// Создает новое окно свойств и заполняет его данными.
        /// </summary>
        /// <param name="pathFile">Путь файла</param>
        /// <param name="type">Тип файла</param>
        /// <returns></returns>
        private static WindowInformation CreateNewWindowInformation(string pathFile, TypeContentElement type)
        {
            WindowInformation window = new WindowInformation();

            if (type == TypeContentElement.folder)
            {
                DirectoryInfo directory = new DirectoryInfo(pathFile);
                Stack<long> infoDirectory = CalculateWeightOfFolder(pathFile);

                window.Icon.Source = new BitmapImage(new Uri("ControlElements/Images/FolderV3.png", UriKind.Relative));

                window.InfoName.Text = directory.Name;
                window.InfoTypeFile.Text = "Folder";
                window.InfoDescription.Text = "No description";
                window.InfoPath.Text = directory.FullName;
                window.InfoSize.Text = $"{FormatFileSize(infoDirectory.Pop())} ({infoDirectory.Pop()} files; {infoDirectory.Pop()} folders;)";
                window.InfoCreate.Text = directory.CreationTimeUtc.ToString();
                window.InfoChange.Text = directory.LastWriteTimeUtc.ToString();
            }
            if (type == TypeContentElement.file)
            {
                FileInfo file = new FileInfo(pathFile);

                window.Icon.Source = GetIconOfFile(pathFile, file.Extension);

                window.InfoName.Text = file.Name;
                window.InfoTypeFile.Text = FormatTypeFile(file.Extension);
                window.InfoDescription.Text = "No description";
                window.InfoPath.Text = file.FullName;
                window.InfoSize.Text = FormatFileSize(file.Length);
                window.InfoCreate.Text = file.CreationTimeUtc.ToString();
                window.InfoChange.Text = file.LastWriteTimeUtc.ToString();
            }

            return window;
        }

        /// <summary>
        /// Возвращает строку с отформатированным размером.        
        /// </summary>
        /// <param name="length">Размер файла</param>
        /// <returns>Отформатированная строка</returns>
        private static string FormatFileSize(long length)
        {
            string formattedLength;

            if (length > Math.Pow(2, 40))
            {
                formattedLength = $"{length / Math.Pow(2, 40):f2} TiB";
            }
            else if (length > Math.Pow(2, 30))
            {
                formattedLength = $"{length / Math.Pow(2, 30):f2} GiB";
            }
            else if (length > Math.Pow(2, 20))
            {
                formattedLength = $"{length / Math.Pow(2, 20):f2} MiB";
            }
            else if (length > 1024)
            {
                formattedLength = $"{length / 1024f:f2} KiB";
            }
            else
            {
                formattedLength = $"{length} B";
            }

            return formattedLength;
        }

        /// <summary>
        /// Возвращает строку с типом файла, если он есть в базе, если нет, то возвращает исходное расширение.
        /// </summary>
        /// <param name="extention">Расширение файла</param>
        /// <returns></returns>
        private static string FormatTypeFile(string extention)
        {
            Dictionary<string, string> fileAssociations = new Dictionary<string, string>
            {
                {".exe", "Application" }
            };

            if (fileAssociations.ContainsKey(extention))
            {
                return fileAssociations[extention] + $" ({extention})";
            }
            else
            {
                return extention;
            }
        }

        private static BitmapImage GetIconOfFile(string pathFile, string extentionFile)
        {
            FileIconCollection col = FileIconCollection.GetSystemFileIcons();
            List<FileIcon> ff = col.FileIcons;
            string pt = Path.GetTempFileName();

            if (File.Exists(pathFile))
            {
                foreach (var item in ff)
                {
                    if (item.FileExtension == extentionFile)
                    {
                        if (item.Icon != null)
                        {
                            item.Icon.ToBitmap().Save(pt);
                        }
                        else
                        {
                            Bitmap bmp = default(Bitmap);
                            bmp = new Bitmap(Icon.ExtractAssociatedIcon(pathFile).ToBitmap());
                            bmp.Save(pt);
                        }
                    }
                }

                return new BitmapImage(new Uri(pt));
            }
            else
            {
                return new BitmapImage(new Uri(@"ControlElements\Images\FolderV3.png", UriKind.Relative));
            }
        }

        /// <summary>
        /// Возвращает стек чисел, где первое число - размер папки, второе число - кол-во файлов в папке, третье число - кол-во папок в папке.
        /// </summary>
        /// <param name="pathDirectory">Директория для которой нужно расчитать вес</param>
        /// <returns>Стек чисел</returns>
        private static Stack<long> CalculateWeightOfFolder(string pathDirectory)
        {
            DirectoryInfo directory = new DirectoryInfo(pathDirectory);
            Stack<long> information = new Stack<long>();

            int counterNumberFolders = 0;
            int counterNumberFiles = 0;
            Stack<long> sum = new Stack<long>();

            Summer(directory);

            void Summer(DirectoryInfo dirMain)
            {
                try
                {
                    foreach (var file in dirMain.GetFiles())
                    {
                        sum.Push(file.Length);
                        counterNumberFiles++;
                    }

                    foreach (var dir in dirMain.GetDirectories())
                    {
                        Summer(dir);
                        counterNumberFolders++;
                    }
                }
                catch (Exception e)
                {
                    Presenter.CallWindowMessage("error", e.Message);
                }
                
            }

            information.Push(counterNumberFolders);
            information.Push(counterNumberFiles);
            information.Push(sum.Sum());

            return information;
        }

    }

    public class FileIcon
    {
        public Icon Icon { get; set; }
        public string FileExtension { get; set; }

        public override string ToString()
        {
            return FileExtension;
        }
    }

    public class FileIconCollection
    {


        // WinAPI function 
        [DllImport("shell32.dll", EntryPoint = "ExtractIconA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern IntPtr ExtractIcon(int hInst, string lpszExeFileName, int nIconIndex);


        public List<FileIcon> FileIcons { get; private set; }


        private FileIconCollection() { }


        private static Icon GetIcon(string iconPath)
        {
            int strIndex = iconPath.IndexOf(",");
            string iconFileName = (strIndex > 0) ? iconPath.Substring(0, strIndex) : iconPath;
            int iconFileIndex;


            int.TryParse(iconPath.Substring(strIndex + 1), out iconFileIndex);


            // Grab icon handle 
            IntPtr hIcon = ExtractIcon(0, iconFileName, iconFileIndex);


            return (hIcon != IntPtr.Zero) ? Icon.FromHandle(hIcon) : null;


        }


        public static FileIconCollection GetSystemFileIcons()
        {


            List<FileIcon> list = new List<FileIcon>();


            RegistryKey extRoot = Registry.ClassesRoot;


            foreach (string key in extRoot.GetSubKeyNames())
            {
                // skip if it is non file extension  key 
                if (String.IsNullOrEmpty(key) || (key.IndexOf(".") != 0)) continue;


                RegistryKey extKey = extRoot.OpenSubKey(key);


                // skip if no such element 
                if ((extKey == null) || (extKey.GetValue("") == null)) continue;


                // get sub key 
                string iconKey = String.Format("{0}\\DefaultIcon", extKey.GetValue(""));


                RegistryKey extIcon = extRoot.OpenSubKey(iconKey);


                // skip if no such element 
                if ((extIcon == null) || (extIcon.GetValue("") == null)) continue;


                FileIcon fi = new FileIcon { FileExtension = key, Icon = GetIcon(extIcon.GetValue("").ToString()) };
                extIcon.Close();
                list.Add(fi);
            }


            extRoot.Close();


            return new FileIconCollection { FileIcons = list };
        }
    }
}
