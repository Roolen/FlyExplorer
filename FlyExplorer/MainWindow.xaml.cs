﻿using System;
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
        #endregion


        public MainWindow()
        {
            InitializeComponent();

            NewTab();

            AnalyzerFileSystem.CreateNewPosition("d://Downloads");

            OutputtingDateForContentArea();

            AnalyzerFileSystem.Update();
            OutputTreeElement();

            AnalyzerFileSystem.UpdateHandler += OutputtingDateForContentArea;

        }

        /// <summary>
        /// Выводит данные в область контента.
        /// </summary>
        private void OutputtingDateForContentArea()
        {
            if (ContentArea != null) ContentArea.Children.Clear();
            OuptuttingFoldersAndFilesForContentArea(0); 
        }

        /// <summary>
        /// Выводит папки и файлы, в виде кнопок, в область контенета, из указанной позиции.
        /// </summary>
        /// <param name="numberPosition">Позиция анализатора файловой системы</param>
        private void OuptuttingFoldersAndFilesForContentArea(sbyte numberPosition)
        {
            ContentArea.Children.Add(Presenter.GetPanelWithFoldersAndFilesForContentArea(numberPosition));
            TabControl.SelectedIndex = 0;
        }

        private void NewTab()
        {
            TabItem tab = new TabItem();
            TabControl.Items.Insert(TabControl.Items.Count - 1, tab);
            if (currentNumberTab != 0) currentNumberTab++;
            AnalyzerFileSystem.CreateNewPosition("C://");
            tab.Header = AnalyzerFileSystem.GetPosition(currentNumberTab);
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
    }
}
