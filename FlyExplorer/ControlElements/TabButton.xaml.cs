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
    /// Логика взаимодействия для TabButton.xaml
    /// </summary>
    public partial class TabButton : UserControl
    {
        DeleteTab methodDeleteTab;

        public TabButton(string header, DeleteTab methodDeleteTab)
        {
            InitializeComponent();

            TextBlockInTabButton.Text = header;
            this.methodDeleteTab = methodDeleteTab;
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            methodDeleteTab?.Invoke();
        }
    }
}
