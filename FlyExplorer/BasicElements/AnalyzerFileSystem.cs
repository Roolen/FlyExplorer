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
        


        static AnalyzerFileSystem()
        {
            Log.Write("Start AnalyzerFileSystem");
        }

        static public void Start()
        {
            
        }

        static public void CreateNewPosition(string path)
        {
            positions.Add(path);
            directorys.Add( new DirectoryInfo( positions[positions.Count - 1] ) );
        }

        static public void TransformPosition(int oldPosition,string newPosition)
        {
            positions[oldPosition] = newPosition;
        }

        static public FileDirectory GetDataInPosition(SByte numberPosition)
        {
            return new FileDirectory(positions[numberPosition]);
        }

        static public LogicDisk GetLogicDisk(string nameDisk)
        {
            return new LogicDisk(nameDisk);
        }

        static public string GetPosition(sbyte numberPosition)
        {
            return positions[numberPosition];
        }

        static public FileInfo[] GetFilesFromPosition(int numberPosition)
        {
            return directorys[numberPosition].GetFiles();
        }

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
