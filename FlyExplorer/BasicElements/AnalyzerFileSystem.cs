using System;
using System.Collections.Generic;
using System.IO;
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

        }

        static public void Start()
        {
            
        }

        static public void CreateNewPosition(string path)
        {
            positions.Add(path);
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
    }
}
