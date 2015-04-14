using System;
using System.Collections.Generic;

/// <summary>
/// A basic implementation of a priority queue.
/// </summary>
/// <typeparam name="T">The items to be prioritized.</typeparam>
public class PriorityQueue<T>
{
    /// <summary>
    /// A sorted list of item queues, based on their priority.
    /// </summary>
    private SortedList<int, Queue<T>> priorityQueue = new SortedList<int, Queue<T>>();

    /// <summary>
    /// Gets a count of the remaining priority queues, not the individual items.
    /// </summary>
    public int Count
    {
        get { return priorityQueue.Count; }
    }

    /// <summary>
    /// Enqueue an item at the given priority.
    /// </summary>
    /// <param name="item">The item to prioritize.</param>
    /// <param name="priority">The priority.</param>
    public void Enqueue(T item, int priority)
    {
        if (!priorityQueue.ContainsKey(priority))
        {
            priorityQueue.Add(priority, new Queue<T>());
        }

        priorityQueue[priority].Enqueue(item);
    }

    /// <summary>
    /// Dequeue the highest priority item.
    /// </summary>
    /// <returns>The highest priority item.</returns>
    public T Dequeue()
    {
        // Highest priority key should be at start of the list.
        IList<int> keys = priorityQueue.Keys;

        if (keys.Count == 0)
        {
            throw new Exception("queue is empty");
        }

        // Dequeue the item.
        T item = priorityQueue[keys[0]].Dequeue();

        // Remove the priority key if there are no more items to remove.
        if (priorityQueue[keys[0]].Count == 0)
        {
            priorityQueue.Remove(keys[0]);
        }

        return item;
    }
}