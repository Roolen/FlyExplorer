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
using FlyExplorer.BasicElements;

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

        /// <summary>
        /// Устанавливает название папки, текст подсказки и изображение папки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            NameFolder.Text = TextFolder;

            ToolTip = textFolder;

            if (typeFolder == "folder") ImageFolder.Source = new BitmapImage(new Uri("Images/Folder.png", UriKind.Relative));
            if (typeFolder == "file") ImageFolder.Source = new BitmapImage(new Uri("Images/file-144.png", UriKind.Relative));
        }

        /// <summary>
        /// Выделяет форму цветом при наведении мыши.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            if(!elementSelectState)
                Grid.Background = new SolidColorBrush(Colors.LightCyan);
        }

        /// <summary>
        /// Убирает выделение формы цветом, когда мышь уводится.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if(!elementSelectState)
            {
                Grid.Background = new SolidColorBrush();
            }
        }

        /// <summary>
        /// Выделяет форму, при нажатии на ней левой клавишей мыши.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Анализатор файловой системы переходит в другую директорию, при двойном клике на папку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (typeFolder == "folder")
            {
                AnalyzerFileSystem.TransformPosition(0, PathFolder);
            }
        }

     
    }
}
