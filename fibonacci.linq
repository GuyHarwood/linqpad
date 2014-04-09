<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	Fibo();
}


public void Fibo()
{
	var current = 1;
	int previous = 1;
	
	for (int i = 0; i < 100; i++)
	{
		Console.WriteLine(current);
		var temp = current + previous;
		previous = current;
		current = temp;
	}
}

// Define other methods and classes here
