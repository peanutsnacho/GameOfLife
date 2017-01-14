using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
