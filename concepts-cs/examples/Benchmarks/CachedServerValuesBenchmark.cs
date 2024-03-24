using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Concepts.Examples.Benchmarks;

public class CachedServerValueEntry
{
    public int Code { get; set; }
    public string Value { get; set; }
}

[MemoryDiagnoser]
public class CachedServerValuesBenchmark
{
    private Random m_random;

    public CachedServerValuesBenchmark()
    {
        m_random = new Random();
    }

    [Benchmark(Description = "PostCachedServerValuesArray")]
    public void PostCachedServerValuesBenchmarkArray()
    {
        var values = new CachedServerValueEntry[]
        {
            new CachedServerValueEntry
            {
                Code = 1,
                Value = "Value1"
            },
            new CachedServerValueEntry
            {
                Code = 2,
                Value = "Value2"
            }
        };

        PostCachedServerValuesArray(ref values);
    }

    [Benchmark(Description = "PostCachedServerValuesList")]
    public void PostCachedServerValuesBenchmarkList()
    {
        var values = new List<CachedServerValueEntry>
        {
            new CachedServerValueEntry
            {
                Code = 1,
                Value = "Value1"
            },
            new CachedServerValueEntry
            {
                Code = 2,
                Value = "Value2"
            }
        };

        PostCachedServerValuesList(values);
    }
    
    private void PostCachedServerValuesArray(ref CachedServerValueEntry[] values)
    {
        if (values == null || values.Length == 0)
        {
            return;
        }

        try
        {
            var orderedValues = values.OrderBy(v => v.Code).ToArray();
            //return Ok("Values saved successfully.");
        }
        catch (System.Exception ex)
        {
            //return BadRequest(ex.Message);
        }
    }
    
    private void PostCachedServerValuesList(List<CachedServerValueEntry> values)
    {
        if (values == null || values.Count == 0)
        {
            return;
        }

        try
        {
            var orderedValues = values.OrderBy(v => v.Code).ToList();
            // values.Clear();
            // orderedValues.Clear();
            //return Ok("Values saved successfully.");
        }
        catch (System.Exception ex)
        {
            //return BadRequest(ex.Message);
        }
    }
}