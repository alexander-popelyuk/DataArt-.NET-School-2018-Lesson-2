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
using System.IO;


namespace Task_1
{
    //
    // Summary:
    //   Main class containing entry point to the program.
    class Program
    {
        // Limit input deserialization file size.
        const long MaxInputSize = 5 * 1024 * 1024;
        // Default command line parameters.
        const string DefaultInputFile = "clientinfo_input.xml";
        const string DefaultOutputDirectory = ".\\Output";
        // Program generated output file parameters.
        const string ClinetInfoFileName = "clientinfo_output";
        const string WorkAddressFileName = "workaddress_output";
        const string HomeAddressFileName = "homeaddress_output";
        //
        // Summary:
        //   Entry point to the program.
        //   Process command line arguments and run application routine.
        static void Main(string[] args)
        {
            try
            {
                string InputFile = DefaultInputFile;
                string OutputDirectory = DefaultOutputDirectory;

                if (args.Length > 2)
                {
                    PrintError("Too many arguments.");
                    PrintUsage();
                }
                else if (args.Length == 1 && args[0] == "/?") PrintHelp();
                else
                {
                    if (args.Length > 0) InputFile = args[0];
                    if (args.Length > 1) OutputDirectory = args[1];
                    ConvertData(InputFile, OutputDirectory);
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
        //   Read file in old format deserialize, convert to new format and
        //   write output files to the output directory in the file system.
        // 
        // Parameters:
        //   input_file:
        //     Input file in old format.
        //
        //   output_directory:
        //     Directory to which generate program output.
        static void ConvertData(string input_file, string output_directory)
        {
            OldFormat.Client old_clinet;
            NewFormat.Client new_clinet;

            if (!File.Exists(input_file))
            {
                PrintError(string.Format("Input file '{0}' does not exist.", input_file));
                return;
            }
            if (new FileInfo(input_file).Length > MaxInputSize)
            {
                PrintError(string.Format("Input file size too big (max = {0}).", MaxInputSize));
                return;
            }

            Console.Write("Reading input file '{0}'...", input_file);
            old_clinet = Lesson_2.Convert.FromXml<OldFormat.Client>(input_file);
            Console.WriteLine("OK!");

            Console.Write("Converting to new format...");
            new_clinet = old_clinet;
            Console.WriteLine("OK!");

            Console.WriteLine("Generating output...");
            if (!Directory.Exists(output_directory)) Directory.CreateDirectory(output_directory);

            string file_name = Path.Combine(output_directory, ClinetInfoFileName + ".json");
            Console.Write("Writing '{0}'...", file_name);
            Lesson_2.Convert.ToJson(new_clinet, file_name);
            Console.WriteLine("OK!");

            file_name = Path.Combine(output_directory, ClinetInfoFileName + ".xml");
            Console.Write("Writing '{0}'...", file_name);
            Lesson_2.Convert.ToXml(new_clinet, file_name);
            Console.WriteLine("OK!");

            file_name = Path.Combine(output_directory, HomeAddressFileName + ".json");
            Console.Write("Writing '{0}'...", file_name);
            Lesson_2.Convert.ToJson(new_clinet.HomeAddress, file_name);
            Console.WriteLine("OK!");

            file_name = Path.Combine(output_directory, WorkAddressFileName + ".xml");
            Console.Write("Writing '{0}'...", file_name);
            Lesson_2.Convert.ToXml(new_clinet.WorkAddress, file_name);
            Console.WriteLine("OK!");
            Console.WriteLine("Successfully completed!");
        }
        //
        // Summary:
        //   Print program help text to standard output stream.
        static void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine("Converts old client information to new format.");
            Console.WriteLine("Additionally saves client work and home addresses");
            Console.WriteLine("in XML and JSON formats respectively.");
            PrintUsage();
        }
        //
        // Summary:
        //   Print program usage text to standard output stream.
        static void PrintUsage()
        {
            Console.WriteLine();
            Console.WriteLine("USAGE: Task_1.exe [ input [ output ]]");
            Console.WriteLine();
            Console.WriteLine("input\tInput file in old format (default: {0}).", DefaultInputFile);
            Console.WriteLine("output\tFolder for output files (default: {0}).", DefaultOutputDirectory);
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
