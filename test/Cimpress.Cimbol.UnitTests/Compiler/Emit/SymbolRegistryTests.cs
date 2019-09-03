﻿using System.Linq;
using Cimpress.Cimbol.Compiler.Emit;
using Cimpress.Cimbol.Compiler.SyntaxTree;
using Cimpress.Cimbol.Runtime.Types;
using NUnit.Framework;

namespace Cimpress.Cimbol.UnitTests.Compiler.Emit
{
    [TestFixture]
    public class SymbolRegistryTests
    {
        [Test]
        public void Should_CreateSymbolRegistryWithArgument_When_GivenProgramNodeWithArgumentDeclaration()
        {
            var argumentDeclarationNode = new ArgumentNode("x");
            var programNode = new ProgramNode(
                new[] { argumentDeclarationNode },
                Enumerable.Empty<ConstantNode>(),
                Enumerable.Empty<ModuleNode>());

            var result = new SymbolRegistry(programNode);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Arguments, Has.Count.EqualTo(1));
            Assert.That(result.Arguments.TryResolve("x", out _), Is.True);
            Assert.That(result.Constants, Is.Empty);
            Assert.That(result.Modules, Is.Empty);
            Assert.That(result.SymbolTables, Is.Empty);
        }

        [Test]
        public void Should_CreateSymbolRegistryWithConstant_When_GivenProgramNodeWithConstantDeclaration()
        {
            var constantDeclarationNode = new ConstantNode("x", BooleanValue.True);
            var programNode = new ProgramNode(
                Enumerable.Empty<ArgumentNode>(),
                new[] { constantDeclarationNode },
                Enumerable.Empty<ModuleNode>());

            var result = new SymbolRegistry(programNode);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Arguments, Is.Empty);
            Assert.That(result.Constants, Has.Count.EqualTo(1));
            Assert.That(result.Constants.TryResolve("x", out _), Is.True);
            Assert.That(result.Modules, Is.Empty);
            Assert.That(result.SymbolTables, Is.Empty);
        }

        [Test]
        public void Should_CreateSymbolRegistryWithModule_When_GivenProgramNodeWithModuleDeclaration()
        {
            var moduleDeclarationNode = new ModuleNode(
                "x",
                Enumerable.Empty<ImportNode>(),
                Enumerable.Empty<FormulaNode>());
            var programNode = new ProgramNode(
                Enumerable.Empty<ArgumentNode>(),
                Enumerable.Empty<ConstantNode>(),
                new[] { moduleDeclarationNode });

            var result = new SymbolRegistry(programNode);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Arguments, Is.Empty);
            Assert.That(result.Constants, Is.Empty);
            Assert.That(result.Modules, Has.Count.EqualTo(1));
            Assert.That(result.Modules.TryResolve("x", out _), Is.True);
            Assert.That(result.SymbolTables, Has.Count.EqualTo(1));
            Assert.That(result.SymbolTables, Contains.Key(moduleDeclarationNode));
        }

        [Test]
        public void Should_CreateSymbolTableInRegistryWithSymbol_When_GivenProgramNodeWithModuleWithFormula()
        {
            var formulaDeclarationNode = new FormulaNode("x", new IdentifierNode("y"), true);
            var moduleDeclarationNode = new ModuleNode(
                "x",
                Enumerable.Empty<ImportNode>(),
                new[] { formulaDeclarationNode });
            var programNode = new ProgramNode(
                Enumerable.Empty<ArgumentNode>(),
                Enumerable.Empty<ConstantNode>(),
                new[] { moduleDeclarationNode });

            var result = new SymbolRegistry(programNode).SymbolTables[moduleDeclarationNode];

            Assert.That(result, Is.Not.Null);
            Assert.That(result.TryResolve("x", out var resultSymbol), Is.True);
            Assert.That(resultSymbol, Is.Not.Null);
            Assert.That(resultSymbol.Name, Is.EqualTo("x"));
        }

        [Test]
        public void Should_CreateSymbolTableInRegistryWithSymbol_When_GivenProgramNodeWithModuleWithImport()
        {
            var importDeclarationNode = new ImportNode("x", new[] { "y" }, ImportType.Constant);
            var moduleDeclarationNode = new ModuleNode(
                "x",
                new[] { importDeclarationNode },
                Enumerable.Empty<FormulaNode>());
            var programNode = new ProgramNode(
                Enumerable.Empty<ArgumentNode>(),
                Enumerable.Empty<ConstantNode>(),
                new[] { moduleDeclarationNode });

            var result = new SymbolRegistry(programNode).SymbolTables[moduleDeclarationNode];

            Assert.That(result, Is.Not.Null);
            Assert.That(result.TryResolve("x", out var resultSymbol), Is.True);
            Assert.That(resultSymbol, Is.Not.Null);
            Assert.That(resultSymbol.Name, Is.EqualTo("x"));
        }
    }
}