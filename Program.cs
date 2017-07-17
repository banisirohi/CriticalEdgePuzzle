using System;
using CritiCalEdgePuzzle;
using System.Collections.Generic;
using log4net;
using System.Reflection;

namespace CritiCalEdgePuzzle
{
	class MainClass
	{
		private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		public static void Main(string[] args)
		{
			log.Info("Logging started!");

			NodeProcess process = new NodeProcess();
			List<string> output;
			int ip1_size = 0;
			ip1_size = Convert.ToInt32(Console.ReadLine());
			string[] ip1 = new string[ip1_size];
			string ip1_item;
			for (int ip1_i = 0; ip1_i < ip1_size; ip1_i++)
			{
				ip1_item = Console.ReadLine();
				ip1[ip1_i] = ip1_item;
			}

			for (int i = 0; i < ip1_size; i++)
			{
				output = process.findCriticalBridges(ip1[i]);
				Console.WriteLine("Critical bridges for input: " + (i+1));
				foreach (var edge in output)
				{
					Console.WriteLine(edge);
				}
			}
			Console.ReadKey();
		}
	}
}
