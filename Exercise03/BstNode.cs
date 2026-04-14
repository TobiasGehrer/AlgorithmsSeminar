namespace Exercise03;

public class BstNode<T> where T : IComparable<T>
{
    public T Value;
    public BstNode<T> Left, Right, Parent;

    public BstNode(T value) => Value = value;
}
