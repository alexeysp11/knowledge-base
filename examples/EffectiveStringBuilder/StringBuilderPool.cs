using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Concepts.Examples.EffectiveStringBuilder;

/// <summary>
/// Implements the mechanism for pooling objects of type StringBuilder.
/// The class provides safe operation in a multi-threaded environment using locks and synchronization methods.
/// </summary>
public class StringBuilderPool
{
    private readonly int m_poolSize;
    private Queue<StringBuilder> m_unusedObjects;
    private List<StringBuilder> m_usedObjects;
    private object m_lockObject = new object();

    /// <summary>
    /// Default constructor.
    /// </summary>
    public StringBuilderPool(int size)
    {
        m_poolSize = size;
        m_unusedObjects = new Queue<StringBuilder>();
        m_usedObjects = new List<StringBuilder>();

        AllocateObjects();
    }

    /// <summary>
    /// Get an object from a pool using a lock.
    /// </summary>
    public StringBuilder Get()
    {
        lock (m_lockObject)
        {
            if (m_unusedObjects.Count == 0)
            {
                Monitor.Wait(m_lockObject);
            }

            var sb = m_unusedObjects.Dequeue();
            m_usedObjects.Add(sb);
            return sb;
        }
    }

    /// <summary>
    /// Move the StringBuilder object from the used collection to the unused collection.
    /// </summary>
    public void Release(StringBuilder sb)
    {
        lock (m_lockObject)
        {
            if (m_usedObjects.Contains(sb))
            {
                m_usedObjects.Remove(sb);
                m_unusedObjects.Enqueue(sb);
                Monitor.Pulse(m_lockObject);
            }
        }
    }

    /// <summary>
    /// Attempt to get an object from the pool without blocking the thread, returning false if there are no more objects.
    /// </summary>
    public bool TryGet(out StringBuilder sb)
    {
        lock (m_lockObject)
        {
            if (m_unusedObjects.Count > 0)
            {
                sb = Get();
                return true;
            }
            else
            {
                sb = null;
                return false;
            }
        }
    }

    /// <summary>
    /// Performs the creation of StringBuilder objects and populates the collection of unused objects.
    /// </summary>
    private void AllocateObjects()
    {
        for (int i = 0; i < m_poolSize; i++)
        {
            var sb = new StringBuilder();
            m_unusedObjects.Enqueue(sb);
        }
    }
}