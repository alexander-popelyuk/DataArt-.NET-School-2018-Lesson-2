using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    class Program
    {
        const long MaxInputSize = 5 * 1024 * 1024;
        const string DefaultSamplesDirectory = ".\\operations";

        static void Main(string[] args)
        {
            try
            {
                string SamplesDirectory = DefaultSamplesDirectory;

                if (args.Length > 1)
                {
                    PrintError("Too many arguments.");
                    PrintUsage();
                }
                else if (args.Length == 1 && args[0] == "/?") PrintHelp();
                else
                {
                    if (args.Length > 0) SamplesDirectory = args[0];
                    ProcessData(SamplesDirectory);
                }
            }
            catch (Exception ex)
            {
                PrintError(ex.Message);
            }
            #if DEBUG
                Console.ReadKey(true);
            #endif
        }

        static void ProcessData(string samples_directory)
        {
            Console.WriteLine("processing data... ok");
        }

        static void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine("Process operations data and print statistics.");
            PrintUsage();
        }

        static void PrintUsage()
        {
            Console.WriteLine();
            Console.WriteLine("USAGE: Task_2.exe [ input ]");
            Console.WriteLine();
            Console.WriteLine("input\tInput folder with samples to process (default: {0}).", DefaultSamplesDirectory);
        }

        static void PrintError(string text)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: {0}", text);
        }
    }
}
