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
using FlyExplorer.ControlElements;

namespace FlyExplorer.ControlElements
{
    /// <summary>
    /// Логика взаимодействия для FolderButton.xaml
    /// </summary>
    public partial class FolderButton : UserControl
    {
        private string textFolder;
        private string pathFolder;
        public string typeFolder;

        public string TextFolder { get => textFolder; set => textFolder = value; }
        public string PathFolder { get => pathFolder; set => pathFolder = value; }

        private bool elementSelectState = false;

        public FolderButton()
        {
            InitializeComponent();
           
        }

        private void NameFolder_Loaded(object sender, RoutedEventArgs e)
        {
            NameFolder.Text = TextFolder;

            ToolTip = textFolder;

            if (typeFolder == "folder") ImageFolder.Source = BitmapFrame.Create(new Uri(@"D:\C#Projects\FlyExplorer\FlyExplorer\ControlElements\Images\Folder.png"));
            if (typeFolder == "file") ImageFolder.Source = BitmapFrame.Create(new Uri(@"D:\C#Projects\FlyExplorer\FlyExplorer\ControlElements\Images\file-144.png"));
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if(!elementSelectState)
                Grid.Background = new SolidColorBrush(Colors.LightCyan);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if(!elementSelectState)
            {
                Grid.Background = new SolidColorBrush();
            }
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!elementSelectState)
            {
                Grid.Background = new SolidColorBrush(Colors.LightBlue);
                elementSelectState = true;
            }
            else if (elementSelectState)
            {
                Grid.Background = new SolidColorBrush(Colors.LightCyan);
                elementSelectState = false;
            }
        }
    }
}
