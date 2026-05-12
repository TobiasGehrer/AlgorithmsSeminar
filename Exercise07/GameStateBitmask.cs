namespace Exercise07;

// Bit 0 = Bäuerin, Bit 1 = Wolf, Bit 2 = Schaf, Bit 3 = Kohl
// 0 = links, 1 = rechts
public class GameStateBitmask : IGameState<GameStateBitmask>
{
    private readonly byte _state;

    private const byte Farmer = 1 << 0;
    private const byte Wolf = 1 << 1;
    private const byte Sheep = 1 << 2;
    private const byte Cabbage = 1 << 3;

    public GameStateBitmask(byte state = 0)
    {
        _state = state;
    } 

    private bool IsRight(byte mask) 
    { 
        return (_state & mask) != 0; 
    }

    public bool IsValid()
    {
        bool farmerRight = IsRight(Farmer);

        // Schaf und Wolf alleine auf einer Seite
        if (IsRight(Wolf) == IsRight(Sheep) && IsRight(Wolf) != farmerRight)
        {
            return false;
        }

        // Schaf und Kohl allein auf gleicher Seite
        if (IsRight(Sheep) == IsRight(Cabbage) && IsRight(Sheep) != farmerRight)
        {
            return false;
        }

        return true;
    }

    public bool IsTerminationState() 
    { 
        return _state == (Farmer | Wolf | Sheep | Cabbage); 
    }

    public List<GameStateBitmask> GetNeighbors()
    {
        var neighbors = new List<GameStateBitmask>();
        byte farmerBit = Farmer;

        // Bäuerin fährt allein
        neighbors.Add(new GameStateBitmask((byte)(_state ^ farmerBit)));

        // Bäuerin fährt mit Wolf, Schaf oder Kohl (nur wenn auf gleicher Seite)
        foreach (var cargo in new[] { Wolf, Sheep, Cabbage })
        {
            if (IsRight(cargo) == IsRight(Farmer))
                neighbors.Add(new GameStateBitmask((byte)(_state ^ farmerBit ^ cargo)));
        }

        return neighbors.Where(s => s.IsValid()).ToList();
    }

    public override string ToString()
    {
        string Side(byte mask) => IsRight(mask) ? "R" : "L";
        return $"Bäuerin:{Side(Farmer)} Wolf:{Side(Wolf)} Schaf:{Side(Sheep)} Kohl:{Side(Cabbage)}";
    }

    public override bool Equals(object? obj)
    { 
        return obj is GameStateBitmask o && o._state == _state; 
    }

    public override int GetHashCode() 
    { 
        return _state; 
    }
}
