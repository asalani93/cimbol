﻿namespace Cimpress.Cimbol.Compiler.Utilities
{
    /// <summary>
    /// A position within a <see cref="SourceText"/> instance.
    /// </summary>
    public struct Position : System.IEquatable<Position>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Position"/> class.
        /// </summary>
        /// <param name="row">The row component of the position.</param>
        /// <param name="column">The column component of the position.</param>
        public Position(int row, int column)
        {
            Column = column;

            Row = row;
        }

        /// <summary>
        /// The column component of the position.
        /// </summary>
        public int Column { get; }

        /// <summary>
        /// The row component of the position.
        /// </summary>
        public int Row { get; }

        public static bool operator ==(Position left, Position right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Check if this position is equal to the provided object.
        /// </summary>
        /// <param name="other">The position to compare with.</param>
        /// <returns>Whether or not the two objects are equal.</returns>
        public override bool Equals(object other)
        {
            if (other is Position otherPosition)
            {
                return Equals(otherPosition);
            }

            return false;
        }

        /// <summary>
        /// Check if this position is equal to the provided position.
        /// </summary>
        /// <param name="otherPosition">The position to compare with.</param>
        /// <returns>Whether or not the two positions are equal.</returns>
        public bool Equals(Position otherPosition)
        {
            return Column == otherPosition.Column && Row == otherPosition.Row;
        }

        /// <inheritdoc cref=""/>
        public override int GetHashCode()
        {
            unchecked
            {
                return (Column * 397) + Row;
            }
        }
    }
}