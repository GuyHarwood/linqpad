<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var svc = new ServiceChannel();
	
	var tokens = (new []
	{
		"Token1", "Token2", "Token3", "Token4", "Token5", "Token6", "Token7", "Token8"
	}).ToList();
	
	const int retryLimit = 3;
	var retryCount = 0;
	
	while(tokens.Any() && retryCount < retryLimit)
	{
		try
		{	 
			(string.Format("{0}, Try:{1}",tokens.First(), retryCount)).Dump();
			svc.Call(tokens.First ());
			tokens.RemoveAt(0);
			retryCount = 0;
		}
		catch (Exception ex)
		{
			("EXC:" + ex.Message).Dump();
			retryCount++;
			if(svc.State.Equals("Faulted"))
			{
				svc = new ServiceChannel();
				svc.Fail = true;
			}
		}
	}
	
	if(tokens.Any())
	{
		"Terminating process, retryLimit exceeded.".Dump();
	}
}

public class ServiceChannel
{
	private readonly Random rnd;
	
	public ServiceChannel()
	{
		rnd = new Random();
	}
	
	private string state = string.Empty;
		
	public bool Fail
	{
		get;set;
	}
	
	public void Call(string token)
	{
		var ms = DateTime.Now.Millisecond.ToString();
		Fail = token[5].Equals(ms[ms.Length - 1]);
		
		if(Fail)
		{
			Thread.Sleep(rnd.Next(1,999));
			state = "Faulted";
			throw new InvalidOperationException("Channel in faulted state");
		}
		var msg = "Success:" + token;
		msg.Dump();
	}
	
	public string State
	{
		get
		{
			return state;		
		}
	}
}