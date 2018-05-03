// MIT License
// 
// Copyright(c) 2018 Alexander Popelyuk
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.


using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace Task_2
{
    //
    // Summary:
    //   Main class containing entry point to the program.
    class Program
    {
        // Limit input deserialization file size.
        const long MaxInputSize = 5 * 1024 * 1024;
        // Default command line parameters.
        const string DefaultSamplesDirectory = ".\\operations";
        //
        // Summary:
        //   Entry point to the program.
        //   Process command line arguments and run application routine.
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
        //
        // Summary:
        //   Process files in specified directory and prints resulting statistics.
        //
        // Parameters:
        //   samples_directory:
        //     Directory containing sample files to process.
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
                Console.WriteLine("No valid files found!");
            }
        }
        //
        // Summary:
        //   Deserialize file to 'Operation' object using provided deserializer.
        //
        // Parameters:
        //   path:
        //     Path to file to deserialize.
        //
        //   operations:
        //     Operation list object to put converted operation.
        //
        //   deserialize:
        //     Deserialization delegate.
        //
        // Return:
        //     true if operation was successful; false otherwise.
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
        //
        // Summary:
        //   Print program help text to standard output stream.
        static void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine("Process operations data and print statistics.");
            PrintUsage();
        }
        //
        // Summary:
        //   Print program usage text to standard output stream.
        static void PrintUsage()
        {
            Console.WriteLine();
            Console.WriteLine("USAGE: Task_2.exe [ input ]");
            Console.WriteLine();
            Console.WriteLine("input\tInput folder with samples to process (default: {0}).", DefaultSamplesDirectory);
        }
        //
        // Summary:
        //   Print error message to standard error stream.
        //
        // Parameters:
        //   text:
        //     Error text to print.
        static void PrintError(string text)
        {
            Console.Error.WriteLine();
            Console.Error.WriteLine("ERROR: {0}", text);
        }
    }
}
