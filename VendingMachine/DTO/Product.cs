using System;

namespace VendingMachine.DTO
{
	public class Product
	{
		public string Name { get; set; }
		public decimal Price { get; set; }

		public Product()
		{
			Name = string.Empty;
			Price = 0.0m;
		}
    
	    public Product(string name, decimal price)
	    {
	        Name = name;
	        Price = price;
	    }

        public override string ToString()
	    {
	        return $"Name: {Name}\t Price: {Price}";
	    }

	    public override bool Equals(object obj)
	    {
	        return obj is Product b && b.Name.Equals(Name, StringComparison.InvariantCultureIgnoreCase);
	    }

	    public override int GetHashCode()
	    {
	        var hashCode = (Name != null ? Name.GetHashCode() : 0);
	        return hashCode;
	    }
	}
}
