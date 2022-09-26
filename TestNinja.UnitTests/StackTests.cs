using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class StackTests
    {
        private Fundamentals.Stack<string> _stack;
        [SetUp]
        public void Setup()
        {
            _stack = new Fundamentals.Stack<string>();
        }
        
        [Test]
        public void Push_ArgumentIsNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _stack.Push(null));
        }

        [Test]
        public void Push_ArgumentIsValid_AddsNewItemToStack()
        {
            _stack.Push("a");

            Assert.That(_stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Pop_StackIsEmpty_ThrowsInvalidOperationException()
        {
            Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackIsNotEmpty_PopsAnItemFromTheList()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");
            _stack.Pop();

            Assert.That(_stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Peek_StackIsEmpty_ThrowsInvalidOperationException()
        {
            Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackIsNotEmpty_ReturnsTheLastItemThatWasAddedToTheList()
        {
            _stack.Push("a");
            _stack.Push("b");
            _stack.Push("c");
            Assert.That(_stack.Peek(), Is.EqualTo("c"));
        }

        [Test]
        public void Count_StackEmpty_ReturnsZero()
        {
            Assert.That(_stack.Count, Is.EqualTo(0));
        }
    }
}
