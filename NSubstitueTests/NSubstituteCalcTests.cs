using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalcTestLib;
using NUnit;
using NSubstitute;
using NUnit.Framework;

namespace ProfitLossServer.UnitTests
{
    [TestFixture]
    class CalcTests
    {
        [Test]
        public void Add_AddTwoNumber_ReturnNumber()
        {
            var calculator = Substitute.For<ICalculator>();
            calculator.Add(1, 2).Returns(3);
            Assert.That(calculator.Add(1, 2), Is.EqualTo(3));
        }

        [Test]
        public void Add_Call_Received()
        {
            var calculator = Substitute.For<ICalculator>();
            calculator.Add(1, 2);
            calculator.Received().Add(1, 2);
            calculator.DidNotReceive().Add(5, 7);
        }

        [Test]
        public void Mode_Call_ReturnValue()
        {
            var calculator = Substitute.For<ICalculator>();
            calculator.Mode.Returns("DEC");
            Assert.That(calculator.Mode, Is.EqualTo("DEC"));

            calculator.Mode = "HEX";
            Assert.That(calculator.Mode, Is.EqualTo("HEX"));
        }

        [Test]
        public void Add_CallWithValue_Received()
        {
            var calculator = Substitute.For<ICalculator>();

            calculator.Add(10, -5);

            calculator.Received().Add(10, Arg.Any<int>());
            calculator.Received().Add(10, Arg.Is<int>(x => x < 0));
        }

        [Test]
        public void Add_CallWithTypeValue_ReturnSum()
        {
            var calculator = Substitute.For<ICalculator>();

            calculator.Add(Arg.Any<int>(), Arg.Any<int>()).Returns(x => (int)x[0] + (int)x[1]);

            Assert.That(calculator.Add(10, 20), Is.EqualTo(30));
        }

        [Test]
        public void Add_CallWithValue_ReturnValue()
        {
            var calculator = Substitute.For<ICalculator>();

            calculator
                .Add(Arg.Any<int>(), Arg.Any<int>())
                .Returns(x => (int)x[0] + (int)x[1]);
            Assert.That(calculator.Add(5, 10), Is.EqualTo(15));
        }

        [Test]
        public void Mode_SetMultiValues_ReturnMultiValues()
        {
            var calculator = Substitute.For<ICalculator>();

            calculator.Mode.Returns("HEX", "DEC", "BIN");
            Assert.That(calculator.Mode, Is.EqualTo("HEX"));
            Assert.That(calculator.Mode, Is.EqualTo("DEC"));
            Assert.That(calculator.Mode, Is.EqualTo("BIN"));
        }

        [Test]
        public void PoweringUp_RaiseEvent_WasRaised()
        {
            var calculator = Substitute.For<ICalculator>();

            bool eventWasRaised = false;
            calculator.PoweringUp += (sender, args) => eventWasRaised = true;

            calculator.PoweringUp += Raise.Event();
            Assert.That(eventWasRaised);
        }


        [Test]
        public void FellInLove_RaiseEventWithTypedArg_WasRaised()
        {
            var calculator = Substitute.For<ICalculator>();

            bool eventWasRaised = false;
            calculator.FellInLove += (sender, args) => eventWasRaised = true;

            //calculator.FellInLove += Raise.EventWith<TestEventArgs>();
            calculator.FellInLove += Raise.EventWith<TestEventArgs>(calculator, new TestEventArgs());
            
            Assert.That(eventWasRaised);
        }
    }
}
