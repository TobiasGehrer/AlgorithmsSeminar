namespace Exercise03;

public class AvlNode<T> where T : IComparable<T>
{
    public T Value;
    public AvlNode<T> Left, Right;
    public int Height = 1;

    public AvlNode(T value) => Value = value;
}
