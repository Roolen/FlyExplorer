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
using System.Threading;
using FlyExplorer.Core;

namespace FlyExplorer
{
    /// <summary>
    /// Логика взаимодействия для WindowLog.xaml
    /// </summary>
    public partial class WindowLog : Window
    {
        public WindowLog()
        {
            InitializeComponent();

            TextBoxLog.Text = "";

            ShowLog();

        }

        private void ShowLog()
        {
            //todo : Переписать реализацию с использованием более оптимизированных типов.
            StringBuilder text = new StringBuilder(300000);
            for (int i = 0; i < Log.log.Count; i++)
            {
                text.Append(Log.log[i]);
                text.AppendLine();
            }
            TextBoxLog.Text = text.ToString();
        }
    }
}
