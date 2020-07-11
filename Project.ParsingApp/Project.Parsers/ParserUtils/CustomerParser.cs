namespace Project.Parsers
{
    using Project.Parsers.Exceptions;
    using Project.Parsers.ParserUtils;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;

    public class CustomerParser : IParser<Customer>
    {
        //# this will probably go to the constructor if we work with french people, as they use ';' instead of ',' (frenchies am I right)
        private const char SEPARATOR = ',';
        private readonly string FilePath;

        public CustomerParser(string filePath)
        {
            this.FilePath = filePath;
        }
        public IList<Customer> Parse()
        {
            CheckFileExist(this.FilePath);
            string[] lines = File.ReadAllLines(this.FilePath);
            List<Customer> results = new List<Customer>();
            Array.ForEach(lines, line =>
            {
                string[] lineParts = line.Split(SEPARATOR);
                try
                {
                    CheckLineValidity(lineParts);
                }
                catch (ParserException e)
                {
                    Trace.WriteLine($"Error when parsing line {line} . Reason: {e.Message}");
                    return;
                }

                results.Add(new Customer
                {
                    Id = int.Parse(lineParts[0]),
                    FirstName = lineParts[1],
                    LastName = lineParts[2],
                    Status = (CustomerStatus)Enum.Parse(typeof(CustomerStatus), lineParts[3])
                });
            });

            return results;
        }

        private void CheckFileExist(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"File does not exist: {filePath}");
            }
        }
        private void CheckLineValidity(string[] lineParts)
        {
            if(lineParts.Length != 4)
            {
                throw new ParserException($"Irregular number of arguments");
            }
            if (!int.TryParse(lineParts[0], out int id))
            {
                throw new ParserException($"Cannot parse ID.");
            }

            if (string.IsNullOrWhiteSpace(lineParts[1]))
            {
                throw new ParserException($"Cannot parse first name.");
            }

            if (string.IsNullOrWhiteSpace(lineParts[2]))
            {
                throw new ParserException($"Cannot parse last name.");
            }

            if (!int.TryParse(lineParts[3], out int customerStatus))
            {
                throw new ParserException($"Cannot parse customer status.");
            }

            if (!Enum.IsDefined(typeof(CustomerStatus), customerStatus))
            {
                throw new ParserException($"Customer status is not supported.");
            }
        }
    }
}