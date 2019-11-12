using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Multithreading_HW1
{
    class JournalData
    {
       private Dictionary<string, bool> watchmens = new Dictionary<string, bool>();

        public void CreateWatchmen(string name, bool isWatch)
        {
            watchmens.Add(name, isWatch);
        }

        public void ChangeWatchmen(string from, string to)
        {
            watchmens[from] = false;
            Thread.Sleep(100);
            Console.WriteLine($"Watchmen {to} changed {from}");
            watchmens[to] = true;
            Thread.Sleep(100);
        }

        public bool IsWatch(string name)
        {
            return watchmens[name];
        }
    }
}