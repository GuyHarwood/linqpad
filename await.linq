<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var second = GetSecond();
	second.Dump();
}

private async Task<int> GetSecond()
{
	var result = await innerGetSecond();
	return result;
}

private Task<int> innerGetSecond()
{
	Thread.Sleep(1000);
	return Task<int>.Factory.StartNew(() => System.DateTime.Now.Second);
}

// Define other methods and classes here
