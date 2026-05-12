namespace Exercise07;

public enum Side
{
    Left,
    Right
}

public class GameStateOO : IGameState<GameStateOO>
{
    private readonly Side[] _positions; // [Farmer, Wolf, Sheep, Cabbage]

    private const int FarmerIdx = 0;
    private const int WolfIdx = 1;
    private const int SheepIdx = 2;
    private const int CabbageIdx = 3;

    public GameStateOO(Side farmer = Side.Left, Side wolf = Side.Left, Side sheep = Side.Left, Side cabbage = Side.Left)
    {
        _positions = [farmer, wolf, sheep, cabbage];
    }

    private GameStateOO(Side[] positions)
    {
        _positions = (Side[])positions.Clone();
    }

    public bool IsValid()
    {
        Side f = _positions[FarmerIdx];

        if (_positions[WolfIdx] == _positions[SheepIdx] && _positions[WolfIdx] != f)
        {
            return false;
        }

        if (_positions[SheepIdx] == _positions[CabbageIdx] && _positions[SheepIdx] != f)
        {
            return false;
        }

        return true;
    }

    public bool IsTerminationState()
    {
        return _positions.All(p => p == Side.Right);
    }

    public List<GameStateOO> GetNeighbors()
    {
        var neighbors = new List<GameStateOO>();
        Side newFarmer = _positions[FarmerIdx] == Side.Left ? Side.Right : Side.Left;

        // Allein fahren
        var alone = (Side[])_positions.Clone();
        alone[FarmerIdx] = newFarmer;
        neighbors.Add(new GameStateOO(alone));

        // Mit Cargo fahren
        foreach (int cargo in new[] { WolfIdx, SheepIdx, CabbageIdx })
        {
            if (_positions[cargo] == _positions[FarmerIdx])
            {
                var withCargo = (Side[])_positions.Clone();
                withCargo[FarmerIdx] = newFarmer;
                withCargo[cargo] = newFarmer;
                neighbors.Add(new GameStateOO(withCargo));
            }
        }

        return neighbors.Where(s => s.IsValid()).ToList();
    }

    public override string ToString()
    {
        string S(int i) => _positions[i] == Side.Right ? "R" : "L";
        return $"Bäuerin:{S(FarmerIdx)} Wolf:{S(WolfIdx)} Schaf:{S(SheepIdx)} Kohl:{S(CabbageIdx)}";
    }

    public override bool Equals(object? obj)
    {
        return obj is GameStateOO o && _positions.SequenceEqual(o._positions);
    }

    public override int GetHashCode()
    {
        return _positions.Aggregate(0, (hash, p) => hash * 2 + (int)p);
    }
}
