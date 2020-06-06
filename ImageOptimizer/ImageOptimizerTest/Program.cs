using System;
using System.Collections.Generic;
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
			compressor compressor = new compressor();
			Console.WriteLine(DateTime.Now);
			compressor.CompressImage(@"PathToImages",90);
			Console.ReadLine();
		}
	}
}
