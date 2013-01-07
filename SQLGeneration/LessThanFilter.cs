﻿using System;

namespace SQLGeneration
{
    /// <summary>
    /// Represents a comparison where the left hand item is less than the right hand item.
    /// </summary>
    public class LessThanFilter : ComparisonFilter
    {
        /// <summary>
        /// Creates a new LessThanFilter.
        /// </summary>
        /// <param name="leftHand">The left hand item.</param>
        /// <param name="rightHand">The right hand item.</param>
        public LessThanFilter(IFilterItem leftHand, IFilterItem rightHand)
            : base(leftHand, rightHand)
        {
        }

        /// <summary>
        /// Gets the filter text without parentheses or a not.
        /// </summary>
        /// <returns>A string representing the filter.</returns>
        protected override string Combine(string leftHand, string rightHand)
        {
            return leftHand + " < " + rightHand;
        }
    }
}