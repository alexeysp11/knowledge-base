using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Threading;

namespace Concepts.Examples.Benchmarks.StructureBenchmarks;

internal struct MyStruct
{
    public int Number;
    public int Value;

    public MyStruct(int number, int value)
    {
        Number = number;
        Value = value;
    }
}

/// <summary>
/// A class for testing performance when using initializing structures.
/// </summary>
[MemoryDiagnoser]
public class StructureInitialization
{
    [Benchmark]
    public void InitializeConstructor()
    {
        MyStruct myStruct = new MyStruct(10, 11);
    }

    [Benchmark]
    public void InitializeFields()
    {
        MyStruct myStruct = new MyStruct { Number = 10, Value = 11 };
    }

    [Benchmark]
    public void InitializeImplicitConstructor()
    {
        MyStruct myStruct;
        myStruct.Number = 10;
        myStruct.Value = 11;
    }

    [Benchmark]
    public void InitializeDefault()
    {
        MyStruct myStruct = default;
    }
}