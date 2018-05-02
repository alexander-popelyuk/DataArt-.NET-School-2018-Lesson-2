using System;
using System.IO;


namespace Task_1
{
    class Program
    {
        const string DefaultInputFile = "clientinfo_input.xml";
        const long MaxInputSize = 5 * 1024 * 1024;
        const string DefaultOutputDirectory = ".\\Output";
        const string ClinetInfoFileName = "clientinfo_output";
        const string WorkAddressFileName = "workaddress_output";
        const string HomeAddressFileName = "homeaddress_output";

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

        static void ConvertData(string input_file, string output_directory)
        {
            OldFormat.Client old_clinet;
            NewFormat.Client new_clinet;
            var file_info = new FileInfo(input_file);
            if (!file_info.Exists)
            {
                PrintError(string.Format("Input file '{0}' does not exist.", input_file));
                return;
            }
            if (file_info.Length > MaxInputSize)
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

        static void PrintHelp()
        {
            Console.WriteLine();
            Console.WriteLine("Converts old client information to new format.");
            Console.WriteLine("Additionally saves client work and home addresses");
            Console.WriteLine("in XML and JSON formats respectively.");
            PrintUsage();
        }

        static void PrintUsage()
        {
            Console.WriteLine();
            Console.WriteLine("USAGE: Task_1.exe [ input [ output ]]");
            Console.WriteLine();
            Console.WriteLine("input\tInput file in old format (default: {0}).", DefaultInputFile);
            Console.WriteLine("output\tFolder for output files (default: {0}).", DefaultOutputDirectory);
        }

        static void PrintError(string text)
        {
            Console.WriteLine();
            Console.WriteLine("ERROR: {0}", text);
        }
    }
}
