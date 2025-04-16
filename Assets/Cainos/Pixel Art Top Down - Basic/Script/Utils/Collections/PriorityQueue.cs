using System;
using System.Collections.Generic;

public class PriorityQueue<T>
{
    private List<(T item, float priority)> heap = new List<(T, float)>();

    public int Count => heap.Count;

    public void Enqueue(T item, float priority)
    {
        heap.Add((item, priority));
        HeapifyUp(heap.Count - 1);
    }

    public T Dequeue()
    {
        if (heap.Count == 0) throw new InvalidOperationException("Queue is empty");

        T item = heap[0].item;
        heap[0] = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        HeapifyDown(0);
        return item;
    }

    public T Peek()
    {
        if (heap.Count == 0) throw new InvalidOperationException("Queue is empty");
        return heap[0].item;
    }

    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parent = (index - 1) / 2;
            if (heap[index].priority >= heap[parent].priority) break;

            (heap[index], heap[parent]) = (heap[parent], heap[index]);
            index = parent;
        }
    }

    private void HeapifyDown(int index)
    {
        int last = heap.Count - 1;
        while (true)
        {
            int left = 2 * index + 1, right = 2 * index + 2;
            int smallest = index;

            if (left <= last && heap[left].priority < heap[smallest].priority)
                smallest = left;
            if (right <= last && heap[right].priority < heap[smallest].priority)
                smallest = right;

            if (smallest == index) break;

            (heap[index], heap[smallest]) = (heap[smallest], heap[index]);
            index = smallest;
        }
    }
}