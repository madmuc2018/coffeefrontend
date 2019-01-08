using System;
namespace coffeefrontend
{
    public class Order : IEquatable<Order>
    {
        public string id { get; set; }
        public string producer { get; set; }
        public string farm { get; set; }
        public string elevation { get; set; }
        public string variety { get; set; }
        public string process { get; set; }
        public string quantity { get; set; }
        public string qc { get; set; }
        public string tastingNotes { get; set; }
        public string status { get; set; }
        public string changedBy { get;}
        public string changedAt { get; }


        public Order()
        { }

        public Order(string id, string producer, string farm, string elevation, string variety, string process, string quantity, string qc, string tastingNotes, string status)
        {
            this.id = id;
            this.producer = producer;
            this.farm = farm;
            this.elevation = elevation;
            this.variety = variety;
            this.process = process;
            this.quantity = quantity;
            this.qc= qc;
            this.tastingNotes = tastingNotes;
            this.status = status;
        }

        public bool Equals(Order other)
        {
            return id == other.id 
                && producer == other.producer 
                && farm == other.farm 
                && elevation == other.elevation
                && variety == other.variety
                && process == other.process
                && quantity == other.quantity
                && qc == other.qc
                && tastingNotes == other.tastingNotes
                && status == other.status;
        }
    }
}
