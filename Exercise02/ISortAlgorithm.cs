namespace Exercise02
{
    public interface ISortAlgorithm
    {
        string Name { get; }
        int Steps { get; }
        TimeSpan ElapsedTime { get; }
        void Sort(int[] array);
    }
}
