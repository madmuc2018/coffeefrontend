using System;
namespace coffeefrontend
{
    public class Order : IEquatable<Order>
    {
        public string id { get; set; }
        public string type { get; set; }
        public string quantity { get; set; }
        public string from { get; set; }
        public string status { get; set; }

        public Order(string id, string type, string quantity, string from, string status)
        {
            this.id = id;
            this.type = type;
            this.quantity = quantity;
            this.from = from;
            this.status = status;
        }

        public bool Equals(Order other)
        {
            return id == other.id 
                && type == other.type 
                && quantity == other.quantity 
                && from == other.from 
                && status == other.status;
        }
    }
}
