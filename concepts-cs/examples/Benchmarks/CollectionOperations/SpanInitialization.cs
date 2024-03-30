using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Concepts.Examples.Benchmarks.CollectionOperations;

[MemoryDiagnoser]
public class SpanInitialization
{
    [Benchmark]
    public void InitStackallockLoop_Benchmark()
    {
        int length = 5;
        Span<int> numbers = stackalloc int[length];
        for (var i = 0; i < length; i++)
        {
            numbers[i] = i;
        }
    }

    [Benchmark]
    public void InitStackallock_Benchmark()
    {
        Span<int> numbers = stackalloc int[] { 1, 2, 3, 4, 5};
    }

    [Benchmark]
    public void InitUsingArray_Benchmark()
    {
        Span<int> numbers = new int[] { 1, 2, 3, 4, 5};
    }

    [Benchmark]
    public void InitImplicit_Benchmark()
    {
        int[] array = { 1, 2, 3, 4, 5};
        Span<int> numbers = array;
    }

    [Benchmark]
    public void InitUsingArrayLoop_Benchmark()
    {
        int length = 5;
        Span<int> numbers = new int[length];
        for (var i = 0; i < length; i++)
        {
            numbers[i] = i;
        }
    }
}