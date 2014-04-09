<Query Kind="Program">
  <NuGetReference>AutoFixture.AutoMoq</NuGetReference>
  <Namespace>Castle.Core.Interceptor</Namespace>
  <Namespace>Castle.DynamicProxy</Namespace>
  <Namespace>Castle.DynamicProxy.Generators</Namespace>
  <Namespace>Moq</Namespace>
  <Namespace>Moq.Language</Namespace>
  <Namespace>Moq.Language.Flow</Namespace>
  <Namespace>Moq.Protected</Namespace>
  <Namespace>Moq.Stub</Namespace>
  <Namespace>Ploeh.AutoFixture</Namespace>
  <Namespace>Ploeh.AutoFixture.AutoMoq</Namespace>
  <Namespace>Ploeh.AutoFixture.DataAnnotations</Namespace>
  <Namespace>Ploeh.AutoFixture.Dsl</Namespace>
  <Namespace>Ploeh.AutoFixture.Kernel</Namespace>
</Query>

void Main()
{
	var fixture = new Fixture().Customize(new AutoMoqCustomization());
	var calc = fixture.Freeze<Calculator>();
	var basket = new Basket(calc);
	var items = fixture.CreateMany<Product>(10);
	basket.AddRange(items);
	basket.Count.Dump();
	basket.Cost().Dump();
}

public interface ICalculator
{
	decimal Calculate(List<Product> items);
}

public abstract class Product
{
	public virtual decimal Price
	{
		get{
			return 1;
		}
	}
	
	public virtual string Name
	{
		get{
			return "void";
		}
	}
}

public class Calculator : ICalculator
{
	public decimal Calculate(List<Product> products)
	{
		return products.Sum(p => p.Price);
	}
}

public class Basket
{
	private readonly ICalculator _calculator;
	
	public Basket(ICalculator calculator)
	{
		_calculator = calculator;
	}

	private readonly List<Product> _items = new List<Product>();

	public void Add(Product product)
	{
		_items.Add(product);
	}
	
	public void AddRange(IEnumerable<Product> products)
	{
		_items.AddRange(products);
	}
	
	public decimal Cost()
	{
		return _calculator.Calculate(_items);
	}
	
	public int Count 
	{
		get
		{
			return _items.Count;
		}
	}
}

// Define other methods and classes here