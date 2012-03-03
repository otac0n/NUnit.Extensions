// ****************************************************************
// Copyright © 2011, John Gietzen
// This is free software licensed under the zlib/libpng license.
// You may obtain a copy of the license at http://opensource.org.
// ****************************************************************

namespace NUnit.Extensions.Samples.ExpressionConstraint
{
    using System.Text;
    using NUnit.Framework;

    [TestFixture]
    public class ExpressionConstraintTests
    {
        [Test]
        public void Demo_ExpressionConstraintFail()
        {
            Assert.That(1, Is<int>.Where(i => i > 1));
        }

        [Test]
        public void Demo_ExpressionConstraintPass()
        {
            var actual = new StringBuilder();

            Assert.That(actual, Is<StringBuilder>.Where(s => s.Capacity > 0));
        }
    }
}
