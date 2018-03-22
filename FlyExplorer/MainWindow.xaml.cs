﻿using System;
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
            TextBlock textFavorites = new TextBlock { Text = "Favorites", FontSize = 24, FontWeight = FontWeights.Bold };
            treeView.Items.Add(textFavorites);

            TextBlock textComputer = new TextBlock { Text = "Computer", FontSize = 24, FontWeight = FontWeights.Bold };
            treeView.Items.Add(textComputer);

            TreeViewItem[] items = Presenter.GetDirectorysTree();

            for (int i = 0; i < items.Length; i++)
            {
                treeView.Items.Add(items[i]);
            }
        }
    }
}
