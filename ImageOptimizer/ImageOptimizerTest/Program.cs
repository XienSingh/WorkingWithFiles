using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageOptimizerLogic;
namespace ImageOptimizerTest
{
	class Program
	{
		static void Main(string[] args)
		{
			Stopwatch stopWatch = new Stopwatch();
			stopWatch.Start();
			Compressor.CompressImage(@"PathToImages", 100);
			stopWatch.Stop();
			TimeSpan ts = stopWatch.Elapsed;
			string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
			ts.Hours, ts.Minutes, ts.Seconds,
			ts.Milliseconds / 10);
			Console.WriteLine("RunTime " + elapsedTime);
			Console.ReadLine();
		}
	}
}
