namespace Exercise03;

public class BinarySearchTree<T> where T : IComparable<T>
{
    protected BstNode<T> Root;

    // INSERT
    public void Insert(T value)
    {
        var node = new BstNode<T>(value);

        if (Root == null)
        {
            Root = node;
            return;
        }

        var current = Root;

        while (true)
        {
            int cmp = value.CompareTo(current.Value);

            if (cmp == 0)
            {
                return; // no duplicates
            }

            if (cmp < 0)
            {
                if (current.Left == null)
                {
                    current.Left = node;
                    node.Parent = current;
                    return;
                }

                current = current.Left;
            }
            else
            {
                if (current.Right == null)
                {
                    current.Right = node;
                    node.Parent = current;
                    return;
                }

                current = current.Right;
            }
        }
    }

    // Search
    public BstNode<T> Search(T value)
    {
        var current = Root;

        while (current != null)
        {
            int cmp = value.CompareTo(current.Value);

            if (cmp == 0)
            {
                return current;
            }

            current = cmp < 0 ? current.Left : current.Right;
        }

        return null;
    }

    // Minimum
    public T Minimum() => SubtreeMin(Root).Value;

    private BstNode<T> SubtreeMin(BstNode<T> node)
    {
        while (node.Left != null)
        {
            node = node.Left;
        }

        return node;
    }

    // Minimum
    public T Maximum() => SubtreeMax(Root).Value;

    private BstNode<T> SubtreeMax(BstNode<T> node)
    {
        while (node.Right != null)
        {
            node = node.Right;
        }

        return node;
    }

    // Predecessor
    public T? Predecessor(T value)
    {
        var node = Search(value) ?? throw new Exception($"{value} not found");

        if (node.Left != null)
        {
            return SubtreeMax(node.Left).Value;
        }

        var ancestor = node.Parent;

        while (ancestor != null && node == ancestor.Left)
        {
            node = ancestor;
            ancestor = ancestor.Parent;
        }

        return ancestor != null ? ancestor.Value : default;
    }

    // Successor
    public T? Successor(T value)
    {
        var node = Search(value) ?? throw new Exception($"{value} not found");

        if (node.Right != null)
        {
            return SubtreeMin(node.Right).Value;
        }

        var ancestor = node.Parent;

        while (ancestor != null && node == ancestor.Right)
        {
            node= ancestor;
            ancestor= ancestor.Parent;
        }

        return ancestor != null ? ancestor.Value : default;
    }

    // Remove
    public void Remove(T value)
    {
        var node = Search(value) ?? throw new Exception($"{value} not found");

        if (node.Left == null)
        {
            Transplant(node, node.Right);
        }
        else if (node.Right == null)
        {
            Transplant(node, node.Left);
        }
        else
        {
            // replace with in-order successor
            var successor = SubtreeMin(node.Right);

            if (successor.Parent != node)
            {
                Transplant(successor, successor.Right);
                successor.Right = node.Right;
                successor.Right.Parent = successor;
            }

            Transplant(node, successor);
            successor.Left = node.Left;
            successor.Left.Parent = successor;
        }
    }

    private void Transplant(BstNode<T> u, BstNode<T> v)
    {
        if (u.Parent == null)
        {
            Root = v;
        }
        else if (u == u.Parent.Left)
        {
            u.Parent.Left = v;
        }
        else
        {
            u.Parent.Right = v;
        }

        if (v != null)
        {
            v.Parent = u.Parent;
        }
    }

    // Update
    // New value must be in open interval (Predecessor, Successor)
    public void Update(T oldValue, T newValue)
    {
        var node = Search(oldValue) ?? throw new Exception($"{oldValue} not found");

        T? pre = Predecessor(oldValue);
        T? suc = Successor(oldValue);

        if (pre != null && newValue.CompareTo(pre) <= 0)
        {
            throw new Exception($"New value must be > {pre}");
        }

        if (suc != null && newValue.CompareTo(suc) >= 0)
        {
            throw new Exception($"New value must be < {suc}");
        }

        node.Value = newValue;
    }

    // In-Order Traversal
    public List<T> InOrder()
    {
        var result = new List<T>();
        InOrderRec(Root, result);
        return result;
    }

    private void InOrderRec(BstNode<T> node, List<T> result)
    {
        if (node == null)
        {
            return;
        }

        InOrderRec(node.Left, result);
        result.Add(node.Value);
        InOrderRec(node.Right, result);
    }

    // Console Visualisation
    public void Print() => PrintRec(Root, "", "");

    private void PrintRec(BstNode<T> node, string indent, string label)
    {
        if (node == null)
        {
            return;
        }

        Console.WriteLine($"{indent}{label}({node.Value})");

        if (node.Left != null || node.Right != null)
        {
            PrintRec(node.Left, indent + "  ", "L:");
            PrintRec(node.Right, indent + "  ", "R:");
        }
    }
}
