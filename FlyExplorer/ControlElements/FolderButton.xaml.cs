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

namespace FlyExplorer.ControlElements
{
    /// <summary>
    /// Логика взаимодействия для FolderButton.xaml
    /// </summary>
    public partial class FolderButton : UserControl
    {
        private string textFolder;
        public string TextFolder { get => textFolder; set => textFolder = value; }

        public FolderButton()
        {
            InitializeComponent();

           
        }

        private void NameFolder_Loaded(object sender, RoutedEventArgs e)
        {
            NameFolder.Text = TextFolder;
        }
    }
}
