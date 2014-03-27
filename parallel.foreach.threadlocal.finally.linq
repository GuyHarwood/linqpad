<Query Kind="Statements" />

var data = Enumerable.Range(1, 10000);
var expected = data.Count();
var allOfEm = new List<int>();

System.Threading.Tasks.Parallel.ForEach<int,List<int>>(data, 
				 () => new List<int>(),
				(item, loopState, local) => 
				{
					local.Add(item);
					return local;
				},
				(local) => allOfEm.AddRange(local));
				
allOfEm.Count().Dump();
expected.Dump();
				
