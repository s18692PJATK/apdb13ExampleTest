namespace Test2Example.Entities
{
    public class Confectionery_Order
    {
        public int IdConfect { get; set; }
        public int IdOrder { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
        public virtual Confectionery Confectionery { get; set; }
        public virtual Order Order { get; set; }
        

    }
}