using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlyExplorer.ControlElements
{
    class CommandsWindow
    {
        public static RoutedCommand CloseTab { get; set; }

        static CommandsWindow()
        {
            CloseTab = new RoutedCommand("CloseTab", typeof(MainWindow));
        }
    }
}
