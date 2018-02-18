using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyExplorer.Core
{
    static class Log
    {
        static public List<string> log = new List<string>();

        static Log()
        {
            log.Add("--Log initialize--");
        }

        static public void Write(string value)
        {
            log.Add(value);
        }

    }
}
