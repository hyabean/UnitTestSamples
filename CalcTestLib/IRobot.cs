using System;

namespace CalcTestLib
{
    public class TestEventArgs : System.EventArgs
    {
        public int Type { get; set; }
    }

    public delegate void TestDelegate(object a, EventArgs b);

    public interface IRobot
    {
        event EventHandler<TestEventArgs> FellInLove;
    }
}