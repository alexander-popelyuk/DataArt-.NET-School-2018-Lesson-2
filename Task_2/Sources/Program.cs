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
                string samples_directory = DefaultSamplesDirectory;
                
                if (args.Length > 1)
                {
                    PrintError("Too many arguments.");
                    PrintUsage();
                }
                else if (args.Length == 1 && args[0] == "/?") PrintHelp();
                else
                {
                    if (args.Length > 0) samples_directory = args[0];
                    ProcessData(samples_directory);
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
            if (!Directory.Exists(samples_directory))
            {
                PrintError(string.Format("Directory '{0}' does not exist.", samples_directory));
                return;
            }
            Console.WriteLine("Looking up in '{0}'...", samples_directory);
            var sample_files = Directory.GetFiles(samples_directory);
            var operations = new List<Operation>();
            int failed_count = 0;
            foreach (var file_path in sample_files)
            {
                var file_extension = Path.GetExtension(file_path).ToLower();

                if (file_extension == ".xml")
                {
                    if (!ProcessFile(file_path, operations, Lesson_2.Convert.FromXml<Operation>)) failed_count++;
                }
                if (file_extension == ".json")
                {
                    if (!ProcessFile(file_path, operations, Lesson_2.Convert.FromJson<Operation>)) failed_count++;
                }
            }
            Console.WriteLine("{0} files processed, {1} success, {2} failed", operations.Count + failed_count, operations.Count, failed_count);
            // check we done at least one successfull conversion
            if (operations.Count > 0)
            {
                Console.WriteLine("Max operation parameters:");
                Console.WriteLine(operations.Aggregate((max, next) => next.Amount > max.Amount ? next : max));
            }
            else
            {
                Console.WriteLine("Valid file not found!");
            }
        }

        static bool ProcessFile(string path, List<Operation> operations, Func<string, Operation> deserialize)
        {
            Console.Write("Processing '{0}'...", path);

            if (new FileInfo(path).Length > MaxInputSize)
            {
                Console.WriteLine("SKIP! (File size too big, max = {0}!).", MaxInputSize);
                return false;
            }
            try
            {
                operations.Add(deserialize(path));
                Console.WriteLine("OK!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("FAIL! ({0}", ex.Message);
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
