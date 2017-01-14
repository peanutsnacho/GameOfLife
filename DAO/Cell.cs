using Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DAO
{
    public class Cell : ICell, INotifyPropertyChanged
    {
        private bool _isAlive;
        private int _x;
        private int _y;

        public Cell(int col, int row, bool alive)
        {
            X = col;
            Y = row;
            IsAlive = alive;
        }

        public bool IsAlive
        {
            get { return _isAlive; }
            set
            {
                _isAlive = value;
                RaisePropertyChanged();
            }
        }

        public int X
        {
            get { return _x; }
            private set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            private set { _y = value; }

        }

        public override string ToString()
        {
            return string.Format("Komórka [{0},{1}] | {2} ", X, Y, IsAlive ? "Żywa" : "Martwa");
        }

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
