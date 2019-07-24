﻿using System.Collections.Generic;

namespace Cimpress.Cimbol.Compiler.SyntaxTree
{
    /// <summary>
    /// A syntax tree node representing a unary operation.
    /// Unary operations include operations like not and negate.
    /// </summary>
    public sealed class UnaryOpNode : IExpressionNode
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnaryOpNode"/> class.
        /// </summary>
        /// <param name="opType">The type of unary operation.</param>
        /// <param name="operand">The operand.</param>
        public UnaryOpNode(UnaryOpType opType, IExpressionNode operand)
        {
            OpType = opType;

            Operand = operand;
        }

        /// <summary>
        /// The type of unary operation.
        /// </summary>
        public UnaryOpType OpType { get; }

        /// <summary>
        /// The operand.
        /// </summary>
        public IExpressionNode Operand { get; }

        /// <inheritdoc cref="ISyntaxNode.Children"/>
        public IEnumerable<ISyntaxNode> Children()
        {
            yield return Operand;
        }

        /// <inheritdoc cref="ISyntaxNode.ChildrenReverse"/>
        public IEnumerable<ISyntaxNode> ChildrenReverse()
        {
            yield return Operand;
        }

        /// <inheritdoc cref="object.ToString"/>
        public override string ToString()
        {
            return $"{{{nameof(UnaryOpNode)} {OpType.GetOperator()}}}";
        }
    }
}