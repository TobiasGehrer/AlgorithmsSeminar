namespace Exercise03;

class Program
{
    static void Main(string[] args)
    {
        // BST Demo
        var bst = new BinarySearchTree<int>();

        foreach (var v in new[] { 55, 40, 60, 28, 95, 10, 30, 88, 99, 68, 90, 63, 84, 98 })
        { 
            bst.Insert(v); 
        }

        Console.WriteLine("=== BST ===");
        bst.Print();
        Console.WriteLine($"Min: {bst.Minimum()}  Max: {bst.Maximum()}");
        Console.WriteLine($"Predecessor(55): {bst.Predecessor(55)}  Successor(55): {bst.Successor(55)}");
        Console.WriteLine($"In-Order: [{string.Join(", ", bst.InOrder())}]");
        Console.WriteLine("\nUpdate(28, 29):"); bst.Update(28, 29); bst.Print();
        Console.WriteLine("\nRemove(55):"); bst.Remove(55); bst.Print();

        // AVL Demo
        Console.WriteLine("\n=== AVL ===");
        var avl = new AvlTree<int>();
        // inserting in order that would make BST degenerate → AVL stays balanced
        foreach (var v in new[] { 10, 20, 30, 40, 50, 25 })          
        { 
            avl.Insert(v); 
        }

        avl.Print();
        Console.WriteLine($"In-Order: [{string.Join(", ", avl.InOrder())}]");
        Console.WriteLine("\nRemove(30):"); avl.Remove(30); avl.Print();

        // Heap Demo
        Console.WriteLine("\n=== Min-Heap ===");
        var heap = new MinHeap<int>();

        foreach (var v in new[] { 9, 5, 8, 3, 7, 4 })            
        { 
            heap.Insert(v); 
        }

        heap.Print();

        Console.WriteLine($"ExtractMin: {heap.ExtractMin()}"); heap.Print();
        Console.WriteLine($"ExtractMin: {heap.ExtractMin()}"); heap.Print();
    }
}
