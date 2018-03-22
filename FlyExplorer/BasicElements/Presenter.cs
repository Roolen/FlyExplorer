using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyExplorer.BasicElements;
using FlyExplorer.Core;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;

namespace FlyExplorer.BasicElements
{
    static class Presenter
    {
        static public TreeViewItem GetDirectorysTree()
        {
           
            TreeViewItem tree = new TreeViewItem() { Header = "Computer",
                                                     FontFamily = new FontFamily("Segoe UI"),
                                                     Foreground = new SolidColorBrush(Color.FromArgb(255, 130, 130, 237)) };  // Associate with configurator.

            TreeViewItem[] items = GetMenuItemsFromFiles("D:/ITVDN");
            

            for (int i = 0; i < items.Length; i++)
            {
                tree.Items.Add(items[i]);
            }

            return tree;
        }

        static private MenuItem[] GetMenuItemsLogicDisks()
        {
            DriveInfo[] disks = AnalyzerFileSystem.GetAllLogicDisk();

            MenuItem[] items = new MenuItem[disks.Length];

            for (int i = 0; i < disks.Length; i++)
            {
                items[i] = new MenuItem { Header = disks[i], Width = 150, FontSize = 14 };  //Associate with configurator.
            }

            return items;
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
            }


            return items.ToArray();
        }
    }
}
