using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace KnowledgeBase.Examples.Benchmarks.CodeStyleApproaches;

/// <summary>
/// A class for testing performance when using initializing structures.
/// </summary>
[MemoryDiagnoser]
public class WhileVsRecursion
{
    private int m_number = 100;

    [Benchmark]
    public void UseWhileLoop()
    {
        var result = UseWhileLoop(m_number);
    }

    [Benchmark]
    public void UseRecursion()
    {
        var result = UseRecursion(0, m_number);
    }

    private int UseWhileLoop(int number)
    {
        int iteration = 0;
        while (iteration < number)
        {
            iteration += 1;
        }
        return iteration;
    }

    private int UseRecursion(int iteration, int number)
    {
        if (iteration < number)
            return UseRecursion(iteration + 1, number);
        return iteration;
    }
}