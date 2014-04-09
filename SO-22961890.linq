<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async void Main()
{
	var searchProducts = Task.Factory.StartNew(() => GetProducts());
	var searchBrochure = Task.Factory.StartNew(() => GetBrochures());
	var all = await Task.WhenAll(new [] { searchProducts, searchBrochure });
	
	all.Dump();
}

private IEnumerable<Product> GetProducts()
{
	return Enumerable.Range(1, 10).Select(i => new Product()
	{
		Id = i
	}).ToArray();
}

private IEnumerable<Product> GetBrochures()
{
	return Enumerable.Range(11, 10).Select(i => new Product()
	{
		Id = i
	}).ToArray();
}

public class Product
{
	public int Id {get;set;}
}


