using System;
using System.Diagnostics;
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
    public enum TypeContentElement
    {
        folder,
        file
    }


    /// <summary>
    /// Логика взаимодействия для FolderButton.xaml
    /// </summary>
    public partial class ContentElementButton : UserControl
    {
        private string textContentElement;
        private string pathContentElement;
        public TypeContentElement typeContentElement;
        private sbyte positionContentElement;

        public string Text
        {
            get => textContentElement;

            set
            {
                if (value != null) textContentElement = value;
                else if (value == null) textContentElement = "";
            }
        }

        public string PathContentElement
        {
            get => pathContentElement;
            set
            {
                if (value != null) pathContentElement = value;
                else if (value == null) pathContentElement = "";
            }
        }

        private bool elementSelectState = false;

        public ContentElementButton(sbyte position)
        {
            InitializeComponent();

            positionContentElement = position;
        }

        /// <summary>
        /// Открывает файл принадлежащий ContentElement.
        /// </summary>
        private void OpenFileContentElement()
        {
            try
            {
                if (typeContentElement == TypeContentElement.folder && pathContentElement != null)
                {
                    AnalyzerFileSystem.TransformPosition(positionContentElement, PathContentElement);
                }
                else if (typeContentElement == TypeContentElement.file && pathContentElement != null)
                {
                    Process.Start(PathContentElement);
                }
            }
            catch (Exception e)
            {
                Presenter.CallWindowMessage("ERROR", e.Message);
            }

        }

        /// <summary>
        /// Устанавливает название папки, текст подсказки и изображение папки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            NameFolder.Text = Text;

            ToolTip = Text;

            if (typeContentElement == TypeContentElement.folder)
            {
                ContextMenuOpenFile.Header = "Open folder";
                ImageFolder.Source = new BitmapImage(new Uri("Images/FolderV3.png", UriKind.Relative));
            }

            if (typeContentElement == TypeContentElement.file)
            {
                ContextMenuOpenFile.Header = "Open file";
                ImageFolder.Source = new BitmapImage(new Uri("Images/file.png", UriKind.Relative));
            }
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
            OpenFileContentElement();
        }

        /// <summary>
        /// Открывает файл, при нажатии на элемент контекстного меню. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileContentElement();
        }

        private void ContextMenuProperties_Click(object sender, RoutedEventArgs e)
        {
            Presenter.OpenWindowInformationOfFile();
        }
    }
}
