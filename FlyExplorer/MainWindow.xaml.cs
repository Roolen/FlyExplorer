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

            AnalyzerFileSystem.CreateNewPosition("d:\\Mathematic");
            
            FileDirectory dir = new FileDirectory("d:\\Mathematic");
            FileInfo[] files = new FileInfo[dir.GetFiles().Length];
            int cur = 0;
            foreach (FileInfo str in dir.GetFiles())
            {
                files[cur] = str;
                cur++;
            }

            for (int i = 0; i < files.Length; i++)
            {
                MainText.Text += files[i].Name;
                MainText.Text += "\n";
            }
            
        }
    }
}
