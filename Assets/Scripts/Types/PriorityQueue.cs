using System;
using System.Collections.Generic;
using UnityEngine;

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
        // TODO:
        // Get the highest priority key.
        IList<int> keys = priorityQueue.Keys;

        foreach (int key in keys)
        {
            Debug.Log("Key: " + key);
        }
        return default(T);
    }
}