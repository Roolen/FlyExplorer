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
        static public TreeViewItem GetMenuItemDirectorysTree()
        {
            DriveInfo[] disks = AnalyzerFileSystem.GetAllLogicDisk();

            TreeViewItem tree = new TreeViewItem() { Header = "Computer", FontFamily = new FontFamily("Segoe UI"), Foreground = new SolidColorBrush(Color.FromArgb(255, 130, 130, 237)) };


            for (int i = 0; i < disks.Length; i++)
            {
                tree.Items.Add(new MenuItem { Header = disks[i], Width = 150, FontSize = 14 });
            }

            return new TreeViewItem();
        }
    }
}
