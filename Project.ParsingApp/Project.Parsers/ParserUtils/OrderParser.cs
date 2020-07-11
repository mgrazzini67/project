using Project.Parsers.Exceptions;
using Project.Parsers.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace Project.Parsers.ParserUtils
{
    public class OrderParser : IParser<Order>
    {
        private const char SEPARATOR = ' ';
        private readonly string FilePath;

        public OrderParser(string filePath)
        {
            this.FilePath = filePath;
        }
        public IList<Order> Parse()
        {
            CheckFileExist(this.FilePath);
            string[] lines = File.ReadAllLines(this.FilePath);
            List<Order> orders = new List<Order>();
            Array.ForEach(lines, line =>
            {
                string[] lineParts = line.Trim().Split(SEPARATOR);
                try
                {
                    CheckLineValidity(lineParts);
                }
                catch (ParserException e)
                {
                    Trace.WriteLine($"Error when parsing line {line} . Reason: {e.Message}");
                    return;
                }

                orders.Add(new Order
                {
                    Id = int.Parse(lineParts[0]),
                    CreationDate = DateTime.Parse(lineParts[1]),
                    ItemId = int.Parse(lineParts[2]),
                    CustomerId = int.Parse(lineParts[3]),
                    NumberItemInOrder = int.Parse(lineParts[4]),
                    SingleItemPrice = decimal.Parse(lineParts[5], new CultureInfo("de")),
                    Comment = GetCommentFromParts(lineParts, 6)
                });
            });

            return orders;
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
            if (lineParts.Length < 6)
            {
                throw new ParserException($"Irregular number of arguments");
            }
            if (!int.TryParse(lineParts[0], out int id) || id < 0)
            {
                throw new ParserException($"Cannot parse ID.");
            }
            if (!DateTime.TryParse(lineParts[1], null, System.Globalization.DateTimeStyles.RoundtripKind, out DateTime dDate))
            {
                throw new ParserException($"Cannot parse Date.");
            }
            if (!int.TryParse(lineParts[2], out int itemId) || itemId < 0)
            {
                throw new ParserException($"Cannot parse item ID.");
            }
            if (!int.TryParse(lineParts[3], out int customerID) || customerID < 0)
            {
                throw new ParserException($"Cannot parse customer ID.");
            }
            if (!int.TryParse(lineParts[4], out int numberItemInOrder) || numberItemInOrder < 0)
            {
                throw new ParserException($"Cannot parse number of item.");
            }
            if (!decimal.TryParse(lineParts[5], out decimal singleItemPrice) || singleItemPrice < 0)
            {
                throw new ParserException($"Cannot parse price.");
            }
        }

        /// <summary>
        /// Get all commentary parts, aggregate them and remove the first and last " if needed
        /// Comments come in the form of bla or "bla bla bla". 
        /// </summary>
        /// <returns>Comment string without first and last '"' characters, or null if no comment</returns>
        private string GetCommentFromParts(string[] lineParts, int startingPart)
        {
            if(lineParts.Length <= startingPart)
            {
                return null;
            }
            // For some reason 2.0 and 2.1 do not have range, so I can't do [startingPart..^0]
            var comment = String.Join(" ", lineParts.Skip(startingPart).ToArray());
            comment = comment.Trim('"');
            return comment;
        }
    }
}
