using System;
using System.Collections.Generic;
using System.IO;
using FlyExplorer.Core;
using System.Threading.Tasks;

namespace FlyExplorer.BasicElements
{
    static class AnalyzerFileSystem
    {
        static List<LogicDisk> logicDisks = new List<LogicDisk>();
        static List<string> positions = new List<string>();
        static List<DirectoryInfo> directorys = new List<DirectoryInfo>();
        static List<FileInfo[]> files = new List<FileInfo[]>();



        static AnalyzerFileSystem()
        {
            Log.Write("Start AnalyzerFileSystem");
        }

        static public void Start()
        {
            
        }

        static public void Update()
        {
            
        }

        /// <summary>
        /// Создаёт новую позицию анализатора файловой системы.
        /// </summary>
        /// <param name="path">Путь новой позиции</param>
        static public void CreateNewPosition(string path)
        {
            positions.Add(path);
            directorys.Add( new DirectoryInfo( positions[positions.Count - 1] ) );
        }

        /// <summary>
        /// Удаляют существующую позицию анализатора файловой системы.
        /// </summary>
        /// <param name="numberPosition">Индекс позиции которую следует удалить</param>
        static public void DeletePosition(int numberPosition)
        {
            positions.RemoveAt(numberPosition);
            directorys.RemoveAt(numberPosition);
        }

        /// <summary>
        /// Задать новый путь позиции анализатора файловой системы.
        /// </summary>
        /// <param name="oldPosition">Индекс позиции</param>
        /// <param name="newPosition">Новый путь позиции</param>
        static public void TransformPosition(int numberPosition,string newPosition)
        {
            positions[numberPosition] = newPosition;
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
    }
}
