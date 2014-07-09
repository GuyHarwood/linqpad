<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var inv = new Invoice(new InvoiceFooter(null));
	inv.Generate(1).Dump();
}

public interface IGenerateInvoice
{
	string Generate(int id);
	IGenerateInvoice Next();
}

public class Invoice : IGenerateInvoice
{
	private readonly IGenerateInvoice _next; 
	
	public Invoice(IGenerateInvoice next)
	{
		_next = next;	
	}

	public string Generate(int id)
	{
		var invoice = string.Format("Invoice {0}", id);
		return invoice;
	}	
	
	public IGenerateInvoice Next()
	{
		return _next;
	}
}

public class InvoiceFooter : IGenerateInvoice
{
	private readonly IGenerateInvoice _nextGenerator;
	
	public InvoiceFooter(IGenerateInvoice nextGenerator)
	{
		_nextGenerator = nextGenerator;
	}
	
	public string Generate(int id)
	{
		throw new NotImplementedException();
	}
	
	public IGenerateInvoice Next()
	{
		return _nextGenerator;
	}
}