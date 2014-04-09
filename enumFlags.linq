<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	var epicMotor = Features.Lights | Features.Stereo | Features.Jack;
	int value = (int)epicMotor;
	value.Dump();
	
	if ((epicMotor & Features.Lights) == Features.Lights)
		Console.WriteLine("it has lights");
}

[Flags]
public enum Features
{
	Lights = 1,
	Stereo = 2,
	Jack = 4,
	Wipers = 8
}