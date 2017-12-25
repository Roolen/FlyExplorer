using System;
using System.IO;

namespace FlyExplorer.BasicElements
{
    public class FileDirectory
    {
        private string nameDirectory;
        private ulong size;
        private DirectoryInfo directory;


        public ulong Size
        {
            get { return size; }
        }

        public string NameDirectory
        {
            get { return nameDirectory; }
            set { nameDirectory = value; }
        }

        /// <summary>
        /// Создает экземпляр фойловой дериктории.
        /// </summary>
        /// <param name="path"></param>
        public FileDirectory(string path)
        {
            directory = new DirectoryInfo(path);
            NameDirectory = directory.Name;
        }

        public FileInfo[] GetFiles()
        {
            return directory.GetFiles();
        }
    }
}