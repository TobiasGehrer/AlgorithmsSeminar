namespace Exercise07;

public class DisjointSet
{
    private readonly Dictionary<string, string> _parent = new();
    private readonly Dictionary<string, int> _rank = new();

    public void Add(string node)
    {
        _parent[node] = node;
        _rank[node] = 0;
    }

    public string Find(string node)
    {
        if (_parent[node] != node)
        {
            _parent[node] = Find(_parent[node]); // Path Compression
        }

        return _parent[node];
    }

    public void Union(string a, string b)
    {
        var rootA = Find(a);
        var rootB = Find(b);
        
        if (rootA == rootB) 
        { 
            return; 
        }

        // Union by Rank
        if (_rank[rootA] < _rank[rootB]) 
        { 
            _parent[rootA] = rootB; 
        }
        else if (_rank[rootA] > _rank[rootB]) 
        { 
            _parent[rootB] = rootA; 
        }
        else 
        { 
            _parent[rootB] = rootA; _rank[rootA]++; 
        }
    }

    public bool Connected(string a, string b)
    {
        return Find(a) == Find(b);
    }
}
