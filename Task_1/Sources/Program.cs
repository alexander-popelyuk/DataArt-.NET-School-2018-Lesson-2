using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;


namespace Task_1
{
    class Program
    {
        const string DefaultInputFile = "clientinfo_input.xml";
        const string DefaultOutputDirectory = "./Output";
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
            Convert.FromXml<OldFormat.Client>(input_file, out old_clinet);
            new_clinet = old_clinet;
            if (!Directory.Exists(output_directory)) Directory.CreateDirectory(output_directory);
            Convert.ToJson(new_clinet, Path.Combine(output_directory, ClinetInfoFileName + ".json"));
            Convert.ToXml(new_clinet, Path.Combine(output_directory, ClinetInfoFileName + ".xml"));
            Convert.ToJson(new_clinet.HomeAddress, Path.Combine(output_directory, HomeAddressFileName + ".json"));
            Convert.ToXml(new_clinet.WorkAddress, Path.Combine(output_directory, WorkAddressFileName + ".xml"));
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
            Console.WriteLine("output\tOutput folder for three files: (default: {0}).", DefaultOutputDirectory);
        }

        static void PrintError(string text)
        {
            Console.WriteLine("ERROR: {0}", text);
        }
    }
}
