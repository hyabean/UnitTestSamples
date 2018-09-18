using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalcTestLib
{
    public interface ICalculator
    {
        int Add(int a, int b);
        string Mode { get; set; }
        event EventHandler PoweringUp;

        event EventHandler<TestEventArgs> FellInLove;
    }

}
