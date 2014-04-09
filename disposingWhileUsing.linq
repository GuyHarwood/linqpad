<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	using(var manager = new Manager())
	{
		Task.Factory.StartNew(() => InitManager(manager));
		manager.Dump("after thread dispatch, before using exits");
	}
}

public class Manager : IDisposable
{
	private bool _disposed;
	
	public string State {get; set;}
	public bool Disposed 
	{
		get
		{
			return _disposed;
		}
	}

	public void Dispose()
	{
		Dispose(true);
		GC.SuppressFinalize(this);
	}
	
	protected virtual void Dispose(bool disposing)
	{
		if (!_disposed)
		{
			if (disposing)
			{
				_disposed = true;
			}
			_disposed = true;
		}
	}
}

private void InitManager(Manager manager)
{
	Thread.Sleep(500);
	try
	{	
		manager.Dump("Before Init");
		manager.State = "Ready";
		manager.Dump("After init");
	}
	catch (Exception ex)
	{
		manager.Dump("exception occured");
		ex.Dump();
	}
	
}

// Define other methods and classes here
