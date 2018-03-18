using System;
using System.Collections.Generic;
using System.IO;
using FlyExplorer.Core;
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
        static List<DirectoryInfo> directorys = new List<DirectoryInfo>();
        /// <summary>
        /// Список массивов, файлов анализатора файловой системы.
        /// </summary>
        static List<FileInfo[]> files = new List<FileInfo[]>();



        static AnalyzerFileSystem()
        {
            Log.Write("Start AnalyzerFileSystem");
        }

        static public void Update()
        {
            Logging();

            DriveInfo[] drive = DriveInfo.GetDrives();
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
            directorys.Add( new DirectoryInfo( positions[positions.Count - 1] ) );

            Log.Write($"AFS: NewPosition # {positions.Count - 1} --- { positions[positions.Count - 1] }");
        }

        /// <summary>
        /// Удаляют существующую позицию анализатора файловой системы.
        /// </summary>
        /// <param name="numberPosition">Индекс позиции которую следует удалить</param>
        static public void DeletePosition(int numberPosition)
        {
            positions.RemoveAt(numberPosition);
            directorys.RemoveAt(numberPosition);

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

            Log.Write($"AFS: Position # {numberPosition} Transform path to {newPath}");
        }

        /// <summary>
        /// Возвращает логический диск.
        /// </summary>
        /// <param name="nameDisk">Название логического диска</param>
        /// <returns>Логический диск</returns>
        static public LogicDisk GetLogicDisk(string nameDisk)
        {
            return new LogicDisk(nameDisk);
        }

        /// <summary>
        /// Возвращает массив логических дисков.
        /// </summary>
        /// <returns>Массив дисков.</returns>
        static public LogicDisk[] GetAllLogicDisk()
        {
            return logicDisks.ToArray();
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
            return directorys[numberPosition].GetFiles();
        }

        /// <summary>
        /// Возвращает массив имен файлов, из деректории, указанной позиции анализатора файловой системы.
        /// </summary>
        /// <param name="numberPosition">Номер позиции</param>
        /// <returns>Масси имен файлов</returns>
        static public string[] GetFilesNameFromPosition(int numberPosition)
        {
            FileInfo[] files = directorys[numberPosition].GetFiles();
            
            string[] namesFiles = new string[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                namesFiles[i] = files[i].Name;
            }

            return namesFiles;
            
        }

        /// <summary>
        /// Возвращает массив размеров файлов, из деректории, указанной позиции анализатора файловой системы.
        /// </summary>
        /// <param name="numberPosition">Номер позиции</param>
        /// <returns>Массив размеров</returns>
        static public long[] GetFilesSizeFromPosition(int numberPosition)
        {
            FileInfo[] files = directorys[numberPosition].GetFiles();

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
            FileInfo[] files = directorys[numberPosition].GetFiles();

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
            FileInfo[] files = directorys[numberPosition].GetFiles();

            DateTime[] dateFiles = new DateTime[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                dateFiles[i] = files[i].LastWriteTime;
            }

            return dateFiles;
        }
    }
}
