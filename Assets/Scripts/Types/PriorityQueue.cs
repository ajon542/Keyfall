using System;
using System.Collections.Generic;

// This is a basic implementation of a priority queue, nothing special.
public class PriorityQueue<T>
{
    private SortedList<int, Queue<T>> priorityQueue = new SortedList<int, Queue<T>>();

    public void Enqueue(T item, int priority)
    {
        if (!priorityQueue.ContainsKey(priority))
        {
            priorityQueue.Add(priority, new Queue<T>());
        }

        priorityQueue[priority].Enqueue(item);
    }

    public T Dequeue()
    {
        // Highest priority key should be at start of the list
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

    public int Count
    {
        get { return priorityQueue.Count; }
    }
}