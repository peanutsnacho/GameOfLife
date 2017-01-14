namespace Interfaces
{
    public interface ICell
    {
        int X { get; }
        int Y { get; }
        bool IsAlive { get; set; }
    }
}
