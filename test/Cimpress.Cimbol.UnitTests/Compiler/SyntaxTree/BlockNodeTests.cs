﻿using Cimpress.Cimbol.Compiler.SyntaxTree;
using NUnit.Framework;

namespace Cimpress.Cimbol.UnitTests.Compiler.SyntaxTree
{
    [TestFixture]
    public class BlockNodeTests
    {
        [Test]
        public void Should_SerializeToString_When_Valid()
        {
            var node = new BlockNode(new IExpressionNode[] { null });
            Assert.AreEqual("{BlockNode}", node.ToString());
        }

        [Test]
        public void Should_ReturnChildrenInOrder_When_IteratingChildren()
        {
            var child1 = new LiteralNode(1);
            var child2 = new LiteralNode(2);
            var child3 = new LiteralNode(3);
            var node = new BlockNode(new[] { child1, child2, child3 });

            var expected = new[] { child1, child2, child3 };
            CollectionAssert.AreEqual(expected, node.Children());
        }

        [Test]
        public void Should_ReturnChildrenInOrder_When_IteratingChildrenReverse()
        {
            var child1 = new LiteralNode(1);
            var child2 = new LiteralNode(2);
            var child3 = new LiteralNode(3);
            var node = new BlockNode(new[] { child1, child2, child3 });

            var expected = new[] { child3, child2, child1 };
            CollectionAssert.AreEqual(expected, node.ChildrenReverse());
        }
    }
}