// ****************************************************************
// Copyright © 2011, John Gietzen
// This is free software licensed under the zlib/libpng license.
// You may obtain a copy of the license at http://opensource.org.
// ****************************************************************

namespace NUnit.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using NUnit.Framework;
    using System.Collections;

    public static class Expectation
    {
        public static string[] DiscoverTestCases(string prefix)
        {
            var assembly = Assembly.GetCallingAssembly();

            return (from resourceName in assembly.GetManifestResourceNames()
                    where resourceName.StartsWith(prefix + ".", StringComparison.InvariantCultureIgnoreCase)
                    where resourceName.EndsWith(".expected", StringComparison.InvariantCultureIgnoreCase)
                    select Path.GetFileNameWithoutExtension(resourceName)).ToArray();
        }

        public static string GetTestData(string testCase)
        {
            var assembly = Assembly.GetCallingAssembly();

            var data = testCase + ".data";

            using (var stream = assembly.GetManifestResourceStream(data))
            {
                if (stream == null)
                {
                    return null;
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static string GetExpectation(string testCase, Assembly assembly)
        {
            var expected = testCase + ".expected";

            using (var stream = assembly.GetManifestResourceStream(expected))
            {
                if (stream == null)
                {
                    return null;
                }

                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        public static void Check(object result, string testCase)
        {
            var actual = GetActual(result);
            var expected = GetExpectation(testCase, Assembly.GetCallingAssembly());

            Assert.That(actual, Is.EqualTo(expected));
        }

        private static string GetActual(object result)
        {
            if (result == null)
            {
                return null;
            }
            else if (result is string)
            {
                return (string)result;
            }
            else if (result is IEnumerable)
            {
                var resultsEnum = (IEnumerable)result;
                return string.Join(Environment.NewLine, from object i in resultsEnum select GetActual(i));
            }
            else
            {
                return result.ToString();
            }
        }
    }
}
