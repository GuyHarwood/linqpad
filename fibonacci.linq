<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	int length = 20;
	Rough(length);
	Console.WriteLine("================");
	Elegant(length);
}


public void Rough(int length)
{
	var current = 1;
	int previous = 1;
	
	for (int i = 0; i < length; i++)
	{
		Console.WriteLine("{0} ", current);
		var temp = current + previous;
		previous = current;
		current = temp;
	}
}

static void Elegant (int length)
{
	for (int i = 0, prevFib = 1, curFib = 1; i < length; i++)
	{
		Console.WriteLine("{0} ", prevFib);
		int newFib = prevFib+curFib;
		prevFib = curFib;
		curFib = newFib;
	}
}

// Define other methods and classes here