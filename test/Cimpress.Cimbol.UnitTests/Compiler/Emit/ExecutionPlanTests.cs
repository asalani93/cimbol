﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cimpress.Cimbol.Compiler.Emit;
using Cimpress.Cimbol.Compiler.SyntaxTree;
using Cimpress.Cimbol.Runtime.Types;
using NUnit.Framework;

namespace Cimpress.Cimbol.UnitTests.Compiler.Emit
{
    [TestFixture]
    public class ExecutionPlanTests
    {
        [Test]
        public void Should_ThrowError_When_InitializedWithNullListOfExecutionGroups()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var executionPlan = new ExecutionPlan((IEnumerable<ExecutionGroup>)null);
            });
        }

        [Test]
        public void Should_ThrowError_When_InitializedWithNullDependencyTable()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var executionPlan = new ExecutionPlan((DependencyTable)null);
            });
        }

        [Test]
        public void Should_MergeExecutionGroups_When_InitializedWithEmptyListOfExecutionGroups()
        {
            var executionGroups = Enumerable.Empty<ExecutionGroup>();
            var executionPlan = new ExecutionPlan(executionGroups);

            var result = executionPlan.ExecutionGroups.ToArray();

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void Should_MergeExecutionGroups_When_InitializedWithExecutionGroups()
        {
            var executionStep1 = new ExecutionStep(
                new FormulaDeclarationNode("a", new IdentifierNode("a"), false),
                ExecutionStepType.Synchronous);
            var executionStep2 = new ExecutionStep(
                new FormulaDeclarationNode("b", new IdentifierNode("b"), false),
                ExecutionStepType.Synchronous);
            var executionStep3 = new ExecutionStep(
                new FormulaDeclarationNode("c", new IdentifierNode("c"), false),
                ExecutionStepType.Synchronous);
            var executionStep4 = new ExecutionStep(
                new FormulaDeclarationNode("d", new IdentifierNode("d"), false),
                ExecutionStepType.Synchronous);
            var executionGroup1 = new ExecutionGroup(new[] { executionStep1, executionStep2 });
            var executionGroup2 = new ExecutionGroup(new[] { executionStep3, executionStep4 });
            var executionPlan = new ExecutionPlan(new[] { executionGroup1, executionGroup2 });

            var result = executionPlan.ExecutionGroups.ToArray();

            Assert.That(result, Has.Length.EqualTo(1));
            var resultGroup = result.First();
            var expected = new[] { executionStep1, executionStep2, executionStep3, executionStep4 };
            Assert.That(resultGroup.ExecutionSteps, Is.EqualTo(expected));
        }

        [Test]
        public void Should_BuildExecutionGroupsFromDependencyTable_When_InitializedWithDependencyTable()
        {
            var formulaNode1 = new FormulaDeclarationNode("a", new LiteralNode(BooleanValue.True), true);
            var formulaNode2 = new FormulaDeclarationNode("b", new IdentifierNode("a"), true);
            var formulaNode3 = new FormulaDeclarationNode("c", new IdentifierNode("b"), true);
            var formulaNode4 = new FormulaDeclarationNode("d", new IdentifierNode("c"), true);
            var moduleNode = new ModuleDeclarationNode(
                "x",
                Enumerable.Empty<ImportDeclarationNode>(),
                new[] { formulaNode1, formulaNode2, formulaNode3, formulaNode4 });
            var programNode = new ProgramNode(
                Enumerable.Empty<ArgumentDeclarationNode>(),
                Enumerable.Empty<ConstantDeclarationNode>(),
                new[] { moduleNode });
            var dependencyTable = DependencyTable.Build(programNode);
            var executionPlan = new ExecutionPlan(dependencyTable);

            var result = executionPlan.ExecutionGroups.ToArray();
            
            Assert.That(result, Has.Length.EqualTo(1));
            var resultGroup = result.First();
            var expected = new[] { formulaNode1, formulaNode2, formulaNode3, formulaNode4 };
            Assert.That(resultGroup.ExecutionSteps.Select(x => x.Node), Is.EqualTo(expected));
        }
    }
}