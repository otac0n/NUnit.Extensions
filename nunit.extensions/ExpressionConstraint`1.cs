// ****************************************************************
// Copyright © 2011, John Gietzen
// This is free software licensed under the zlib/libpng license.
// You may obtain a copy of the license at http://opensource.org.
// ****************************************************************

namespace NUnit.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq.Expressions;
    using NUnit.Framework.Constraints;

    public class ExpressionConstraint<T> : Constraint
    {
        private Predicate<T> constraint;
        private string constraintText;

        public ExpressionConstraint()
            : this(null)
        {
        }

        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures",
            Justification = "This is the correct and required nesting for Expression<T> expressions.")]
        public ExpressionConstraint(Expression<Predicate<T>> constraint)
        {
            if (constraint != null)
            {
                this.constraint = constraint.Compile();
                this.constraintText = constraint.ToString();
            }
        }

        public override bool Matches(object actual)
        {
            this.actual = actual;

            if (actual == null)
            {
                return false;
            }

            if (!(actual is T))
            {
                return false;
            }

            if (this.constraint == null)
            {
                return true;
            }

            return this.constraint((T)actual);
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            if (this.constraint != null)
            {
                writer.Write(typeof(T).Name + " matching: " + this.constraintText);
            }
            else
            {
                writer.Write(typeof(T).Name);
            }
        }

        public override void WriteActualValueTo(MessageWriter writer)
        {
            if (writer == null)
            {
                return;
            }

            if (this.actual == null)
            {
                writer.Write("(null)");
            }
            else if (!typeof(T).IsAssignableFrom(this.actual.GetType()))
            {
                writer.Write(this.actual.GetType().Name);
            }
            else
            {
                writer.Write(this.actual.GetType().Name + " not matching constraint.");
            }
        }
    }
}
