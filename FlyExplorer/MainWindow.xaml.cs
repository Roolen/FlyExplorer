using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
using FlyExplorer.ControlElements;
using FlyExplorer.Core;
using System.IO;

namespace FlyExplorer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private sbyte currentNumberTab = 0;
        private List<TabItem> tabs = new List<TabItem>();
        #endregion


        public MainWindow()
        {
            InitializeComponent();

            #region Output for default tab
            NewTab();
            #endregion

            OutputTreeElement();

            #region Subscribe to delegates
            AnalyzerFileSystem.UpdateHandler += OutputtingDateForContentArea;

            #endregion

        }

        /// <summary>
        /// Выводит данные в область контента.
        /// </summary>
        private void OutputtingDateForContentArea(sbyte numberPosition)
        {
            OutputtingFoldersAndFilesForContentArea(numberPosition);
            OutputtingAddressLine(numberPosition);
        }

        /// <summary>
        /// Выводит папки и файлы, в виде кнопок, в область контенета, из указанной позиции.
        /// </summary>
        /// <param name="numberPosition">Позиция анализатора файловой системы</param>
        private void OutputtingFoldersAndFilesForContentArea(sbyte numberPosition)
        {
            ScrollViewer viewer = new ScrollViewer { Content = Presenter.GetPanelWithFoldersAndFilesForContentArea(numberPosition) };

            tabs[numberPosition].Content = viewer;
            tabs[numberPosition].Header = AnalyzerFileSystem.GetPosition(numberPosition);

        }

        private void OutputtingAddressLine(sbyte numberPosition)
        {
            string path = AnalyzerFileSystem.GetPosition(numberPosition);

            AdressLine.Children.Clear();

            foreach (ButtonAddressLine button in GetButtonsAddressLine(path, numberPosition))
            {
                AdressLine.Children.Add(button);
            }
        }

        private ButtonAddressLine[] GetButtonsAddressLine(string path, sbyte numberPosition)
        {
            string[] pathElements = path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            ButtonAddressLine[] buttons = new ButtonAddressLine[pathElements.Length - 1];

            for (int i = 0; i < pathElements.Length - 1; i++)
            {
                buttons[i] = new ButtonAddressLine(path, numberPosition);
                buttons[i].buttonAddressLine.Content = pathElements[i];
            }

            return buttons;
        }

        /// <summary>
        /// Создаёт новую вкладку и выводит в области контента, файлы и папки.
        /// </summary>
        private void NewTab()
        {
            AnalyzerFileSystem.CreateNewPosition("C:\\");

            TabItem tab = new TabItem { Header = AnalyzerFileSystem.GetPosition(currentNumberTab),
                                        Content = Presenter.GetPanelWithFoldersAndFilesForContentArea(currentNumberTab) };

            TabControl.Items.Insert(TabControl.Items.Count - 1, tab);
            tabs.Add(tab);

            OutputtingAddressLine(currentNumberTab);
            currentNumberTab++;
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

        /// <summary>
        /// Создает новую вкладку, при нажатии на кнопку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonForCreateNewTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NewTab();
        }

        /// <summary>
        /// Выделяет предыдущую вкладку, при фокусе на кнопке.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonForCreateNewTab_GotFocus(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedIndex = TabControl.SelectedIndex - 1;
        }
    }
}
