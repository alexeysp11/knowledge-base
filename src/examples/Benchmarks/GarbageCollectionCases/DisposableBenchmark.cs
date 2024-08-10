using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace KnowledgeBase.Examples.Benchmarks.GarbageCollectionCases;

public interface IPerson
{
    string? Name { get; set; }
}

public class SimplePerson : IPerson
{
    public string? Name { get; set; }
}

public class DisposablePerson : IPerson, System.IDisposable
{
    public string? Name { get; set; }
    
    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                // 
            }
            disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~DisposablePerson()
    {
        Dispose(false);
    }
}

[MemoryDiagnoser]
public class DisposableBenchmark
{
    private int m_number = 10000;

    [Benchmark]
    [InvocationCount(1000)]
    public void WithIDisposable()
    {
        for (int i = 0; i < m_number; i++)
        {
            using (var person = new DisposablePerson())
            {
                person.Name = $"DPerson{i}";
            }
        }
        // GC.Collect(0);
    }

    [Benchmark]
    [InvocationCount(1000)]
    public void WithoutIDisposable()
    {
        for (int i = 0; i < m_number; i++)
        {
            var person = new SimplePerson();
            person.Name = $"SPerson{i}";
        }
        // GC.Collect(0);
    }
}