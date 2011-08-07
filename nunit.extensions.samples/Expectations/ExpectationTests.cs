// ****************************************************************
// Copyright © 2011, John Gietzen
// This is free software licensed under the zlib/libpng license.
// You may obtain a copy of the license at http://opensource.org.
// ****************************************************************

namespace NUnit.Extensions.Samples.Expectations
{
    using System;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class ExpectationTests
    {
        private readonly string[] demoTestCases = Expectation.DiscoverTestCases(typeof(ExpectationTests).Namespace + ".TestCases");

        [Test]
        [TestCaseSource("demoTestCases")]
        public void Demo_TestCases(string testCase)
        {
            var testData = Expectation.GetTestData(testCase);

            var wordCounts = from line in testData.Split('\n')
                             let words = line.Split(" ".ToArray(), StringSplitOptions.RemoveEmptyEntries)
                             select words.Length;

            Expectation.Check(wordCounts, testCase);
        }
    }
}
