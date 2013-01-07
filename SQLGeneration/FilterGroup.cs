﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SQLGeneration.Properties;

namespace SQLGeneration
{
    /// <summary>
    /// Represents a grouping of filters.
    /// </summary>
    public class FilterGroup : Filter, IFilterGroup
    {
        private readonly List<IFilter> _filters;

        /// <summary>
        /// Creates a new FilterGroup.
        /// </summary>
        public FilterGroup()
        {
            _filters = new List<IFilter>();
        }

        /// <summary>
        /// Gets the filters in the filter group.
        /// </summary>
        public IEnumerable<IFilter> Filters
        {
            get
            {
                return new ReadOnlyCollection<IFilter>(_filters);
            }
        }

        /// <summary>
        /// Adds the filter to the group.
        /// </summary>
        /// <param name="filter">The filter to add.</param>
        public void AddFilter(IFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }
            _filters.Add(filter);
        }

        /// <summary>
        /// Removes the filter from the group.
        /// </summary>
        /// <param name="filter">The filter to remove.</param>
        /// <returns>True if the filter was removed; otherwise, false.</returns>
        public bool RemoveFilter(IFilter filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException("filter");
            }
            return _filters.Remove(filter);
        }

        /// <summary>
        /// Gets whether there are any filters in the group.
        /// </summary>
        public bool HasFilters
        {
            get
            {
                return _filters.Count > 0;
            }
        }

        /// <summary>
        /// Gets the filter text without parentheses or a not.
        /// </summary>
        /// <returns>A string representing the filter.</returns>
        protected override string GetFilterText()
        {
            if (_filters.Count == 0)
            {
                throw new SQLGenerationException(Resources.EmptyFilterGroup);
            }
            StringBuilder result = new StringBuilder();
            IFilter first = _filters[0];
            result.Append(first.GetFilterText());
            ConjunctionConverter converter = new ConjunctionConverter();
            for (int index = 1; index < _filters.Count; ++index)
            {
                IFilter filter = _filters[index];
                result.Append(" ");
                string conjunction = converter.ToString(filter.Conjunction);
                result.Append(conjunction);
                result.Append(" ");
                result.Append(filter.GetFilterText());
            }
            return result.ToString();
        }
    }
}