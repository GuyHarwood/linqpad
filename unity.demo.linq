<Query Kind="Program">
  <NuGetReference>Unity</NuGetReference>
  <Namespace>System</Namespace>
  <Namespace>System.Threading</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>Microsoft.Practices.Unity</Namespace>
</Query>

void Main()
{
	ResolveType();
	RegisterInstance();
}

public void ResolveType()
{
    Demo.ResetCounters();
 
    using (var container = new UnityContainer())
    {
        var demoA = container.Resolve<Demo>();
        var demoB = container.Resolve<Demo>();
 
        Demo.ConstructorCounter.Dump();
    }
 
    Demo.DisposeCounter.Dump();
}

public void DefaultLifetimeManager()
{
    Demo.ResetCounters();
 
    using (var container = new UnityContainer())
    {
        container.RegisterType<IDemo, Demo>();
 
        var demoA = container.Resolve<IDemo>();
        var demoB = container.Resolve<IDemo>();
 
        Assert.NotSame(demoA, demoB);
        Assert.Equal(2, Demo.ConstructorCounter);
    }
 
    Assert.Equal(0, Demo.DisposeCounter);
}

public void TransientLifetimeManager()
{
    Demo.ResetCounters();
 
    using (var container = new UnityContainer())
    {
        var manager = new TransientLifetimeManager();
        container.RegisterType<IDemo, Demo>(manager);
 
        var demoA = container.Resolve<IDemo>();
        var demoB = container.Resolve<IDemo>();
 
        Assert.NotSame(demoA, demoB);
        Assert.Equal(2, Demo.ConstructorCounter);
    }
 
    Assert.Equal(0, Demo.DisposeCounter);
}

public void RegisterInstance()
{
    Demo.ResetCounters();
 
    using (var container = new UnityContainer())
    {
        var demo = new Demo();
        container.RegisterInstance<IDemo>(demo);
 
        var demoA = container.Resolve<IDemo>();
        var demoB = container.Resolve<IDemo>();
 
        demoA.Equals(demoB).Dump();
        Assert.Equal(1, Demo.ConstructorCounter);
    }
 
    Assert.Equal(1, Demo.DisposeCounter);
}

public void ContainerControlledLifetimeManager()
{
    Demo.ResetCounters();
            
    using (var container = new UnityContainer())
    {
        var manager = new ContainerControlledLifetimeManager();
        container.RegisterType<IDemo, Demo>(manager);
 
        var demoA = container.Resolve<IDemo>();
        var demoB = container.Resolve<IDemo>();
 
        Assert.Same(demoA, demoB);
        Assert.Equal(1, Demo.ConstructorCounter);
    }
 
    Assert.Equal(1, Demo.DisposeCounter);
}

public void ExternallyControlledLifetimeManager()
{
    Demo.ResetCounters();
 
    using (var container = new UnityContainer())
    {
        var manager = new ExternallyControlledLifetimeManager();
        container.RegisterType<IDemo, Demo>(manager);
 
        var demoA = container.Resolve<IDemo>();
        var demoB = container.Resolve<IDemo>();
 
        Assert.Same(demoA, demoB);
        Assert.Equal(1, Demo.ConstructorCounter);
    }
 
    Assert.Equal(0, Demo.DisposeCounter);
}

public void ContainerControlledLifetimeManagerWithKey()
{
    Demo.ResetCounters();
 
    using (var container = new UnityContainer())
    {
        var manager1 = new ContainerControlledLifetimeManager();
        container.RegisterType<IDemo, Demo>("1", manager1);
 
        var manager2 = new ContainerControlledLifetimeManager();
        container.RegisterType<IDemo, Demo>("2", manager2);
 
        Assert.Throws<ResolutionFailedException>(() => container.Resolve<IDemo>());
 
        var demo1A = container.Resolve<IDemo>("1");
        var demo1B = container.Resolve<IDemo>("1");
        var demo2A = container.Resolve<IDemo>("2");
        var demo2B = container.Resolve<IDemo>("2");
                
        Assert.Same(demo1A, demo1B);
        Assert.Same(demo2A, demo2B);
        Assert.NotSame(demo1A, demo2A);
        Assert.Equal(2, Demo.ConstructorCounter);
    }
 
    Assert.Equal(2, Demo.DisposeCounter);
}

public void RegisterTypeForMultipleInterfaces()
{
    Demo.ResetCounters();
 
    using (var container = new UnityContainer())
    {
        var demoManager = new ContainerControlledLifetimeManager();
        container.RegisterType<IDemo, Demo>(demoManager);
 
        var demo = container.Resolve<IDemo>();
 
        Assert.Throws<ResolutionFailedException>(() => container.Resolve<IAnotherDemo>());
 
        Assert.NotNull(demo);
        Assert.Equal(1, Demo.ConstructorCounter);
    }
 
    Assert.Equal(1, Demo.DisposeCounter);
}

public void ContainerControlledLifetimeManagerWithMultipleInterfaces()
{
    Demo.ResetCounters();
 
    using (var container = new UnityContainer())
    {
        var demoManager = new TransientLifetimeManager();
        container.RegisterType<IDemo, Demo>(demoManager);
 
        var anotherManager = new ContainerControlledLifetimeManager();
        container.RegisterType<IAnotherDemo, Demo>(anotherManager);
 
        var demoA = container.Resolve<IDemo>();
        var demoB = container.Resolve<IDemo>();
        var anotherDemoA = container.Resolve<IAnotherDemo>();
        var anotherDemoB = container.Resolve<IAnotherDemo>();
 
        Assert.Same(demoA, demoB);
        Assert.Same(anotherDemoA, anotherDemoB);
        Assert.Same(demoA, anotherDemoA);
        Assert.Equal(1, Demo.ConstructorCounter);
    }
 
    Assert.Equal(1, Demo.DisposeCounter);
}

public void TransientLifetimeManagerWithMultipleInterfaces()
{
    Demo.ResetCounters();
 
    using (var container = new UnityContainer())
    {
        var demoManager = new ContainerControlledLifetimeManager();
        container.RegisterType<IDemo, Demo>(demoManager);
 
        var anotherManager = new TransientLifetimeManager();
        container.RegisterType<IAnotherDemo, Demo>(anotherManager);
 
        var demoA = container.Resolve<IDemo>();
        var demoB = container.Resolve<IDemo>();
        var anotherDemoA = container.Resolve<IAnotherDemo>();
        var anotherDemoB = container.Resolve<IAnotherDemo>();
 
        Assert.NotSame(demoA, demoB);
        Assert.NotSame(anotherDemoA, anotherDemoB);
        Assert.NotSame(demoA, anotherDemoA);
        Assert.Equal(4, Demo.ConstructorCounter);
    }
 
    Assert.Equal(0, Demo.DisposeCounter);
}

public void RegisterInstanceMultipleTimes()
{
    Demo.ResetCounters();
 
    using (var container = new UnityContainer())
    {
        var demo = new Demo();
 
        container.RegisterInstance<IDemo>(demo);
        container.RegisterInstance<IAnotherDemo>(demo);
 
        var demoA = container.Resolve<IDemo>();
        var anotherDemoA = container.Resolve<IAnotherDemo>();
                
        Assert.Same(demoA, anotherDemoA);
        Assert.Equal(1, Demo.ConstructorCounter);
    }
 
    Assert.Equal(2, Demo.DisposeCounter);
}

public void RegisterInstanceMultipleTimesWithExternal()
{
    Demo.ResetCounters();
 
    using (var container = new UnityContainer())
    {
        var demo = new Demo();
 
        var demoManager = new ContainerControlledLifetimeManager();
        container.RegisterInstance<IDemo>(demo, demoManager);
                
        var anotherManager = new ExternallyControlledLifetimeManager();
        container.RegisterInstance<IAnotherDemo>(demo, anotherManager);
 
        var demoA = container.Resolve<IDemo>();
        var anotherDemoA = container.Resolve<IAnotherDemo>();
 
        Assert.Same(demoA, anotherDemoA);
        Assert.Equal(1, Demo.ConstructorCounter);
    }
 
    Assert.Equal(1, Demo.DisposeCounter);
}

public static class Assert
{
	public static void Equal(int a, int b)
	{
		a.Equals(b).Dump("Assert.Equals");
	}
}

public interface IAnotherDemo { }
        
public interface IDemo : IDisposable { }
 
public class Demo : IDemo, IAnotherDemo
{
    public static int ConstructorCounter {get; private set;}
    public static int DisposeCounter {get; private set;}
 
    public static void ResetCounters()
    {
        ConstructorCounter = 0;
        DisposeCounter = 0;
    }
 
    public Demo()
    {
        ConstructorCounter++;
    }
 
    public void Dispose()
    {
        DisposeCounter++;
    }
}