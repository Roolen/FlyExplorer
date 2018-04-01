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
    /// Логика взаимодействия для ButtonAddressLine.xaml
    /// </summary>
    public partial class ButtonAddressLine : UserControl
    {
        private string pathButton;
        private sbyte numberPosition;

        public ButtonAddressLine(string[] path, sbyte numberPosition)
        {
            InitializeComponent();

            for (int i = 0; i < path.Length; i++)
            {
                if(i != path.Length - 1)
                    pathButton += path[i] += "\\";
            }
            this.numberPosition = numberPosition;
        }

        private void buttonAddressLine_Click(object sender, RoutedEventArgs e)
        {
            AnalyzerFileSystem.TransformPosition(numberPosition, pathButton);
        }
    }
}
