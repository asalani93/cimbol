﻿using System;
using System.Globalization;
using Cimpress.Cimbol.Runtime.Functions;

namespace Cimpress.Cimbol.Runtime.Types
{
    /// <summary>
    /// An implementation of <see cref="ILocalValue"/> used to stored <see cref="decimal"/> values.
    /// </summary>
    public class NumberValue : ILocalValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NumberValue"/> class.
        /// </summary>
        /// <param name="value">The value stored in the <see cref="NumberValue"/>.</param>
        public NumberValue(decimal value)
        {
            Value = value;
        }

        /// <summary>
        /// The value stored in the <see cref="NumberValue"/>.
        /// </summary>
        public decimal Value { get; }

        /// <inheritdoc cref="ILocalValue.Access"/>
        public ILocalValue Access(string key)
        {
#pragma warning disable CA1303
            throw new NotSupportedException("ErrorCode067");
#pragma warning restore CA1303
        }

        /// <inheritdoc cref="ILocalValue.CastBoolean"/>
        public BooleanValue CastBoolean()
        {
#pragma warning disable CA1303
            throw new NotSupportedException("ErrorCode068");
#pragma warning restore CA1303
        }

        /// <inheritdoc cref="ILocalValue.CastNumber"/>
        public NumberValue CastNumber()
        {
            return this;
        }

        /// <inheritdoc cref="ILocalValue.CastString"/>
        public StringValue CastString()
        {
            return new StringValue(Value.ToString(CultureInfo.InvariantCulture));
        }

        /// <inheritdoc cref="ILocalValue.EqualTo"/>
        public bool EqualTo(ILocalValue other)
        {
            switch (other)
            {
                case NumberValue otherNumber:
                    return RuntimeFunctions.InnerEqualTo(this, otherNumber);

                case StringValue otherString:
                    return RuntimeFunctions.InnerEqualTo(this, otherString);

                default:
                    return false;
            }
        }

        /// <inheritdoc cref="ILocalValue.Invoke"/>
        public ILocalValue Invoke(params ILocalValue[] arguments)
        {
#pragma warning disable CA1303
            throw new NotSupportedException("ErrorCode069");
#pragma warning restore CA1303
        }
    }
}