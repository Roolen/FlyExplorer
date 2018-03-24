using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyExplorer.BasicElements;
using FlyExplorer.Core;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.IO;

namespace FlyExplorer.BasicElements
{
    static class Presenter
    {
        static public TreeViewItem[] GetDirectorysTree()
        {
            List<TreeViewItem> items = new List<TreeViewItem>();

            TreeViewItem[] disks = GetTreeViewItemsLogicDisks();

            for (int i = 0; i < disks.Length; i++)
            {
                items.Add(disks[i]);
            }

            return items.ToArray();
        }

        static private TreeViewItem[] GetTreeViewItemsLogicDisks()
        {
            DriveInfo[] disks = AnalyzerFileSystem.GetAllLogicDisk();

            TreeViewItem[] items = new TreeViewItem[disks.Length];

            for (int i = 0; i < disks.Length; i++)
            {
                if (disks[i].IsReady)
                {
                    items[i] = new TreeViewItem { Header = disks[i] + disks[i].VolumeLabel, Width = 150, };  //Associate with configurator.
                }
                Log.Write($"Presentor: disk {disks[i]} don't ready");
            }

            return items;
        }

        static public TextBlock GetNewTextBox(string text, int fontSize, FontWeight fontWeight)
        {
            TextBlock textBlock = new TextBlock { Text = text, FontSize = fontSize, FontWeight = fontWeight };
            return textBlock;
        }

        static private TreeViewItem[] GetMenuItemsFromFiles(string root)
        {
            Stack<string> dirs = new Stack<string>();
            List<TreeViewItem> items = new List<TreeViewItem>();

            if (!Directory.Exists(root))
            {
                Log.Write($"Presenter: ArgumentException (this directory {root} isn't correct");
                throw new ArgumentException();
            }
            else { dirs.Push(root); }

            int current = 0;
            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                string[] subDirs;

                try
                {
                    subDirs = Directory.GetDirectories(currentDir);
                }
                catch (UnauthorizedAccessException e)
                {
                    Log.Write($"Presenter: {e.Message} ");
                    continue;
                }
                catch (DirectoryNotFoundException e)
                {
                    Log.Write($"Presenter: {e.Message} ");
                    continue;
                }

                foreach (string file in subDirs)
                {
                    try
                    {
                        //FileInfo fi = new FileInfo(file);
                        items.Add(new TreeViewItem { Header = file, FontSize = 10 });
                        //Log.Write($"Presenter: File: {fi.Name}");
                    }
                    catch (FileNotFoundException e)
                    {
                        Log.Write($"Presenter: {e.Message}");
                        continue;
                    }
                }

                foreach (string str in subDirs)
                {
                    dirs.Push(str);
                }
                current++;
            }


            return items.ToArray();
        }
    }
}
