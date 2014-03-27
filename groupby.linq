<Query Kind="Statements">
  <Reference>&lt;RuntimeDirectory&gt;\System.Linq.dll</Reference>
</Query>

 var persons = new [] 
 			{
				new  { PersonID = 1, Group = "Staff" },
				new  { PersonID = 1, Group = "Contractor" },
				new  { PersonID = 1, Group = "Staff" },
				new  { PersonID = 1, Group = "Staff" },
				new  { PersonID = 1, Group = "Staff" },
				new  { PersonID = 1, Group = "Contractor" }
			};
			
var results = persons.ToLookup(p => p.Group,
                         p => p.PersonID);
						 
foreach(var result in results)
{
	result.Dump();
}