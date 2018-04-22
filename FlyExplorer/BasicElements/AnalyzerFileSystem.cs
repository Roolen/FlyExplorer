using System;
using System.Collections.Generic;
using System.IO;
using FlyExplorer.Core;
using System.Security.AccessControl;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Linq;

namespace FlyExplorer.BasicElements
{
    internal delegate void UpdateAnalyzer(sbyte numberPosition);

    /// <summary>
    /// Представляет модуль анализа файловой системы.
    /// </summary>
    static class AnalyzerFileSystem
    {
        /// <summary>
        /// Список логических дисков.
        /// </summary>
        private static List<LogicDisk> logicDisks = new List<LogicDisk>();
        /// <summary>
        /// Список позиций анализатора файловой системы.
        /// </summary>
        private static List<string> positions = new List<string>();
        /// <summary>
        /// Список массивов, дерикторий анализатора файловой системы.
        /// </summary>
        private static List<DirectoryInfo> directories = new List<DirectoryInfo>();

        /// <summary>
        /// Задает или получает логические диски.
        /// </summary>
        internal static List<LogicDisk> LogicDisks { get => logicDisks; set => logicDisks = value; }
        /// <summary>
        /// Возвращает или задает позиции анализатора файловой системы.
        /// </summary>
        public static List<string> Positions
        {
            get
            {
                if (positions != null) return positions;
                else return new List<string>();
            }
            set
            {
                if (value != null) positions = value;
            }
        }
        /// <summary>
        /// Возвращает или задает директории связанные с позициями анализатора файловой системы.
        /// </summary>
        public static List<DirectoryInfo> Directories
        {
            get
            {
                if (directories != null) return directories;
                else return new List<DirectoryInfo>();
            }
            set
            {
                if (value != null) directories = value;
            }
        }


        /// <summary>
        /// Событие вызывается при обновлении анализатора файловой системы.
        /// </summary>
        public static event UpdateAnalyzer UpdateHandler;



        static AnalyzerFileSystem()
        {
            Log.Write("Start AnalyzerFileSystem"); 
        }

        /// <summary>
        /// Обновляет данные анализатора и информирует об обновлении.
        /// </summary>
        /// <param name="numberPosition"></param>
        static public void Update(sbyte numberPosition)
        {
            Logging();

            UpdateHandler?.Invoke(numberPosition);
        }

        /// <summary>
        /// Выводит в лог данные о состояние анализатора файловой системы.
        /// </summary>
        private static void Logging()
        {
            for (int i = 0; i < Positions.Count; i++)
            {
                Log.Write($"AFS: position # {i} --- { Positions[i] }");

            }
        }

        /// <summary>
        /// Переименовывает указанный файл.
        /// </summary>
        /// <param name="pathFile">Путь к файлу</param>
        /// <param name="newName">Новое имя файла</param>
        public static void RenameFile(string pathFile, string newName)
        {
            string[] name = pathFile.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            name[name.Length - 1] = newName;
            string newPath = "";

            for (int i = 0; i < name.Length; i++)
            {
                newPath += name[i];
                if (i != name.Length - 1) newPath += "\\";
            } 

            FileCopyTo(pathFile, newPath);
            DeleteFile(pathFile);
        }

        public static void CopyFile(string oldPath, string newPath)
        {
            FileCopyTo(oldPath, newPath);
        }

        /// <summary>
        /// Создаёт новую позицию анализатора файловой системы.
        /// </summary>
        /// <param name="path">Путь новой позиции</param>
        public static void CreateNewPosition(string path)
        {
            if (path != null) Positions.Add(path);

            try
            {
                Directories.Add(new DirectoryInfo(Positions[Positions.Count - 1]));
            }
            catch (Exception e)
            {
                Presenter.CallWindowMessage("ERROR", e.Message);
            }

            Log.Write($"AFS: NewPosition # {Positions.Count - 1} --- { Positions[Positions.Count - 1] }");
        }

        /// <summary>
        /// Удаляют существующую позицию анализатора файловой системы.
        /// </summary>
        /// <param name="numberPosition">Индекс позиции которую следует удалить</param>
        static public void DeletePosition(int numberPosition)
        {
            if (Positions[numberPosition] != null && Directories[numberPosition] != null)
            {
                Positions.RemoveAt(numberPosition);
                Directories.RemoveAt(numberPosition);
            }
            else Presenter.CallWindowMessage("ERROR", $"{numberPosition} position is empty.");

            Log.Write($"AFS: DeletePosition # {numberPosition}");
        }

        public static void DeleteLastPosition()
        {
            if (Positions.Count != 0 && Directories.Count != 0)
            {
                Positions.RemoveAt(Positions.Count - 1);
                Directories.RemoveAt(Directories.Count - 1);

                Log.Write($"AFS: DeletePosition # {Positions.Count - 1}");
            }
            else Presenter.CallWindowMessage("ERROR", $"There isn't one position.");
        }

        /// <summary>
        /// Задать новый путь позиции анализатора файловой системы.
        /// </summary>
        /// <param name="oldPosition">Индекс позиции</param>
        /// <param name="newPath">Новый путь позиции</param>
        public static void TransformPosition(sbyte numberPosition,string newPath)
        {
            try
            {
                if (Positions[numberPosition] != null && Directories[numberPosition] != null)
                {
                    Positions[numberPosition] = newPath;
                    Directories[numberPosition] = new DirectoryInfo(Positions[numberPosition]);
                    Update(numberPosition);

                    Log.Write($"AFS: Position # {numberPosition} Transform path to {newPath}");
                }
                else Presenter.CallWindowMessage("ERROR", $"{numberPosition} position is empty.");
            }
            catch (Exception e)
            {
                Presenter.CallWindowMessage("ERROR", e.Message);
            }
        }

        public static void DeleteSpecifiedFileAndFolder(string pathFileOfFolder) => DeleteFile(pathFileOfFolder);

        /// <summary>
        /// Возвращает логический диск.
        /// </summary>
        /// <param name="nameDisk">Название логического диска</param>
        /// <returns>Логический диск</returns>
        static public DriveInfo GetLogicDisk(string nameDisk)
        {
            try
            {
            return new DriveInfo(nameDisk);
            }
            catch (ArgumentException e)
            {
                Presenter.CallWindowMessage("ERROR", e.Message);

                DriveInfo[] drives = DriveInfo.GetDrives();
                return new DriveInfo(drives[0].Name);
            }
        }

        /// <summary>
        /// Возвращает массив логических дисков.
        /// </summary>
        /// <returns>Массив дисков.</returns>
        public static DriveInfo[] GetAllLogicDisk()
        {
            List<DriveInfo> drives = new List<DriveInfo>(DriveInfo.GetDrives());

            for (int i = 0; i < drives.Capacity; i++)
            {
                if (!drives[i].IsReady)
                {
                    Log.Write($"AFS: drive {drives[i]} don't ready");

                    drives.Remove(drives[i]);
                }
            }

            return drives.ToArray();
        }

        /// <summary>
        /// Возвращает путь, указанной позиции анализатора файловой системы.
        /// </summary>
        /// <param name="numberPosition">Номер позиции</param>
        /// <returns>Путь</returns>
        public static string GetPosition(sbyte numberPosition)
        {
            if (Positions[numberPosition] != null)
            {
                return Positions[numberPosition];
            }
            else
            {
                Log.Write("AFS: don't positions, write attempt failed"); 
                return Positions[0];
            }
         }

        /// <summary>
        /// Возвращает массив файлов, из деректории, указанной позиции анализатора файловой системы.
        /// </summary>
        /// <param name="numberPosition">Номер позиции</param>
        /// <returns>Набор файлов</returns>
        public static FileInfo[] GetFilesFromPosition(int numberPosition)
        {
            if (Positions[numberPosition] != null)
            {
                return GetFiles(numberPosition);
            }
            else
            {
                Presenter.CallWindowMessage("Failed", $"{ numberPosition } positions is empty.");
                return GetFiles(0);
            }
        }

        /// <summary>
        /// Возвращает массив имен файлов, из деректории, указанной позиции анализатора файловой системы.
        /// </summary>
        /// <param name="numberPosition">Номер позиции</param>
        /// <returns>Масси имен файлов</returns>
        public static string[] GetFilesNameFromPosition(int numberPosition)
        {
            if (Positions[numberPosition] == null)
            {
                Presenter.CallWindowMessage("Failed", $"{numberPosition} position is empty.");
                return new string[0];
            }

            FileInfo[] files = GetFiles(numberPosition);

            string[] namesFiles = new string[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                namesFiles[i] = files[i].Name;
            }

            return namesFiles;
        }

        /// <summary>
        /// Возвращает массив имен директорий, из указанной позиции анализатора файловой системы.
        /// </summary>
        /// <param name="numberPosition">Номер позиции</param>
        /// <returns>Массив имен директорий</returns>
        public static string[] GetDirectoriesNameFromPosition(int numberPosition)
        {
            if (Positions[numberPosition] == null)
            {
                Presenter.CallWindowMessage("Failed", $"{numberPosition} position is empty.");
                return new string[0];
            }

            DirectoryInfo[] dirs = GetDirectories(numberPosition);

            string[] namesDirs = new string[dirs.Length];
            for (int i = 0; i < dirs.Length; i++)
            {
                namesDirs[i] = dirs[i].Name;
            }

            return namesDirs;
        }

        /// <summary>
        /// Возвращает массив размеров файлов, из деректории, указанной позиции анализатора файловой системы.
        /// </summary>
        /// <param name="numberPosition">Номер позиции</param>
        /// <returns>Массив размеров</returns>
        public static long[] GetFilesSizeFromPosition(int numberPosition)
        {
            if (Positions[numberPosition] == null)
            {
                Presenter.CallWindowMessage("Failed", $"{numberPosition} position is empty.");
                return new long[0];
            }

            FileInfo[] files = GetFiles(numberPosition);

            long[] sizeFiles = new long[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                sizeFiles[i] = files[i].Length;
            }

            return sizeFiles;
        }

        /// <summary>
        /// Возвращает массив "дат создания", из деректории, указанной позиции анализатора файловой системы.
        /// </summary>
        /// <param name="numberPosition">Номер позиции</param>
        /// <returns>Массив дат создания</returns>
        public static DateTime[] GetFilesCreationDateFromPosition(int numberPosition)
        {
            if (Positions[numberPosition] == null)
            {
                Presenter.CallWindowMessage("Failed", $"{numberPosition} position is empty.");
                return new DateTime[0];
            }

            FileInfo[] files = GetFiles(numberPosition);

            DateTime[] dateFiles = new DateTime[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                dateFiles[i] = files[i].CreationTime;
            }

            return dateFiles;
        }

        /// <summary>
        /// Возвращает массив "дат последнего изменения", из деректории, указанной позиции анализатора файловой системы.
        /// </summary>
        /// <param name="numberPosition">Номер позиции</param>
        /// <returns>Массив дат последнего изменения</returns>
        public static DateTime[] GetFilesLastWriteDateFromPosition(int numberPosition)
        {
            if (Positions[numberPosition] == null)
            {
                Presenter.CallWindowMessage("Failed", $"{numberPosition} position is empty.");
                return new DateTime[0];
            }

            FileInfo[] files = GetFiles(numberPosition);

            DateTime[] dateFiles = new DateTime[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                dateFiles[i] = files[i].LastWriteTime;
            }

            return dateFiles;
        }

        public static void CopyFileOrFolderToTheClipboard(string pathFileForCopy)
        {
            if(pathFileForCopy != null)
                CopyFileToClipboard(pathFileForCopy);
        }

        public static string GetPathFileOrFolderOfClipboard()
        {
            string textOfClipboard = GetTextOfClipboard();

            if (File.Exists(textOfClipboard) || Directory.Exists(textOfClipboard))
            {
                return textOfClipboard;
            }
            else
            {
                Presenter.CallWindowMessage("Empty", "The clipboard does not contain the required elements.");
                return null;
            }
        }

        private static FileInfo[] GetFiles(int numberPosition)
        {
            try
            {
                FileInfo[] files = Directories[numberPosition].GetFiles();

                return files;
            }
            catch (UnauthorizedAccessException)
            {
                Presenter.CallWindowMessage("Failed", "For this files don't access.");
                return null;
            }
            

            
        }

        private static string GetNameForFile(string pathTab)
        {
            string[] elementsPath = pathTab.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

            return elementsPath.Last();
        }

        private static DirectoryInfo[] GetDirectories(int numberPosition)
        {
            try
            {
                DirectoryInfo[] dir = Directories[numberPosition].GetDirectories();

                return dir;
            }
            catch (UnauthorizedAccessException)
            {
                Presenter.CallWindowMessage("No access", $"For this directory don't access.");

                TransformPosition((sbyte)numberPosition, "C:\\");
                return GetDirectories(numberPosition);
            }
            
        }

        private static void DeleteFile(string pathFile)
        {
            try
            {
                if (File.Exists(pathFile))
                {
                    FileInfo file = new FileInfo(pathFile);

                    file.Delete();
                }

                if (Directory.Exists(pathFile))
                {
                    DirectoryInfo directory = new DirectoryInfo(pathFile);

                    directory.Delete(true);
                }

                Log.Write($"AFS: deleted file {pathFile}");
            }
            catch (Exception e)
            {
                Log.Write($"AFS: {e.Message}");
                Presenter.CallWindowMessage("error", e.Message);
            }
            
        }

        /// <summary>
        /// Копирует файл или директорию.
        /// </summary>
        /// <param name="oldPath">Файл который требуется скопировать</param>
        /// <param name="newPath">Путь куда требуется скопировать</param>
        private static void FileCopyTo(string oldPath, string newPath)
        {
            if (File.Exists(oldPath))
            {
                if (newPath[newPath.Length - 1] != '\\')
                {
                    newPath = newPath + '\\';
                }

                FileInfo file = new FileInfo(oldPath);

                file.CopyTo(Path.Combine(newPath, GetNameForFile(oldPath)), true);
            }

            if (Directory.Exists(oldPath))
            {
                DirectoryInfo directory = new DirectoryInfo(oldPath);

                CopyDirectories(directory);
                
            }

            void CopyDirectories(DirectoryInfo directory)
            {
                foreach (var dir in directory.GetDirectories())
                {
                    foreach (var file in dir.GetFiles())
                    {
                        file.CopyTo($"{newPath}\\{dir.Name}");
                    }
                }

                foreach (var file in directory.GetFiles())
                {
                    file.CopyTo($"{newPath}\\{directory.Name}");
                }
            }
        }

        private static void CopyFileToClipboard(string pathFile)
        {
            if (File.Exists(pathFile) || Directory.Exists(pathFile))
            {
                Clipboard.SetText(pathFile);

                Log.Write($"AFS: The clipboard contains the file: {pathFile}");
            }
        }

        private static string GetTextOfClipboard()
        {
            if (Clipboard.ContainsText() == true)
                return Clipboard.GetText();

            return null;
        }

        public static void AddDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.AddAccessRule(new FileSystemAccessRule(Account, Rights, ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);

        }

        public static void RemoveDirectorySecurity(string FileName, string Account, FileSystemRights Rights, AccessControlType ControlType)
        {
            // Create a new DirectoryInfo object.
            DirectoryInfo dInfo = new DirectoryInfo(FileName);

            // Get a DirectorySecurity object that represents the 
            // current security settings.
            DirectorySecurity dSecurity = dInfo.GetAccessControl();

            // Add the FileSystemAccessRule to the security settings. 
            dSecurity.RemoveAccessRule(new FileSystemAccessRule(Account,
                                                            Rights,
                                                            ControlType));

            // Set the new access settings.
            dInfo.SetAccessControl(dSecurity);

        }
    }
}
