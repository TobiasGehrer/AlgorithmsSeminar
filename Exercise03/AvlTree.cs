namespace Exercise03;

public class AvlTree<T> where T : IComparable<T>
{
    private AvlNode<T> Root;

    // Insert
    public void Insert(T value) => Root = Insert(Root, value);

    private AvlNode<T> Insert(AvlNode<T> node, T value)
    {
        if (node == null)
        {
            return new AvlNode<T>(value);
        }

        int cmp = value.CompareTo(node.Value);

        if (cmp < 0)
        {
            node.Left = Insert(node.Left, value);
        }
        else if (cmp > 0)
        {
            node.Right = Insert(node.Right, value);
        }
        else
        {
            return node; // no duplicates
        }

        UpdateHeight(node);
        return Rebalance(node);
    }

    // Remove
    public void Remove(T value) => Root = Insert(Root, value);

    private AvlNode<T> Remove(AvlNode<T> node, T value)
    {
        if (node == null)
        {
            throw new Exception($"{value} not found");
        }

        int cmp = value.CompareTo(node.Value);

        if (cmp < 0) 
        { 
            node.Left = Remove(node.Left, value); 
        }
        else if (cmp > 0) 
        { 
            node.Right = Remove(node.Right, value); 
        }
        else
        {
            if (node.Left == null)
            {
                return node.Right;
            }
            if (node.Right == null) 
            { 
                return node.Left; 
            }

            // replace with in-order successor
            var successor = SubtreeMin(node.Right);
            node.Value = successor.Value;
            node.Right = Remove(node.Right, successor.Value);
        }

        UpdateHeight(node);
        return Rebalance(node);
    }

    // Rebalance
    private AvlNode<T> Rebalance(AvlNode<T> node)
    {
        int bf = BalanceFactor(node);

        if (bf > 1)  // left-heavy
        {
            if (BalanceFactor(node.Left) < 0)       // LR case               
            { 
                node.Left = RotateLeft(node.Left); 
            }

            return RotateRight(node);               // LL case
        }

        if (bf < -1) // right-heavy
        {
            if (BalanceFactor(node.Right) > 0)      // RL case
            { 
                node.Right = RotateRight(node.Right); 
            }
            
            return RotateLeft(node);                // RR case
        }

        return node;
    }

    // Rotations
    private AvlNode<T> RotateRight(AvlNode<T> y)
    {
        var x = y.Left;
        y.Left = x.Right;
        x.Right = y;
        UpdateHeight(y);
        UpdateHeight(x);
        return x;
    }

    private AvlNode<T> RotateLeft(AvlNode<T> x)
    {
        var y = x.Right;
        x.Right = y.Left;
        y.Left = x;
        UpdateHeight(x);
        UpdateHeight(y);
        return y;
    }

    // Helpers
    private int Height(AvlNode<T> n) => n?.Height ?? 0;

    private int BalanceFactor(AvlNode<T> n) => Height(n.Left) - Height(n.Right);

    private void UpdateHeight(AvlNode<T> n) => n.Height = 1 + Math.Max(Height(n.Left), Height(n.Right));

    private AvlNode<T> SubtreeMin(AvlNode<T> n) 
    { 
        while (n.Left != null)            
        {
            n = n.Left;            
        }

        return n;
    }

    // In-Order Traversal
    public List<T> InOrder()
    {
        var result = new List<T>();
        InOrderRec(Root, result);
        return result;
    }

    private void InOrderRec(AvlNode<T> node, List<T> result)
    {
        if (node == null) 
        { 
            return; 
        }

        InOrderRec(node.Left, result);
        result.Add(node.Value);
        InOrderRec(node.Right, result);
    }

    // Print
    public void Print() => PrintRec(Root, "", "");
    private void PrintRec(AvlNode<T> node, string indent, string label)
    {
        if (node == null) 
        { 
            return; 
        }

        Console.WriteLine($"{indent}{label}({node.Value} h{node.Height})");

        if (node.Left != null || node.Right != null)
        {
            PrintRec(node.Left, indent + "  ", "L:");
            PrintRec(node.Right, indent + "  ", "R:");
        }
    }
}
