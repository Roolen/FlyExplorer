using System;
using System.Collections.Generic;
using System.IO;
using FlyExplorer.Core;
using System.Windows.Controls;
using System.Threading.Tasks;

namespace FlyExplorer.BasicElements
{
    static class AnalyzerFileSystem
    {
        /// <summary>
        /// Список логических дисков.
        /// </summary>
        static List<LogicDisk> logicDisks = new List<LogicDisk>();
        /// <summary>
        /// Список позиций анализатора файловой системы.
        /// </summary>
        static List<string> positions = new List<string>();
        /// <summary>
        /// Список массивов, дерикторий анализатора файловой системы.
        /// </summary>
        static List<DirectoryInfo> directories = new List<DirectoryInfo>();
        /// <summary>
        /// Список массивов, файлов анализатора файловой системы.
        /// </summary>
        static List<FileInfo[]> files = new List<FileInfo[]>();

        public delegate void UpdateAnalyzer();
        static public event UpdateAnalyzer UpdateHandler;



        static AnalyzerFileSystem()
        {
            Log.Write("Start AnalyzerFileSystem");
        }

        static public void Update()
        {
            Logging();

            if(UpdateHandler != null) UpdateHandler();
        }

        /// <summary>
        /// Выводит в лог данные о состояние анализатора файловой системы.
        /// </summary>
        static private void Logging()
        {
            for (int i = 0; i < positions.Count; i++)
            {
                Log.Write($"AFS: position # {i} --- { positions[i] }");

            }
        }

        /// <summary>
        /// Создаёт новую позицию анализатора файловой системы.
        /// </summary>
        /// <param name="path">Путь новой позиции</param>
        static public void CreateNewPosition(string path)
        {
            positions.Add(path);
            directories.Add( new DirectoryInfo( positions[positions.Count - 1] ) );

            Log.Write($"AFS: NewPosition # {positions.Count - 1} --- { positions[positions.Count - 1] }");
        }

        /// <summary>
        /// Удаляют существующую позицию анализатора файловой системы.
        /// </summary>
        /// <param name="numberPosition">Индекс позиции которую следует удалить</param>
        static public void DeletePosition(int numberPosition)
        {
            positions.RemoveAt(numberPosition);
            directories.RemoveAt(numberPosition);

            Log.Write($"AFS: DeletePosition # {numberPosition}");
        }

        /// <summary>
        /// Задать новый путь позиции анализатора файловой системы.
        /// </summary>
        /// <param name="oldPosition">Индекс позиции</param>
        /// <param name="newPath">Новый путь позиции</param>
        static public void TransformPosition(int numberPosition,string newPath)
        {
            positions[numberPosition] = newPath;
            directories[numberPosition] = new DirectoryInfo(positions[positions.Count - 1]);
            Update();

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
            return positions[numberPosition];
        }

        /// <summary>
        /// Возвращает массив файлов, из деректории, указанной позиции анализатора файловой системы.
        /// </summary>
        /// <param name="numberPosition">Номер позиции</param>
        /// <returns>Набор файлов</returns>
        static public FileInfo[] GetFilesFromPosition(int numberPosition)
        {
            return directories[numberPosition].GetFiles();
        }

        /// <summary>
        /// Возвращает массив имен файлов, из деректории, указанной позиции анализатора файловой системы.
        /// </summary>
        /// <param name="numberPosition">Номер позиции</param>
        /// <returns>Масси имен файлов</returns>
        static public string[] GetFilesNameFromPosition(int numberPosition)
        {
            FileInfo[] files = directories[numberPosition].GetFiles();
            
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
            DirectoryInfo[] dirs = directories[numberPosition].GetDirectories();

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
            FileInfo[] files = directories[numberPosition].GetFiles();

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
            FileInfo[] files = directories[numberPosition].GetFiles();

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
            FileInfo[] files = directories[numberPosition].GetFiles();

            DateTime[] dateFiles = new DateTime[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                dateFiles[i] = files[i].LastWriteTime;
            }

            return dateFiles;
        }
    }
}
