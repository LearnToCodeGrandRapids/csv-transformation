using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CommandLine;

namespace CsvTransformation
{
    public class Program
    {
        private class Options
        {
            [Option("input-file", Required = true, HelpText = "Input file to be processed.")]
            public string InputFile { get; set; }

            [Option("output-file", Required = true, HelpText = "Output file destination.")]
            public string OutputFile { get; set; }
        }

        public static void Main(string[] args)
        {
            // Here we are using CommandLineParser to parse the command line
            // arguments. In this case we could have easily parsed them manually
            // but I wanted to show you this very useful library
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptionsAndReturnExitCode)
                .WithNotParsed(HandleParseError);
        }

        /// This method just exists to demonstrate that can have custom handling for error on
        /// command line argument parsing. 
        private static void HandleParseError(IEnumerable<Error> errs)
        {
            Console.WriteLine("Failed to parse command line arguments.");
            
            foreach (var err in errs)
            {
                Console.WriteLine(err.ToString());
            }
        }

        private static void RunOptionsAndReturnExitCode(Options opts)
        {
            Console.WriteLine("Input: " + opts.InputFile + ", Output: " + opts.OutputFile);

            // TODO2 - add file path validation (does file exist, etc.)
            var records = File.ReadLines(opts.InputFile);

            Console.WriteLine("Read in " + records.Count() + " records.");

            var newRecords = new List<string>(new [] { "AccountNumber,LoanId,FirstName,LastName,AmountDue,DateDue,SSN" });

            // here we are building a regex for extracting the values from the records, I recommend 
            // you checkout CsvHelper for doing the same thing http://joshclose.github.io/CsvHelper/reading#getting-all-records
            // CsvHelper is not the most performant solution, but it is usually go enough and it 
            // can make your life a whole lot easier
            var re = new Regex(@"(\d{6}),(\d{3}),""(\w+), (\w+)"",""?([\d,]+)""?,(\d{8}),(\d{4})", RegexOptions.Compiled);

            foreach (var record in records)
            {
                var match = re.Match(record);

                //Console.WriteLine("Record match pattern? " + match.Success);

                if (match.Success)
                {
                    var accountNumber = match.Groups[1].Value;
                    var loanId = match.Groups[2].Value;
                    var lastName = match.Groups[3].Value;
                    var firstName = match.Groups[4].Value;
                    // TODO2 - improve the handling of amounts to account for edge cases, this 
                    // example is very brittle. If anything is wrong it will blow up
                    var amountDue = int.Parse(match.Groups[5].Value.Replace(",", "")) / 100.0m;
                    var uglyDate = match.Groups[6].Value;
                    var dueDate = uglyDate.Substring(0, 4) + "-" + uglyDate.Substring(4, 2) + "-" +
                                  uglyDate.Substring(6, 2);
                    var ssn = match.Groups[7].Value;

                    var newRecord = string.Join(",", accountNumber, loanId, firstName, lastName, amountDue.ToString("F"), dueDate, ssn);

                    //Console.WriteLine("New record: " + newRecord);

                    newRecords.Add(newRecord);
                }
            }

            File.WriteAllLines(opts.OutputFile, newRecords);
        }
    }
}
