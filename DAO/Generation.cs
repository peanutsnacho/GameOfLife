using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class Generation : IGeneration
    {
        private readonly ICell[,] _universe;

        private int _universeSize;

        public Generation(int size)
        {
            UniverseSize = size;
            _universe = new Cell[UniverseSize, UniverseSize];
        }

        public int UniverseSize
        {
            get { return _universeSize; }
            private set { _universeSize = value; }
        }

        public ICell[,] Universe
        {
            get { return _universe; }
        }

        public ICell GetCell(int y, int x)
        {
            if (x < 0 || x >= UniverseSize)
            {
                throw new ArgumentOutOfRangeException("Podany adres X komórki jest poza zakresem.");
            }

            if (y < 0 || y >= UniverseSize)
            {
                throw new ArgumentOutOfRangeException("Podany adres Y komórki jest poza zakresem.");
            }

            return Universe[y, x];
        }

        public void SetCell(int y, int x, bool alive)
        {
            Cell cell = (Cell)GetCell(y, x);

            if (cell != null)
            {
                cell.IsAlive = alive;
            }
        }

        public void Init()
        {
            for (int y = 0; y < UniverseSize; y++)
                for (int x = 0; x < UniverseSize; x++)
                    Universe[y, x] = new Cell(y, x, false);
        }

        public void Clear()
        {
            for (int y = 0; y < UniverseSize; y++)
                for (int x = 0; x < UniverseSize; x++)
                    SetCell(y, x, false);
        }

        public void ToggleLife(int y, int x)
        {
            Cell cell = (Cell)GetCell(y, x);
            cell.IsAlive = !cell.IsAlive;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 0; y < UniverseSize; y++)
            {
                for (int x = 0; x < UniverseSize; x++)
                {
                    sb.Append(  
                        string.Format("{0} ", GetCell(y, x).IsAlive ? 1 : 0)
                        );
                }
                sb.Remove(sb.Length - 1, 1); // Removes the last space 
                sb.AppendLine();
            }

            return base.ToString();
        }
    }
}
