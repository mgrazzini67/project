namespace Project.Parsers.Test
{
    using System.Collections.Generic;
    using System.IO;
    using Xunit;


    public class CustomersParserTests
    {
        const string FILE_PATH_BASE = "testFiles/customers/";
        [Fact]
        public void CanParseSimpleFile()
        {
            const string filePath = FILE_PATH_BASE + "customers.csv";

            var actualCustomers = new CustomerParser(filePath).Parse();

            Assert.Equal(8, actualCustomers.Count);

            var expectedCustomers = new[]
            {
                new Customer { Id = 1001, FirstName = "Mary", LastName = "Smith", Status = CustomerStatus.Premium },
                new Customer { Id = 1002, FirstName = "John", LastName = "Jones", Status = CustomerStatus.Regular },
                new Customer { Id = 1003, FirstName = "William", LastName = "Williams", Status = CustomerStatus.Premium },
                new Customer { Id = 1004, FirstName = "Anna", LastName = "Taylor", Status = CustomerStatus.Regular },
                new Customer { Id = 1005, FirstName = "Dave", LastName = "Davies", Status = CustomerStatus.Regular },
                new Customer { Id = 1006, FirstName = "Susan", LastName = "Evans", Status = CustomerStatus.Regular },
                new Customer { Id = 1007, FirstName = "Thomas", LastName = "Jameson", Status = CustomerStatus.Regular },
                new Customer { Id = 1008, FirstName = "Arthur", LastName = "Richards-Chamberlain", Status = CustomerStatus.Premium }
            };

            for (var index = 0; index < actualCustomers.Count; index++)
            {
                var actualCustomer = actualCustomers[index];
                var expectedCustomer = expectedCustomers[index];
                Assert.Equal(expectedCustomer.Id, actualCustomer.Id);
                Assert.Equal(expectedCustomer.FirstName, actualCustomer.FirstName);
                Assert.Equal(expectedCustomer.LastName, actualCustomer.LastName);
                Assert.Equal(expectedCustomer.Status, actualCustomer.Status);
            }
        }

        [Fact]
        public void ParsingEmptyFileReturnsEmptyList()
        {
            const string filePath = FILE_PATH_BASE + "empty.csv";

            var actualCustomers = new CustomerParser(filePath).Parse();

            Assert.Equal(0, actualCustomers.Count);
        }

        [Fact]
        public void ThrowsWhenFileDoesNotExist()
        {
            const string filePath = FILE_PATH_BASE +  "non-existing-file.csv";

            Assert.Throws<FileNotFoundException>(() => new CustomerParser(filePath).Parse());
        }

        [Fact]
        public void ParsingCsvWithInvalidNumberParameters()
        {
            const string filePath = FILE_PATH_BASE + "customerIncorrectNumberParameters.csv";

            IList<Customer> actualCustomers = new CustomerParser(filePath).Parse();

            Assert.Equal(1, actualCustomers.Count);

            var expectedCustomers = new[]
            {
                new Customer { Id = 1001, FirstName = "Valid", LastName = "Line", Status = CustomerStatus.Premium }
            };

            for (var index = 0; index < actualCustomers.Count; index++)
            {
                var actualCustomer = actualCustomers[index];
                var expectedCustomer = expectedCustomers[index];
                Assert.Equal(expectedCustomer.Id, actualCustomer.Id);
                Assert.Equal(expectedCustomer.FirstName, actualCustomer.FirstName);
                Assert.Equal(expectedCustomer.LastName, actualCustomer.LastName);
                Assert.Equal(expectedCustomer.Status, actualCustomer.Status);
            }
        }
    }
}