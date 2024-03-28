using System;
using System.Collections.Generic;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Concepts.Examples.Benchmarks.CollectionOperations;

/// <summary>
/// 
/// </summary>
internal class ValueEntryClass
{
    public int Code { get; set; }
    public string Value { get; set; }
}

/// <summary>
/// 
/// </summary>
internal struct ValueEntryStruct
{
    public int Code { get; set; }
    public string Value { get; set; }
}

/// <summary>
/// 
/// </summary>
internal class ValueEntrySortingMethods
{
    #region Sorting using LINQ
    public void ClassRefArray_SortLinqToArray(ref ValueEntryClass[] values)
    {
        if (values == null || values.Length == 0)
        {
            return;
        }

        var orderedValues = values.OrderBy(v => v.Code).ToArray();
    }
    
    public void ClassRefArray_SortLinqToList(ref ValueEntryClass[] values)
    {
        if (values == null || values.Length == 0)
        {
            return;
        }

        var orderedValues = values.OrderBy(v => v.Code).ToList();
    }
    
    public void ClassList_SortLinqToArray(List<ValueEntryClass> values)
    {
        if (values == null || values.Count == 0)
        {
            return;
        }

        var orderedValues = values.OrderBy(v => v.Code).ToArray();
    }
    
    public void ClassList_SortLinqToList(List<ValueEntryClass> values)
    {
        if (values == null || values.Count == 0)
        {
            return;
        }

        var orderedValues = values.OrderBy(v => v.Code).ToList();
    }
    #endregion  // Sorting using LINQ

    #region Sorting using bubble sort
    public void ClassRefArray_BubbleSort(ref ValueEntryClass[] values)
    {
        if (values == null || values.Length == 0)
        {
            return;
        }

        for (int i = 0; i < values.Length - 1; i++)
        {
            for (int j = 0; j < values.Length - i - 1; j++)
            {
                if (values[j].Code > values[j + 1].Code)
                {
                    var temp = values[j];
                    values[j] = values[j + 1];
                    values[j + 1] = temp;
                }
            }
        }
    }

    public void ClassList_BubbleSort(List<ValueEntryClass> values)
    {
        if (values == null || values.Count == 0)
        {
            return;
        }

        for (int i = 0; i < values.Count - 1; i++)
        {
            for (int j = 0; j < values.Count - i - 1; j++)
            {
                if (values[j].Code > values[j + 1].Code)
                {
                    var temp = values[j];
                    values[j] = values[j + 1];
                    values[j + 1] = temp;
                }
            }
        }
    }

    public void ClassSpan_BubbleSort(Span<ValueEntryClass> values)
    {
        if (values == null || values.Length == 0)
        {
            return;
        }

        for (int i = 0; i < values.Length - 1; i++)
        {
            for (int j = 0; j < values.Length - i - 1; j++)
            {
                if (values[j].Code > values[j + 1].Code)
                {
                    var temp = values[j];
                    values[j] = values[j + 1];
                    values[j + 1] = temp;
                }
            }
        }
    }

    public void ClassRefSpan_BubbleSort(ref Span<ValueEntryClass> values)
    {
        if (values == null || values.Length == 0)
        {
            return;
        }

        for (int i = 0; i < values.Length - 1; i++)
        {
            for (int j = 0; j < values.Length - i - 1; j++)
            {
                if (values[j].Code > values[j + 1].Code)
                {
                    var temp = values[j];
                    values[j] = values[j + 1];
                    values[j + 1] = temp;
                }
            }
        }
    }

    public void StructRefArray_BubbleSort(ref ValueEntryStruct[] values)
    {
        if (values == null || values.Length == 0)
        {
            return;
        }

        for (int i = 0; i < values.Length - 1; i++)
        {
            for (int j = 0; j < values.Length - i - 1; j++)
            {
                if (values[j].Code > values[j + 1].Code)
                {
                    var temp = values[j];
                    values[j] = values[j + 1];
                    values[j + 1] = temp;
                }
            }
        }
    }

    public void StructList_BubbleSort(List<ValueEntryStruct> values)
    {
        if (values == null || values.Count == 0)
        {
            return;
        }

        for (int i = 0; i < values.Count - 1; i++)
        {
            for (int j = 0; j < values.Count - i - 1; j++)
            {
                if (values[j].Code > values[j + 1].Code)
                {
                    var temp = values[j];
                    values[j] = values[j + 1];
                    values[j + 1] = temp;
                }
            }
        }
    }

    public void StructSpan_BubbleSort(Span<ValueEntryStruct> values)
    {
        if (values == null || values.Length == 0)
        {
            return;
        }

        for (int i = 0; i < values.Length - 1; i++)
        {
            for (int j = 0; j < values.Length - i - 1; j++)
            {
                if (values[j].Code > values[j + 1].Code)
                {
                    var temp = values[j];
                    values[j] = values[j + 1];
                    values[j + 1] = temp;
                }
            }
        }
    }

    public void StructRefSpan_BubbleSort(ref Span<ValueEntryStruct> values)
    {
        if (values == null || values.Length == 0)
        {
            return;
        }

        for (int i = 0; i < values.Length - 1; i++)
        {
            for (int j = 0; j < values.Length - i - 1; j++)
            {
                if (values[j].Code > values[j + 1].Code)
                {
                    var temp = values[j];
                    values[j] = values[j + 1];
                    values[j + 1] = temp;
                }
            }
        }
    }
    #endregion  // Sorting using bubble sort
}

/// <summary>
/// A class for testing the execution speed of methods and memory allocation 
/// when passing various collections of data structures and sorting them.
/// </summary>
[MemoryDiagnoser]
public class ValueEntryPassAndSortBenchmark
{
    private Random m_random;
    private ValueEntrySortingMethods m_sortingMethods;
    private ValueEntryClass[] m_valuesClassArray;
    private List<ValueEntryClass> m_valuesClassList;
    private ValueEntryStruct[] m_valuesStructArray;
    private List<ValueEntryStruct> m_valuesStructList;

    /// <summary>
    /// Default constructor.
    /// </summary>
    public ValueEntryPassAndSortBenchmark()
    {
        m_random = new Random();
        m_sortingMethods = new ValueEntrySortingMethods();
        InitializeCollections();    
    }

    #region Benchmark methods
    /// <summary>
    /// 
    /// </summary>
    [Benchmark(Description = "ClassRefArray_SortLinqToArray")]
    public void ClassRefArray_SortLinqToArray_Benchmark()
    {
        m_sortingMethods.ClassRefArray_SortLinqToArray(ref m_valuesClassArray);
    }

    /// <summary>
    /// 
    /// </summary>
    [Benchmark(Description = "ClassRefArray_SortLinqToList")]
    public void ClassRefArray_SortLinqToList_Benchmark()
    {
        m_sortingMethods.ClassRefArray_SortLinqToList(ref m_valuesClassArray);
    }

    /// <summary>
    /// 
    /// </summary>
    [Benchmark(Description = "ClassList_SortLinqToArray")]
    public void ClassList_SortLinqToArray_Benchmark()
    {
        m_sortingMethods.ClassList_SortLinqToArray(m_valuesClassList);
    }

    /// <summary>
    /// 
    /// </summary>
    [Benchmark(Description = "ClassList_SortLinqToList")]
    public void ClassList_SortLinqToList_Benchmark()
    {
        m_sortingMethods.ClassList_SortLinqToList(m_valuesClassList);
    }

    /// <summary>
    /// 
    /// </summary>
    [Benchmark(Description = "ClassRefArray_BubbleSort")]
    public void ClassRefArray_BubbleSort_Benchmark()
    {
        m_sortingMethods.ClassRefArray_BubbleSort(ref m_valuesClassArray);
    }

    /// <summary>
    /// 
    /// </summary>
    [Benchmark(Description = "ClassList_BubbleSort")]
    public void ClassList_BubbleSort_Benchmark()
    {
        m_sortingMethods.ClassList_BubbleSort(m_valuesClassList);
    }

    /// <summary>
    /// 
    /// </summary>
    [Benchmark(Description = "ClassSpan_BubbleSort")]
    public void ClassSpan_BubbleSort_Benchmark()
    {
        Span<ValueEntryClass> span = m_valuesClassArray;
        m_sortingMethods.ClassSpan_BubbleSort(span);
    }

    /// <summary>
    /// 
    /// </summary>
    [Benchmark(Description = "ClassRefSpan_BubbleSort")]
    public void ClassRefSpan_BubbleSort_Benchmark()
    {
        Span<ValueEntryClass> span = m_valuesClassArray;
        m_sortingMethods.ClassRefSpan_BubbleSort(ref span);
    }

    /// <summary>
    /// 
    /// </summary>
    [Benchmark(Description = "StructRefArray_BubbleSort")]
    public void StructRefArray_BubbleSort_Benchmark()
    {
        m_sortingMethods.StructRefArray_BubbleSort(ref m_valuesStructArray);
    }

    /// <summary>
    /// 
    /// </summary>
    [Benchmark(Description = "StructList_BubbleSort")]
    public void StructList_BubbleSort_Benchmark()
    {
        m_sortingMethods.StructList_BubbleSort(m_valuesStructList);
    }

    /// <summary>
    /// 
    /// </summary>
    [Benchmark(Description = "StructSpan_BubbleSort")]
    public void StructSpan_BubbleSort_Benchmark()
    {
        Span<ValueEntryStruct> span = m_valuesStructArray;
        m_sortingMethods.StructSpan_BubbleSort(span);
    }

    /// <summary>
    /// 
    /// </summary>
    [Benchmark(Description = "StructRefSpan_BubbleSort")]
    public void StructRefSpan_BubbleSort_Benchmark()
    {
        Span<ValueEntryStruct> span = m_valuesStructArray;
        m_sortingMethods.StructRefSpan_BubbleSort(ref span);
    }
    #endregion  // Benchmark methods
    
    #region Initialization methods
    private void InitializeCollections()
    {
        // Classes.
        m_valuesClassArray = new ValueEntryClass[]
        {
            new ValueEntryClass
            {
                Code = 1,
                Value = "Value1"
            },
            new ValueEntryClass
            {
                Code = 2,
                Value = "Value2"
            }
        };
        m_valuesClassList = new List<ValueEntryClass>
        {
            new ValueEntryClass
            {
                Code = 1,
                Value = "Value1"
            },
            new ValueEntryClass
            {
                Code = 2,
                Value = "Value2"
            }
        };

        // Structures.
        m_valuesStructArray = new ValueEntryStruct[]
        {
            new ValueEntryStruct
            {
                Code = 1,
                Value = "Value1"
            },
            new ValueEntryStruct
            {
                Code = 2,
                Value = "Value2"
            }
        };
        m_valuesStructList = new List<ValueEntryStruct>
        {
            new ValueEntryStruct
            {
                Code = 1,
                Value = "Value1"
            },
            new ValueEntryStruct
            {
                Code = 2,
                Value = "Value2"
            }
        };
    }
    #endregion  // Initialization methods
}