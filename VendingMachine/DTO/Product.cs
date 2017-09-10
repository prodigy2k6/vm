

namespace VendingMachine.DTO
{
	public class Product
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
        public int Code { get; set; }

		public Product()
		{
			Name = string.Empty;
			Price = 0.0m;
		}

	    public override string ToString()
	    {
	        return $"Name: {Name}\t Price: {Price}\t SelectionCode: {Code}";
	    }
	}
}
