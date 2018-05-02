using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


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
            //Console.WriteLine("processing data... ok");
            if (!Directory.Exists(samples_directory))
            {
                PrintError(string.Format("Directory '{0}' does not exist.", samples_directory));
                return;
            }
            var sample_files = new DirectoryInfo(samples_directory).GetFiles();
            var operations = new List<Operation>();
            foreach (var file in sample_files)
            {
                if (file.Extension.ToLower() == ".xml")
                {
                    if (ProcessFile(file.FullName, operations, Lesson_2.Convert.FromXml<Operation>))
                        Console.WriteLine("{0} ok", file.Name);
                    else
                        Console.WriteLine("{0} fail", file.Name);
                }
                if (file.Extension.ToLower() == "json")
                {
                    if (ProcessFile(file.FullName, operations, Lesson_2.Convert.FromJson<Operation>))
                        Console.WriteLine("{0} ok", file.Name);
                    else
                        Console.WriteLine("{0} fail", file.Name);
                }
            }
            // check we found at leas one file
            if (operations.Count > 0)
            {
                Console.WriteLine(operations.Aggregate((max, next) => next.Amount > max.Amount ? next : max));
            }
            else
            {
                Console.WriteLine("nothing here!");
            }
        }

        static bool ProcessFile(string path, List<Operation> operations, Func<string, Operation> deserialize)
        {
            var file_info = new FileInfo(path);
            if (file_info.Length > MaxInputSize)
            {
                PrintError(string.Format("Input file size too big (max = {0}).", MaxInputSize));
                return false;
            }
            try
            {
                operations.Add(deserialize(path));
                return true;
            }
            catch (Exception ex)
            {
                PrintError(ex.Message); // sure ? maybe just 'failed!' toka
                return false;
            }
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
