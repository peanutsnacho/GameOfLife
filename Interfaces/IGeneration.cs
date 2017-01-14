namespace Interfaces
{
    public interface IGeneration
    {
        ICell[,] Universe { get; }
        int UniverseSize { get; }
        ICell GetCell(int y, int x);
        void SetCell(int y, int x, bool alive);
        void Init();
        void Clear();
    }
}
