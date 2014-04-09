<Query Kind="Program">
  <Connection>
    <ID>c5a93308-7666-4bf2-9ed8-9e4091ba86fd</ID>
    <Persist>true</Persist>
    <Server>devvm-sql</Server>
    <SqlSecurity>true</SqlSecurity>
    <UserName>guy</UserName>
    <Password>AQAAANCMnd8BFdERjHoAwE/Cl+sBAAAAYmlIsXV2m0q46KvvCMsM3AAAAAACAAAAAAADZgAAwAAAABAAAABVz23T0x3Qvqtm80waxNMvAAAAAASAAACgAAAAEAAAADbo+JZC+PT83v3QPClDvZYQAAAAcliS0tk0ZW5aL3o+ZKQ6LBQAAAAgTqRpUGgDwVr8xWKkqfV7c/UqwA==</Password>
    <Database>TDXRMIS</Database>
    <ShowServer>true</ShowServer>
  </Connection>
  <Namespace>System</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var searchProducts = Task.Factory.StartNew(() => GetProducts());
	var searchBrochure = Task.Factory.StartNew(() => GetBrochures());
	var all = Task.WhenAll(new [] { searchProducts, searchBrochure });
	
	all.Dump();
}

private IEnumerable<Product> GetProducts()
{
	return Enumerable.Range(11, 20).Select(i => new Product()
	{
		Id = i
	}).ToArray();
}

private IEnumerable<Product> GetBrochures()
{
	return Enumerable.Range(11, 20).Select(i => new Product()
	{
		Id = i
	}).ToArray();
}

public class Product
{
	public int Id {get;set;}
}


