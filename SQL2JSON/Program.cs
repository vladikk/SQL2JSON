using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SQL2JSON.Core;
using SQL2JSON.Infrastructure;

namespace SQL2JSON
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var argsParser = new ArgsParser();
            argsParser.Parse(args);

            if (argsParser.ShowHelp) {
                ShowHelp(argsParser);
            }

            var missingParameters = GetMissingParameters(argsParser);
            if (missingParameters.Any()) {
                ShowMissingParametersMessage(missingParameters, argsParser);
            } else {
                Execute(argsParser);
            }
        }

        private static void ShowMissingParametersMessage(string[] missingParameters, ArgsParser argsParser)
        {
            Console.WriteLine("The following arguments are missing or empty:");
            foreach (var missingParameter in missingParameters) {
                Console.WriteLine("# {0}", missingParameter);
            }
            Console.WriteLine();
            ShowHelp(argsParser);
        }

        private static string[] GetMissingParameters(ArgsParser argsParser)
        {
            var result = new List<string>();
            if (string.IsNullOrEmpty(argsParser.ConnectionString))
                result.Add("cs (connection string)");
            if (string.IsNullOrEmpty(argsParser.SQL))
                result.Add("sql (sql query you want to execute)");
            if (string.IsNullOrEmpty(argsParser.OutputFilePath))
                result.Add("output (output file path)");

            return result.ToArray();
        }

        static void ShowHelp(ArgsParser argsParser)
        {
            Console.WriteLine("Executes a sql query against MS-SQL server and captures its results as a json file.");
            Console.WriteLine();
            Console.WriteLine("Usage: SQL2JSON.exe [OPTIONS]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            argsParser.WriteOptionDescriptions(Console.Out);
            Console.WriteLine();
            Console.WriteLine("Example #1:");
            Console.WriteLine("sql2json.exe -cs=\"Data Source=.\\MSSQL2008;Initial Catalog=DB1;User Id=usr;Password=pwd;\" -sql=\"select * from users\" -output=\"c:\\temp\\users.json\"");
            Console.WriteLine();
            Console.WriteLine("Example #2:");
            Console.WriteLine("sql2json.exe -cs=\"Data Source=.\\MSSQL2008;Initial Catalog=DB1;User Id=usr;Password=pwd;\" -sql=\"select user_id, first_name as 'name::first', last_name as 'name::last' from users\" -output=\"c:\\temp\\users.json\"");
        }

        static void Execute(ArgsParser args)
        {
            var transformer = !string.IsNullOrEmpty(args.Delimeter) ? new ObjectGraphTransformer(args.Delimeter) : null;
            var converter = new SqlToJsonConverter(new ADONetDataAccess(args.ConnectionString), new JsonDotNetSerializer(), transformer);
            var json = converter.ConvertQuery(args.SQL);
            File.WriteAllText(args.OutputFilePath, json);
        }
    }
}