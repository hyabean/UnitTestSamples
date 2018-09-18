using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using CalcTestLib;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace UnitTestSamples.FakeItEasyTests
{
    public class FakeItEasyTests
    {
        [Test]
        public void BuyCandy_Normal_MustCalled()
        {
            // Creating a fake object is just dead easy!
            // No mocks, no stubs, everything's a fake!
            var lollipop = A.Fake<ICandy>();
            var shop = A.Fake<ICandyShop>();
            // Easily set up a call to return a value
            A.CallTo(() => shop.GetTopSellingCandy()).Returns(lollipop);
            // Use your fake as you would an instance of the faked type.
            var developer = new SweetTooth();
            developer.BuyTastiestCandy(shop);
            // Asserting uses the same syntax as configuring calls.
            // There's no need to learn another syntax.
            A.CallTo(() => shop.BuyCandy(lollipop)).MustHaveHappened();
        }

        [Test]
        public void Event_Called_ReturnTrue()
        {
            var robot = A.Fake<IRobot>();
            TestEventArgs args = new TestEventArgs();

            bool isCalled = false;

            // Somehow use the fake from the code being tested
            robot.FellInLove += (sender, arg) =>
            {
                if (sender == robot && args == arg)
                {
                    isCalled = true;
                }
            };

            // Raise the event!
            //robot.FellInLove += Raise.With(robot, args); // the "sender" will be robot

            //// Use the overload for empty event args. Sender will be robot, args will be EventArgs.Empty
            //robot.FellInLove += Raise.WithEmpty();

            // Specify sender and event args explicitly:
            robot.FellInLove += Raise.With(sender: robot, e: args);

            isCalled.ShouldBe(true);
        }

        [Test]
        public void Faking_async_methods()
        {

        }
    }

    public class SweetTooth
    {
        public void BuyTastiestCandy(ICandyShop shop)
        {
            var candy = shop.GetTopSellingCandy();

            if (candy != null)
            {
                shop.BuyCandy(candy);
            }
        }
    }

    public interface ICandyShop
    {
        ICandy GetTopSellingCandy();
        void BuyCandy(ICandy lollipop);
    }

    public interface ICandy
    {
    }
}
