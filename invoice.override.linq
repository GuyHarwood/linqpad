<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var inv = new CustomInvoice();
	inv.Generate(1).Dump();
}

public class Invoice
{
	public string Generate(int id)
	{
		var invoice = string.Format("Invoice {0}", id);
		invoice = OnGenerateInvoice(invoice);
		return invoice;
	}
	
	protected virtual string OnGenerateInvoice(string invoice)
	{
		return invoice;
	}
}

public class CustomInvoice : Invoice
{
	protected override string OnGenerateInvoice(string invoice)
	{
		return string.Format("{0} created at {1}", invoice, DateTime.Now.ToShortTimeString());
	}
}