using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface ICell
    {
        int X { get; }
        int Y { get; }
        bool IsAlive { get; set; }
    }
}
