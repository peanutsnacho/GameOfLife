using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CORE
{
    public class EvolutionOutput
    {
        private bool _isFinalState;
        private int _generationNumber;

        public EvolutionOutput(int GenerationNumber, bool IsFinalState)
        {
            this.GenerationNumber = GenerationNumber;
            this.IsFinalState = IsFinalState;
        }

        public bool IsFinalState
        {
            get { return _isFinalState; }
            private set { _isFinalState = value; }
        }

        public int GenerationNumber
        {
            get { return _generationNumber; }
            set { _generationNumber = value; }
        }
    }
}
