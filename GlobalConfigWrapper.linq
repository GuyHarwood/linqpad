<Query Kind="Program" />

void Main()
{
	IConfig config = new GlobalConfigWrapper();
	config.Dump();
}

// Define other methods and classes here


public class Util
{
	private readonly IConfig _config;

	public Util()
	{
		_config = new GlobalConfigWrapper();
	}
	
	public Util(IConfig config)
	{
		_config = config;
	}

	public int DefaultExtensionLength
	{
		get{
			return _config.DefaultExtensionLength;
		}
	}
}

public interface IConfig
{
	int DefaultExtensionLength {get;}
}

public class GlobalConfigWrapper : IConfig
{
	public int DefaultExtensionLength
	{
		get{
			 return 15;
		}
	}
}
