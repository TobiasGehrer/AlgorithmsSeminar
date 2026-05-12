namespace Exercise07;

public interface IGameState<T> where T : IGameState<T>
{
    bool IsValid();
    bool IsTerminationState();
    List<T> GetNeighbors();
}
