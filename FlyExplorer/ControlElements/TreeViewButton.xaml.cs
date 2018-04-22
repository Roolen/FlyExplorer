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

namespace FlyExplorer.ControlElements
{
    /// <summary>
    /// Логика взаимодействия для TreeViewButton.xaml
    /// </summary>
    public partial class TreeViewButton : UserControl
    {
        private string pathButton;

        /// <summary>
        /// Инициализирует новый класс TreeViewButton.
        /// </summary>
        /// <param name="path">Путь на который указывает кнопка</param>
        public TreeViewButton(string path)
        {
            InitializeComponent();

            pathButton = path;

            ButtonForTreeView.FontFamily = new FontFamily("Berlin Sans FB");
            ButtonForTreeView.Foreground = new SolidColorBrush(Colors.CornflowerBlue);
        }

        /// <summary>
        /// Создаёт новую вкладку при нажатии на кнопку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonForTreeView_Click(object sender, RoutedEventArgs e)
        {
            Presenter.SetNewTab(pathButton);
        }
    }
}
