using UnityEngine;

public class SelectStack<T>
{
    private T[] values;
    private int capacity;
    private int top;

    public T[] Values { get => values; set => values = value; }
    public int Capacity { get => capacity; set => capacity = value; }

    public SelectStack(int capacity)
    {
        this.capacity = capacity;
        SetCapacity(capacity);
    }
    public SelectStack(T[] values)
    {
        CloneValues(values);
    }

    public void Push(T value, int pushIndex = 0)
    {
        T[] oldValues = CloneValues(values);
        values[pushIndex] = value;
        for (int index = pushIndex; index < values.Length - 1; index++)
            values[index + 1] = oldValues[index];
    }
    public T Pop()
    {
        T returnValue = values[values.Length];
        values[values.Length - 1] = default(T);
        return returnValue;
    }
    public T Pop(int popIndex)
    {
        T returnValue = values[popIndex];
        for (int index = popIndex; popIndex < values.Length - 1; index++)
            values[index] = values[index + 1];
        values[values.Length - 1] = default(T);
        return returnValue;
    }
    public void Clear()
    {
        for (int index = 0; index < values.Length; index++)
            values[index] = default(T);
    }
    public bool IsFull()
    {
        bool isFull = true;
        for (int index = 0; index < values.Length; index++)
        {
            if (values[index].Equals(default(T)))
                isFull = false;
        }
        return isFull;
    }
    private void SetCapacity(int capacity)
    {
        values = new T[capacity];
    }
    private T[] CloneValues(T[] values)
    {
        T[] clonedValues = new T[values.Length];
        for (int index = 0; index < values.Length; index++)
            clonedValues[index] = values[index];
        return clonedValues;
    }
}
