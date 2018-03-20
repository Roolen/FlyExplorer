using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FlyExplorer.BasicElements;
using FlyExplorer.Core;
using System.IO;

namespace FlyExplorer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            NewTab();

            AnalyzerFileSystem.CreateNewPosition("d://Mathematic");

            string[] namesFiles = AnalyzerFileSystem.GetFilesNameFromPosition(0);

                for (int i = 0; i < namesFiles.Length; i++)
                {
                    ContentArea.Children.Add(new Button { Content = namesFiles[i] });
                }

            AnalyzerFileSystem.Update();
            OutputTreeElement();

        }

        private void NewTab()
        {
            TabItem tab = new TabItem();
            TabControl.Items.Add(tab);
            tab.Header = "NewTab";
        }

        private void ExitElement_Copy_Click(object sender, RoutedEventArgs e)
        {
            WindowLog winLog = new WindowLog();
            winLog.Show();
        }

        private void OutputTreeElement()
        {
            string[] disks = Environment.GetLogicalDrives();
            TreeViewItem tree = new TreeViewItem() { Header = "Computer", FontFamily = new FontFamily("Segoe UI"), Foreground = new SolidColorBrush(Color.FromArgb(255, 130, 130, 237)) };

            for (int i = 0; i < disks.Length; i++)
            {
                tree.Items.Add(new MenuItem { Header = disks[i], Width = 150, FontSize = 14 });
            }
                treeView.Items.Add(tree);
        }
    }
}
