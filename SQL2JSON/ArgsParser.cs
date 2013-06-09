using System.IO;
using NDesk.Options;

namespace SQL2JSON
{
    public class ArgsParser
    {
        public string ConnectionString;
        public string SQL;
        public string Delimeter;
        public string OutputFilePath;
        public bool ShowHelp;
        private OptionSet optionSet;

        public ArgsParser()
        {
            optionSet = new OptionSet {
                { "cs=", "Connection string to the database", v => ConnectionString = v },
                { "sql=", "SQL query", v => SQL = v },
                { "output=", "Output file path", v => OutputFilePath = v },
                { "delimeter=", "The delimeter used to separate JSON objects(optional)", v => Delimeter = v },
                { "help",  "show this message", v => ShowHelp = v != null }
            };
        }

        public void Parse(string[] args)
        {
            ConnectionString = null;
            SQL = null;
            Delimeter = null;
            OutputFilePath = null;
            ShowHelp = false;

            try {
                optionSet.Parse(args);
            } catch (OptionException e) {
                ShowHelp = true;
            }
        }

        public void WriteOptionDescriptions(TextWriter writer)
        {
            optionSet.WriteOptionDescriptions(writer);
        }
    }
}