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

        /// <summary>
        /// Инициализирует новый экземпляр класса ButtonAddressLine.
        /// </summary>
        /// <param name="path">Путь на который указывает кнопка</param>
        /// <param name="numberPosition">Номер вкладки к которой принадлежит кнопка</param>
        /// <param name="numberButton">Степень глубины кнопки</param>
        public ButtonAddressLine(string[] path, sbyte numberPosition, int numberButton)
        {
            InitializeComponent();

            SetPathOfButton(path, numberButton);

            this.numberPosition = numberPosition;
        }

        /// <summary>
        /// Задаёт путь кнопки.
        /// </summary>
        /// <param name="path">Путь</param>
        /// <param name="numberButton">Номер кнопки</param>
        private void SetPathOfButton(string[] path, int numberButton)
        {
            for (int i = 0; i < path.Length; i++)
            {
                if (i < path.Length - numberButton)
                {
                    pathButton += path[i] += "\\";
                }
            }
        }



        /// <summary>
        /// Меняет позицию анализатора файловой системы при нажатии на кнопку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonAddressLine_Click(object sender, RoutedEventArgs e)
        {
            AnalyzerFileSystem.TransformPosition(numberPosition, pathButton);
        }
    }
}
