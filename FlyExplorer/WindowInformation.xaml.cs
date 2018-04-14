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
using System.Windows.Shapes;
using FlyExplorer.BasicElements;

namespace FlyExplorer
{
    /// <summary>
    /// Логика взаимодействия для WindowInformation.xaml
    /// </summary>
    public partial class WindowInformation : Window
    {
        public WindowInformation()
        {
            InitializeComponent();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EditFileNameButton_Click(object sender, RoutedEventArgs e)
        {
            InfoName.Visibility = Visibility.Hidden;

            InfoRename.Visibility = Visibility.Visible;
            InfoRename.Text = InfoName.Text;

            OKRenameButton.Visibility = Visibility.Visible;
            EditFileNameButton.Visibility = Visibility.Hidden;
        }

        private void OKRenameButton_Click(object sender, RoutedEventArgs e)
        {
            InfoName.Visibility = Visibility.Visible;
            AnalyzerFileSystem.RenameFile(InfoPath.Text, InfoRename.Text);
            InfoName.Text = InfoRename.Text;

            InfoRename.Visibility = Visibility.Hidden;
            OKRenameButton.Visibility = Visibility.Hidden;
            EditFileNameButton.Visibility = Visibility.Visible;
        }
    }
}
