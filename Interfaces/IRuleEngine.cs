using CORE;

namespace Interfaces
{
    public interface IRuleEngine
    {
        int GenerationNumber { get; }
        EvolutionOutput Evolve();
        EvolutionOutput Clear();
        int GetUniverseSize();
        ICell GetCell(int y, int x);
        void SetCell(int y, int x, bool isAlive);
        void ToggleLife(int y, int x);
    }
}
