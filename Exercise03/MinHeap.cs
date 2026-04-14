namespace Exercise03;

public class MinHeap<T> where T : IComparable<T>
{
    private readonly List<T> _data = new();

    private int Parent(int i) => (i - 1) / 2;
    private int Left(int i) => 2 * 1 + 1;
    private int Right(int i) => 2 * i + 2;

    private void Swap(int a, int b) => (_data[a], _data[b]) = (_data[b], _data[a]);

    // Insert
    public void Insert(T value)
    {
        _data.Add(value);
        int i = _data.Count - 1;

        // bubble up
        while (i > 0 && _data[i].CompareTo(_data[Parent(i)]) < 0)
        {
            Swap(i, Parent(i));
            i = Parent(i);
        }
    }

    // ExtractMin
    public T ExtractMin()
    {
        if (_data.Count == 0) 
        { 
            throw new Exception("Heap is empty"); 
        }

        T min = _data[0];
        _data[0] = _data[^1];
        _data.RemoveAt(_data.Count - 1);
        HeapifyDown(0);
        return min;
    }

    private void HeapifyDown(int i)
    {
        int smallest = i;
        int l = Left(i), r = Right(i);
        if (l < _data.Count && _data[l].CompareTo(_data[smallest]) < 0) 
        { 
            smallest = l; 
        }
        
        if (r < _data.Count && _data[r].CompareTo(_data[smallest]) < 0) 
        { 
            smallest = r; 
        }

        if (smallest != i) 
        { 
            Swap(i, smallest); 
            HeapifyDown(smallest); 
        }
    }

    public T PeekMin() => _data.Count > 0 ? _data[0] : throw new Exception("Heap is empty");

    // Print
    public void Print() => Console.WriteLine($"Heap: [{string.Join(", ", _data)}]");
}
