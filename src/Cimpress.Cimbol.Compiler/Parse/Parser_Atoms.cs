﻿using System;
using Cimpress.Cimbol.Compiler.Scan;
using Cimpress.Cimbol.Compiler.SyntaxTree;
using Cimpress.Cimbol.Compiler.Utilities;

namespace Cimpress.Cimbol.Compiler.Parse
{
    /// <summary>
    /// The set of methods to use with the <see cref="Parser"/> for parsing atoms.
    /// </summary>
    public partial class Parser
    {
        /// <summary>
        /// Parse a series of <see cref="Token"/> objects into a terminal syntax tree node.
        /// </summary>
        /// <returns>Either a <see cref="ConstantNode"/> or <see cref="IdentifierNode"/>.</returns>
        public INode Atom()
        {
            var current = Lookahead(0);
            switch (current)
            {
                // Production rule for a true keyword.
                // Atom -> "true"
                case TokenType.TrueKeyword:
                {
                    Match(TokenType.TrueKeyword);
                    return new ConstantNode(true);
                }

                // Production rule for a false keyword.
                // Atom -> "false"
                case TokenType.FalseKeyword:
                {
                    Match(TokenType.FalseKeyword);
                    return new ConstantNode(false);
                }

                // Production rule for a number literal.
                // Atom -> NumberLiteral
                case TokenType.NumberLiteral:
                {
                    var match = Match(TokenType.NumberLiteral);
                    return new ConstantNode(NumberSerializer.DeserializeNumber(match.Value));
                }

                // Production rule for a string literal.
                // Atom -> StringLiteral
                case TokenType.StringLiteral:
                {
                    var match = Match(TokenType.StringLiteral);
                    return new ConstantNode(StringSerializer.DeserializeString(match.Value));
                }

                // Production rule for an identifier.
                // Atom -> Identifier
                case TokenType.Identifier:
                {
                    var match = Match(TokenType.Identifier);
                    return new IdentifierNode(IdentifierSerializer.DeserializeIdentifier(match.Value));
                }

                // Production rule for a parenthesized expression.
                // Atom -> LeftParenthesis Expression RightParenthesis
                case TokenType.LeftParenthesis:
                {
                    Match(TokenType.LeftParenthesis);
                    var expression = Expression();
                    Match(TokenType.RightParenthesis);
                    return expression;
                }

                default:
                    // Expected a terminal, found something else.
                    throw new NotSupportedException();
            }
        }
    }
}