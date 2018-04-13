using System;
using System.Collections.Generic;
using System.IO;
using FlyExplorer.Core;
using System.Security.AccessControl;
using System.Windows.Controls;
using System.Threading.Tasks;

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
            Positions[numberPosition] = newPath;
            Directories[numberPosition] = new DirectoryInfo(Positions[numberPosition]);
            Update(numberPosition);

            Log.Write($"AFS: Position # {numberPosition} Transform path to {newPath}");
        }

        /// <summary>
        /// Возвращает логический диск.
        /// </summary>
        /// <param name="nameDisk">Название логического диска</param>
        /// <returns>Логический диск</returns>
        static public DriveInfo GetLogicDisk(string nameDisk)
        {
            return new DriveInfo(nameDisk);
        }

        /// <summary>
        /// Возвращает массив логических дисков.
        /// </summary>
        /// <returns>Массив дисков.</returns>
        static public DriveInfo[] GetAllLogicDisk()
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
        static public string GetPosition(sbyte numberPosition)
        {
            if (Positions.Count != 0)
            {
            return Positions[numberPosition];
            }
            else
            {
                Log.Write("AFS: don't positions, write attempt failed"); 
                return "";
            }
         }

        /// <summary>
        /// Возвращает массив файлов, из деректории, указанной позиции анализатора файловой системы.
        /// </summary>
        /// <param name="numberPosition">Номер позиции</param>
        /// <returns>Набор файлов</returns>
        static public FileInfo[] GetFilesFromPosition(int numberPosition)
        {
            return GetFiles(numberPosition);
        }

        /// <summary>
        /// Возвращает массив имен файлов, из деректории, указанной позиции анализатора файловой системы.
        /// </summary>
        /// <param name="numberPosition">Номер позиции</param>
        /// <returns>Масси имен файлов</returns>
        static public string[] GetFilesNameFromPosition(int numberPosition)
        {
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
        static public string[] GetDirectoriesNameFromPosition(int numberPosition)
        {
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
        static public long[] GetFilesSizeFromPosition(int numberPosition)
        {
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
        static public DateTime[] GetFilesCreationDateFromPosition(int numberPosition)
        {
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
        static public DateTime[] GetFilesLastWriteDateFromPosition(int numberPosition)
        {
            FileInfo[] files = GetFiles(numberPosition);

            DateTime[] dateFiles = new DateTime[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                dateFiles[i] = files[i].LastWriteTime;
            }

            return dateFiles;
        }

        private static FileInfo[] GetFiles(int numberPosition)
        {
            try
            {
                FileInfo[] files = Directories[numberPosition].GetFiles();

                return files;
            }
            catch (UnauthorizedAccessException e)
            {
                return null;
            }
            

            
        }

        private static DirectoryInfo[] GetDirectories(int numberPosition)
        {
            try
            {
                DirectoryInfo[] dir = Directories[numberPosition].GetDirectories();

                return dir;
            }
            catch (UnauthorizedAccessException e)
            {
                Presenter.CallWindowMessage("No access", $"For this directory don't access.");

                TransformPosition((sbyte)numberPosition, "C:\\");
                return GetDirectories(numberPosition);
            }
            
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
