using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Multithreading_HW2
{
	class SimpleNumberChecker
	{
		public static HashSet<int> list = new HashSet<int>();
		private static object lockInstance = new object();

		public static void findPrimes(int minPoint, int maxPoint)
		{
			int primes;
			int count;
			Stopwatch sw = new Stopwatch();
			sw.Start();
			for (primes = minPoint; primes <= maxPoint; primes++)
			{
				count = 0;
				for (int i = 1; i <= primes; i++)
				{
					if (primes % i == 0)
						count++;
				}
				if (count <= 2)
				{
					Console.WriteLine(primes + " " + Thread.CurrentThread.Name);
					if (!list.Contains(primes))
					{
						lock (lockInstance)
						{
							list.Add(primes);
						}
					}
				}
				
			}

			Console.WriteLine(list);
			sw.Stop();
			Console.WriteLine($"Thread work: {sw.ElapsedMilliseconds}");
		}

		public static void run(int min, int max, int countOfThreads)
		{
			int numberInOneThread = max / countOfThreads;
			SimpleNumberChecker[] threads = new SimpleNumberChecker[countOfThreads];

			for (int i = 0; i < countOfThreads; i++)
			{
				new Thread(() =>
				{
					findPrimes(min, min+=numberInOneThread);
				}).Start();
			}
		}
	}
}