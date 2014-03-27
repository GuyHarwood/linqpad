<Query Kind="Program" />


void Main()
{
	var nameData = "Birt,Chris,Sir";
	//usage
	var firstName = GetNamePart(nameData, "firstname");
	firstName.Dump();
	
	var addressData = "GREEN SLOPES QLD 4120";
	var city = GetAddressPart(addressData, "city");
	city.Dump();
	
	var area = GetAddressPart(addressData, "area");
	area.Dump();
	
	var postcode = GetAddressPart(addressData, "postcode");
	postcode.Dump();
}

private string GetAddressPart(string input, string part)
{
	var splitAddressData = input.Split(' ');
	
	switch(part)
	{
		case "city" : 
		{
			var upperCityIndex = splitAddressData.Length - 2;
			return string.Join(" ", splitAddressData.Take(upperCityIndex));
			break;
		}
		case "area" :
			return splitAddressData[splitAddressData.Length - 2];
			break;
			
		case "postcode" :
			return splitAddressData[splitAddressData.Length - 1];
			break;
		default:
			return string.Empty;
	}
}

private string GetNamePart(string input, string part)
{
	byte index;
	switch (part)
	{
		case "firstname" :
			index = 0;
			break;
		case "surname" : 
			index = 1;
			break;
		case "title" : 
			index = 2;
			break;
		default:
			return string.Empty;
	}
	
	return input.Split(',')[index];
}

// Define other methods and classes here
