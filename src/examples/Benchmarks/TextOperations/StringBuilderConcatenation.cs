using System;
using System.Text;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace KnowledgeBase.Examples.Benchmarks.TextOperations;

[MemoryDiagnoser]
public class StringBuilderConcatenation
{
    private readonly int m_stringNumber = 10000;
    private StringBuilder m_stringBuilder = new StringBuilder();
    
    [Benchmark]
    [InvocationCount(20)]
    public void StringBuilder_Benchmark()
    {
        m_stringBuilder.Append("result: ");
        for (int i = 0; i < m_stringNumber; i++)
        {
            m_stringBuilder.Append($"string{i} ");
        }
        var result = m_stringBuilder.ToString();
    }

    [Benchmark]
    [InvocationCount(20)]
    public void Concatenation_Benchmark()
    {
        var result = "result: ";
        for (int i = 0; i < m_stringNumber; i++)
        {
            result += $"string{i} ";
        }
    }
}