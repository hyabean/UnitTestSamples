using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalcTestLib;
using FakeItEasy;
using FakeItEasy.Sdk;
using NUnit.Framework;
using Shouldly;

namespace UnitTestSamples.FakeItEasyTests
{
    public class OtherTests
    {
        [Test]
        public void Syntax_Normal_haha()
        {
            var foo = A.Fake<IFoo>();
            var func = A.Fake<Func<string, int>>(); 
            
            var foos = A.CollectionOfFake<Foo>(10);


            var type = GetTypeOfFake();
            object fake = Create.Fake(type);
            IList<object> fakes = Create.CollectionOfFake(type, 10);

            // Specifying arguments for constructor using expression. This is refactoring friendly!
            // The constructor seen here is never actually invoked. It is an expression and it's purpose
            // is purely to communicate the constructor arguments which will be extracted from it
            var foo1 = A.Fake<FooClass>(x => x.WithArgumentsForConstructor(() => new FooClass("foo", "bar")));

            // Specifying arguments for constructor using IEnumerable<object>.
            var foo2 = A.Fake<FooClass>(x => x.WithArgumentsForConstructor(new object[] { "foo", "bar" }));

            // Specifying additional interfaces to be implemented. Among other uses,
            // this can help when a fake skips members because they have been
            // explicitly implemented on the class being faked.
            var foo3 = A.Fake<FooClass>(x => x.Implements(typeof(IFoo)));
            // or
            var foo4 = A.Fake<FooClass>(x => x.Implements<IFoo>());

            // Assigning custom attributes to the faked type.
            // foo's type should have "FooAttribute"
            var foo5 = A.Fake<IFoo>(x => x.WithAttributes(() => new FooAttribute()));

            // Create wrapper - unconfigured calls will be forwarded to wrapped
            var wrapped = new FooClass("foo", "bar");
            var foo6 = A.Fake<IFoo>(x => x.Wrapping(wrapped));
        }

        private Type GetTypeOfFake()
        {
            return typeof(Foo);
        }
    }
}
