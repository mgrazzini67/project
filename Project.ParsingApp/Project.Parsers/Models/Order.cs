using System;

namespace Project.Parsers.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public int ItemId { get; set; }
        public int CustomerId { get; set; }
        public int NumberItemInOrder { get; set; }
        public decimal SingleItemPrice { get; set; }
        public string Comment { get; set; }
    }
}
