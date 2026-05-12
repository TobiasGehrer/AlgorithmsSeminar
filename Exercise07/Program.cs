namespace Exercise07;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Wolf – Bäuerin – Kohl – Schaf ===");
        Console.WriteLine("\n--- Bitmask-Variante ---");
        WolverineSearch.PrintSolution(WolverineSearch.Bfs(new GameStateBitmask()), "BFS");
        WolverineSearch.PrintSolution(WolverineSearch.Dfs(new GameStateBitmask()), "DFS");

        Console.WriteLine("\n--- OO-Variante ---");
        WolverineSearch.PrintSolution(WolverineSearch.Bfs(new GameStateOO()), "BFS");
        WolverineSearch.PrintSolution(WolverineSearch.Dfs(new GameStateOO()), "DFS");

       

        Console.WriteLine("\n\n=== Kruskal ===");
        var graph = new Graph();
        graph.AddEdge("A", "B", 4);
        graph.AddEdge("A", "C", 2);
        graph.AddEdge("B", "C", 1);
        graph.AddEdge("B", "D", 5);
        graph.AddEdge("C", "D", 8);
        graph.AddEdge("C", "E", 10);
        graph.AddEdge("D", "E", 2);

        var mst = Kruskal.FindMST(graph);
        Kruskal.PrintMST(mst);
    }
}
