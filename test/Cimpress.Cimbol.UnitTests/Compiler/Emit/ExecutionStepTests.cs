﻿using Cimpress.Cimbol.Compiler.Emit;
using Cimpress.Cimbol.Compiler.SyntaxTree;
using NUnit.Framework;

namespace Cimpress.Cimbol.UnitTests.Compiler.Emit
{
    [TestFixture]
    public class ExecutionStepTests
    {
        [Test]
        public void Should_Initialize_When_GivenValidParameters()
        {
            var formulaDeclarationNode = new FormulaDeclarationNode("x", new IdentifierNode("y"), false);

            var result = new ExecutionStep(formulaDeclarationNode, ExecutionStepType.Synchronous);

            Assert.That(result.Node, Is.SameAs(formulaDeclarationNode));
            Assert.That(result.Type, Is.EqualTo(ExecutionStepType.Synchronous));
        }

        [Test]
        public void Should_BeMarkedAsAsynchronous_When_GivenAsynchronousType()
        {
            var formulaDeclarationNode = new FormulaDeclarationNode("x", new IdentifierNode("y"), false);
            var executionStep = new ExecutionStep(formulaDeclarationNode, ExecutionStepType.Asynchronous);

            var result = executionStep.IsAsynchronous;

            Assert.That(result, Is.True);
        }

        [Test]
        public void ShouldNot_BeMarkedAsAsynchronous_When_GivenSynchronousType()
        {
            var formulaDeclarationNode = new FormulaDeclarationNode("x", new IdentifierNode("y"), false);
            var executionStep = new ExecutionStep(formulaDeclarationNode, ExecutionStepType.Synchronous);

            var result = executionStep.IsAsynchronous;

            Assert.That(result, Is.False);
        }
    }
}