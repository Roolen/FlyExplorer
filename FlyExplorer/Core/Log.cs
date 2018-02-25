using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyExplorer.Core
{
    static class Log
    {
        /// <summary>
        /// Список элементов лога.
        /// </summary>
        static public List<string> log = new List<string>();

        static Log()
        {
            log.Add($"--Log initialize--");
        }

        /// <summary>
        /// Создаёт новую запись в логе.
        /// </summary>
        /// <param name="value">Встраиваемая запись</param>
        static public void Write(string value)
        {
            log.Add(value);
        }

    }
}
