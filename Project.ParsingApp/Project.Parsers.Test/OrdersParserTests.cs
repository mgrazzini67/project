
using Project.Parsers.Models;
using Project.Parsers.ParserUtils;
using System;
using System.IO;
using Xunit;

namespace Project.Parsers.Test
{
    public class OrdersParserTests
    {
        const string FILE_PATH_BASE = "testFiles/orders/";
        [Fact]
        public void CanParseSimpleFile()
        {
            const string filePath = FILE_PATH_BASE + "orders.txt";

            var orders = new OrderParser(filePath).Parse();
            Assert.Equal(5, orders.Count);

            var expectedOrders = new[]
            {
                new Order { Id = 181, CreationDate = Convert.ToDateTime("2019-01-08T14:07:53Z"), ItemId = 5432, CustomerId = 1002, NumberItemInOrder = 12, SingleItemPrice  = 25.67m},
                new Order { Id = 182, CreationDate = Convert.ToDateTime("2019-01-08T15:53:11Z"), ItemId = 6987, CustomerId = 1003, NumberItemInOrder = 110, SingleItemPrice  = 0.99m, Comment = "Nice!"},
                new Order { Id = 183, CreationDate = Convert.ToDateTime("2019-01-09T02:11:07Z"), ItemId = 4569, CustomerId = 1005, NumberItemInOrder = 1, SingleItemPrice  = 9m},
                new Order { Id = 184, CreationDate = Convert.ToDateTime("2019-01-09T21:54:45Z"), ItemId = 5432, CustomerId = 1001, NumberItemInOrder = 1, SingleItemPrice  = 25.75m, Comment = "Well done, good service."},
                new Order { Id = 185, CreationDate = Convert.ToDateTime("2019-01-10T11:45:29Z"), ItemId = 965, CustomerId = 1002, NumberItemInOrder = 10, SingleItemPrice  = 101.99m, Comment = "good"},
            };

            for (var index = 0; index < orders.Count; index++)
            {
                var order = orders[index];
                var expectedOrder = expectedOrders[index];
                Assert.Equal(expectedOrder.Id, order.Id);
                Assert.Equal(expectedOrder.CreationDate, order.CreationDate);
                Assert.Equal(expectedOrder.ItemId, order.ItemId);
                Assert.Equal(expectedOrder.CustomerId, order.CustomerId);
                Assert.Equal(expectedOrder.NumberItemInOrder, order.NumberItemInOrder);
                Assert.Equal(expectedOrder.SingleItemPrice, order.SingleItemPrice);
                Assert.Equal(expectedOrder.Comment, order.Comment);
            }
        }

        [Fact]
        public void ParsingEmptyFileReturnsEmptyList()
        {

        }

        [Fact]
        public void ThrowsWhenFileDoesNotExist()
        {
            const string filePath = FILE_PATH_BASE + "non-existing-file.txt";

            Assert.Throws<FileNotFoundException>(() => new CustomerParser(filePath).Parse());
        }

        [Fact]
        public void ParsingOrderWithInvalidNumberParameters()
        {

        }

        [Fact]
        public void ParsingOrderDateInvalidFormat()
        {

        }

        [Fact]
        public void ParsingOrderMultipleWordCommentaryWithSemicolon()
        {

        }
    }
}
