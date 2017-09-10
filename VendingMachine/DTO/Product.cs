

using System;

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
    
	    public Product(int code, string name, decimal price)
	    {
	        Code = code;
	        Name = name;
	        Price = price;
	    }

        public override string ToString()
	    {
	        return $"Name: {Name}\t Price: {Price}\t SelectionCode: {Code}";
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
