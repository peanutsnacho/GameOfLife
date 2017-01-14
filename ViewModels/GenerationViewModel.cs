using CORE;
using DAO;
using Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ViewModels
{
    class GenerationViewModel : INotifyPropertyChanged
    {
        private readonly IRuleEngine ruleEngine;
        private int _pupulationSize;
        private int _generationNumber;
        private bool _isFinalState;

        public GenerationViewModel(int universeSize)
        {
            ruleEngine = new ConowayRuleEngine(new Generation(universeSize));

            EvolveCommand = new RelayCommand<object>(_ => Evolve(), _ => CanEvolve());
            ResetCommand = new RelayCommand<object>(_ => ResetGame(), _ => CanResetGame());
            ToggleLifeCommand = new RelayCommand<string>((cellRowColumn) => ToggleLife(cellRowColumn), _ => CanToggleLife());
        }

        private void Evolve()
        {
            EvolutionOutput output = ruleEngine.Evolve();
            GenerationNumber = output.GenerationNumber;
            IsFinalState = output.IsFinalState;
        }

        private bool CanEvolve()
        {
            return !IsFinalState;
        }

        private void ResetGame()
        {
            EvolutionOutput output = ruleEngine.Clear();
            GenerationNumber = output.GenerationNumber;
            IsFinalState = output.IsFinalState;
        }

        private bool CanResetGame()
        {
            return GenerationNumber > 1 || IsFinalState;
        }

        private void ToggleLife(string cellYX)
        {
            string[] split = cellYX.Split(',');
            int y = int.Parse(split[0]);
            int x = int.Parse(split[1]);
            ruleEngine.ToggleLife(y, x);
        }

        private bool CanToggleLife()
        {
            return GenerationNumber > 1 || IsFinalState;
        }

        public int UniverseSize
        {
            get { return ruleEngine.GetUniverseSize(); }
        }

        public int PupulationSize
        {
            get { return _pupulationSize; }
            private set
            {
                _pupulationSize = value;
                RaisePropertyChanged();
            }
        }

        public int GenerationNumber
        {
            get { return _generationNumber; }
            private set
            {
                _generationNumber = value;
                RaisePropertyChanged();
            }
        }

        public bool IsFinalState
        {
            get { return _isFinalState; }
            private set
            {
                _isFinalState = value;
                RaisePropertyChanged();
            }
        }

        public RelayCommand<object> EvolveCommand { get; private set; }
        public RelayCommand<object> ResetCommand { get; private set; }
        public RelayCommand<string> ToggleLifeCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
