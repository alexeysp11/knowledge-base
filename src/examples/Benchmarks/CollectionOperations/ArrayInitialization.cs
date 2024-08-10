using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace KnowledgeBase.Examples.Benchmarks.CollectionOperations;

[MemoryDiagnoser]
public class ArrayInitialization
{
    [Benchmark]
    public void InitEmptyArray_UsingClass_Benchmark()
    {
        var array = (int[])System.Array.CreateInstance(typeof(int), 5);
    }

    [Benchmark]
    public void InitEmptyArray_UsingBrackets_Benchmark()
    {
        var array = new int[5];
    }

    [Benchmark]
    public void InitArray_UsingClass_LoopIndex_Benchmark()
    {
        var array = (int[])System.Array.CreateInstance(typeof(int), 5);
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i + 1;
        }
    }

    [Benchmark]
    public void InitArray_UsingClass_LoopSetValue_Benchmark()
    {
        var array = (int[])System.Array.CreateInstance(typeof(int), 5);
        for (int i = 0; i < array.Length; i++)
        {
            array.SetValue(i + 1, i);
        }
    }

    [Benchmark]
    public void InitArray_UsingBrackets_Benchmark()
    {
        var array = new int[5] { 1, 2, 3, 4, 5 };
    }

    [Benchmark]
    public void InitArray_UsingBrackets_LoopIndex_Benchmark()
    {
        var array = new int[5];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i + 1;
        }
    }

    [Benchmark]
    public void InitArray_UsingBrackets_LoopSetValue_Benchmark()
    {
        var array = new int[5];
        for (int i = 0; i < array.Length; i++)
        {
            array.SetValue(i + 1, i);
        }
    }
}