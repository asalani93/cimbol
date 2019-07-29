﻿using System.Linq;
using Cimpress.Cimbol.Compiler.SyntaxTree;
using NUnit.Framework;

namespace Cimpress.Cimbol.UnitTests.Compiler.SyntaxTree
{
    [TestFixture]
    public class ImportStatementNodeTests
    {
        [Test]
        public void Should_SerializeToString_When_Valid()
        {
            var node = new ImportStatementNode("a", new[] { "x", "y", "z" }, ImportType.Formula);
            Assert.That(node.ToString(), Is.EqualTo("{ImportStatementNode Formula a}"));
        }

        [Test]
        public void Should_ConvertPathToImmutableArray_When_GivenPath()
        {
            var node = new ImportStatementNode("a", new[] { "x", "y", "z" }, ImportType.Formula);
            Assert.That(node.ImportPath, Is.EqualTo(new[] { "x", "y", "z" }));
        }

        [Test]
        public void Should_ReturnEmptyEnumerable_When_IteratingChildren()
        {
            var node = new ImportStatementNode("a", new[] { "x", "y", "z" }, ImportType.Formula);
            Assert.That(node.Children(), Is.EqualTo(Enumerable.Empty<ISyntaxNode>()));
        }

        [Test]
        public void Should_ReturnEmptyEnumerable_When_IteratingChildrenReverse()
        {
            var node = new ImportStatementNode("a", new[] { "x", "y", "z" }, ImportType.Formula);
            Assert.That(node.ChildrenReverse(), Is.EqualTo(Enumerable.Empty<ISyntaxNode>()));
        }
    }
}