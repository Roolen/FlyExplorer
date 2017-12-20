using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyExplorer.BasicElements
{
    static class AnalyzerFileSystem
    {
        static List<LogicDisk> logicDisks = new List<LogicDisk>();
        static List<string> positions = new List<string>();


        static AnalyzerFileSystem()
        {

        }

        static public void TransformPosition(String newPosition)
        {

        }

        static public FileDirectory GetDataInPosition(SByte numberPosition)
        {
            return new FileDirectory(positions[numberPosition]);
        }

        static public LogicDisk GetLogicDisk(string nameDisk)
        {
            return new LogicDisk("t", "jfj");
        }

        static public void GetPosition(sbyte numberPosition)
        {

        }
    }
}
