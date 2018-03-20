using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyExplorer.BasicElements;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;

namespace FlyExplorer.BasicElements
{
    static class Presenter
    {
        static public TreeViewItem GetDirectorysTree()
        {
           
            TreeViewItem tree = new TreeViewItem() { Header = "Computer", FontFamily = new FontFamily("Segoe UI"), Foreground = new SolidColorBrush(Color.FromArgb(255, 130, 130, 237)) };  // Associate with configurator.

            MenuItem[] items = GetMenuItemsLogicDisks();

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
    }
}
