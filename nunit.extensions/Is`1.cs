// ****************************************************************
// Copyright © 2011, John Gietzen
// This is free software licensed under the zlib/libpng license.
// You may obtain a copy of the license at http://opensource.org.
// ****************************************************************

namespace NUnit.Extensions
{
    using System;
    using System.Linq.Expressions;
    using NUnit.Framework.Constraints;

    public class Is<TValue>
    {
        private Is()
        {
        }

        public static Constraint Where(Expression<Predicate<TValue>> constraint)
        {
            return new ExpressionConstraint<TValue>(constraint);
        }
    }
}
