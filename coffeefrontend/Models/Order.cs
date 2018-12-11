using System;
namespace coffeefrontend
{
    public class Order
    {
        public string id { get; }
        public string type { get; }
        public string quantity { get; }
        public string from { get; }
        public string status { get; }

        public Order(string id, string type, string quantity, string from, string status)
        {
            this.id = id;
            this.type = type;
            this.quantity = quantity;
            this.from = from;
            this.status = status;
        }
    }
}
