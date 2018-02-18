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

            MainText.Text = "";

            AnalyzerFileSystem.CreateNewPosition("d://Mathematic");

            string[] namesFiles = AnalyzerFileSystem.GetFilesNameFromPosition(0);

            for (int i = 0; i < namesFiles.Length; i++)
            {
                MainText.Text += namesFiles[i];
                MainText.Text += "\n\n";
            }

        }

        private void ExitElement_Copy_Click(object sender, RoutedEventArgs e)
        {
            WindowLog winLog = new WindowLog();
            winLog.Show();
        }
    }
}
