using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Multithreading_HW1
{
    class MultithreadingIssues
    {
        private static JournalData data = new JournalData();

        public static void Run()
        {
            data.CreateWatchmen("Patric", true);
            data.CreateWatchmen("Frank", false);
            data.CreateWatchmen("Garry", false);
            var readThread = new Thread(ReadFromJournal);
            var writeThread = new Thread(WriteToBank);
            
            writeThread.Start();
            readThread.Start();
            readThread.Join();
            writeThread.Join();
        }

        private static void ReadFromJournal()
        {
            for (int i = 0; i < 1000; i++)
            {
                Thread.Sleep(100);
                if ((data.IsWatch("Patric")==false) && (data.IsWatch("Frank") == false) && (data.IsWatch("Garry") == false))
                    Console.WriteLine("Race condition & atomic issue: nobody's watch!");
                else Console.WriteLine(data.IsWatch("Patric") ? "Patric is watch!" : data.IsWatch("Frank")? "Frank is watch!" : "Garry is watch!");
            }
        }

        private static void WriteToBank()
        {
            for (int i = 0; i < 1000; i++)
            {
                data.ChangeWatchmen("Patric", "Frank");
                data.ChangeWatchmen("Frank", "Garry");
                data.ChangeWatchmen("Garry", "Patric");
            }
        }
    }
}
