<Query Kind="Program">
  <Namespace>System</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

void Main()
{
	CalculateValue(2,2014,7,9,11,4).Dump();
}

 private static long CalculateValue(int branchNumber, int year, int month, int day, int hour, int minute)
{
 return branchNumber * 1000000000000L + year * 100000000L + month * 1000000L + day * 10000L + hour * 100L + minute;
}

