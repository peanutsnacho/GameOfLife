using CORE;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO
{
    public class ConowayRuleEngine : IRuleEngine
    {
        private Generation _generation;
        private int _generationNumber;
        private const int underpopulation = 2,
            overpopulation = 3,
            reproduction = 3;

        public ConowayRuleEngine(Generation gen)
        {
            Generation = gen;
            GenerationNumber = 1;
        }

        public Generation Generation
        {
            get { return _generation; }
            set { _generation = value; }
        }

        public int GenerationNumber
        {
            get { return _generationNumber; }
            private set { _generationNumber = value; }
        }

        private int CountLiveNeighbours(Generation generation, Cell cell)
        {
            int liveNeighbours = 0;
            int x, y, nextX, prevX, nextY, prevY;

            x = cell.X;
            y = cell.Y;

            prevX = x - 1;
            nextX = x + 1;
            if (x == 0)
                prevX = GetUniverseSize() - 1;
            else if (x == GetUniverseSize() - 1)
                nextX = 0;

            prevY = y - 1;
            nextY = y + 1;
            if (y == 0)
                prevY = GetUniverseSize() - 1;
            else if (y == GetUniverseSize() - 1)
                nextY = 0;


            IEnumerable<Cell> neighbours = new List<Cell> {
                (Cell)GetCell(nextY, prevX),
                (Cell)GetCell(nextY, x),
                (Cell)GetCell(nextY, nextX),
                (Cell)GetCell(y, prevX),
                (Cell)GetCell(y, nextX),
                (Cell)GetCell(prevY, prevX),
                (Cell)GetCell(prevY, x),
                (Cell)GetCell(prevY, nextX)
                };

            foreach (Cell neighbour in neighbours)
            {
                if (neighbour != null && neighbour.IsAlive)
                {
                    liveNeighbours++;
                }
            }

            return liveNeighbours;
        }

        public EvolutionOutput Clear()
        {
            Generation.Clear();
            GenerationNumber = 1;

            return new EvolutionOutput(GenerationNumber, false);

        }

        public EvolutionOutput Evolve() //applies Conoway rules
        {
            List<Cell> changeLifeCells = new List<Cell>();
            Cell cell;
            int liveNeighbours;

            for (int y = 0; y < Generation.UniverseSize; y++)
            {
                for (int x = 0; x < Generation.UniverseSize; x++)
                {
                    cell = (Cell)Generation.GetCell(y, x);
                    liveNeighbours = CountLiveNeighbours(Generation, cell);

                    if (cell.IsAlive && (liveNeighbours < underpopulation || liveNeighbours > overpopulation))
                    {
                        changeLifeCells.Add(cell);
                    }
                    else if (!cell.IsAlive && (liveNeighbours == reproduction))
                    {
                        changeLifeCells.Add(cell);
                    }
                }
            }

            if (changeLifeCells.Any())
            {
                GenerationNumber++;
                changeLifeCells.ForEach(
                    var => ToggleLife(var.Y, var.X));
            }

            return new EvolutionOutput(GenerationNumber, !changeLifeCells.Any());
        }

        public ICell GetCell(int y, int x)
        {
            return Generation.GetCell(y, x);
        }

        public int GetUniverseSize()
        {
            return Generation.UniverseSize;
        }

        public void SetCell(int y, int x, bool isAlive)
        {
            Generation.SetCell(y, x, isAlive);
        }

        public void ToggleLife(int y, int x)
        {
            Generation.ToggleLife(y, x);
        }
    }
}
