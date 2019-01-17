using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearching
{
    class Program
    {
        static void Main(string[] args)
        {
            string liness;
            StreamReader file = new StreamReader(@"YourLogFile.log");
            while ((liness = file.ReadLine()) != null)
            {
                if (liness.Contains("conditon1") || liness.Contains("conditon2") || liness.Contains("conditon3"))
                {
                    using (StreamWriter filewrite =
                    new StreamWriter(@"yourOutputfile.txt", true))
                    {
                        filewrite.WriteLine(liness);
                    }
                    Console.WriteLine(liness);
                }
            }
            file.Close();
            Console.ReadLine();
        }
    }
}
