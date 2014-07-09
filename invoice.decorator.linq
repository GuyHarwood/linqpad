<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var inv = new CustomInvoice(new Invoice());
	inv.Generate(1).Dump();
}

public interface IGenerateInvoice
{
	string Generate(int id);
}

public class Invoice : IGenerateInvoice
{
	public string Generate(int id)
	{
		var invoice = string.Format("Invoice {0}", id);
		return invoice;
	}	
}

public class CustomInvoice : IGenerateInvoice
{
	private readonly IGenerateInvoice _decoratedInvoice;
	
	public CustomInvoice(IGenerateInvoice decoratedInvoice)
	{
		_decoratedInvoice = decoratedInvoice;
	}
	
	public string Generate(int id)
	{
		var generated = _decoratedInvoice.Generate(id);
		return string.Format("{0} created at {1}", generated, DateTime.Now.ToShortTimeString());
	}
}
