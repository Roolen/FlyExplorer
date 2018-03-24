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

        /// <summary>
        /// Открывает окно лога.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilesElementTheLog_Copy_Click(object sender, RoutedEventArgs e)
        {
            WindowLog winLog = new WindowLog();
            winLog.Show();
        }

        /// <summary>
        /// Выводит все элементы дерева файловой системы.
        /// </summary>
        private void OutputTreeElement()
        {
            treeView.Items.Add(Presenter.GetNewTextBox("Favorites", 24, FontWeights.Bold));

            treeView.Items.Add(Presenter.GetNewTextBox("Computer", 24, FontWeights.Bold));

            OutputDrivesOnTreeView();

            treeView.Items.Add(Presenter.GetNewTextBox("Network", 24, FontWeights.Bold));
        }

        /// <summary>
        /// Выводит логические диски, в качестве элементов дерева файловой системы.
        /// </summary>
        private void OutputDrivesOnTreeView()
        {
            TreeViewItem[] itemsDriveSystem = Presenter.GetDirectorysTree();

            foreach (TreeViewItem driveItem in itemsDriveSystem)
            {
                treeView.Items.Add(driveItem);
            }
        }
    }
}
