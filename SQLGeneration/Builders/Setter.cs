﻿using System;
using System.Collections.Generic;
using SQLGeneration.Parsing;

namespace SQLGeneration.Builders
{
    /// <summary>
    /// Adds a column being set to a value to the command.
    /// </summary>
    public class Setter
    {
        private readonly Column _column;
        private readonly IProjectionItem _value;

        /// <summary>
        /// Initializes a new instance of a Setter.
        /// </summary>
        /// <param name="column">The name of the column to set.</param>
        /// <param name="value">The value to set the column to.</param>
        public Setter(Column column, IProjectionItem value)
        {
            if (column == null)
            {
                throw new ArgumentNullException("column");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            _column = column;
            _value = value;
        }

        /// <summary>
        /// Gets the column being set.
        /// </summary>
        public Column Column
        {
            get { return _column; }
        }

        /// <summary>
        /// Gets the value that the column is being set to.
        /// </summary>
        public IProjectionItem Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets the expression for setting a column in an update statement.
        /// </summary>
        /// <param name="options">The configuration to use when building the command.</param>
        /// <returns>The setter expression.</returns>
        internal IEnumerable<string> GetSetterTokens(CommandOptions options)
        {
            TokenStream stream = new TokenStream();
            stream.AddRange(((IProjectionItem)_column).GetProjectionTokens(options));
            stream.Add("=");
            stream.AddRange(_value.GetProjectionTokens(options));
            return stream;
        }
    }
}