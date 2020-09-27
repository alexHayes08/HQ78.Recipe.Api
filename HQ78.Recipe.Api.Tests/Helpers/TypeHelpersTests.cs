using HQ78.Recipe.Api.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Schema.NET;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace HQ78.Recipe.Api.Tests.Helpers
{
    [TestClass]
    public class TypeHelpersTests
    {
        [TestMethod]
        public void IsTypeImplicitlyConvertableTest()
        {
            var baseType = typeof(Values<string, int>);
            var possibleConversion = typeof(string);

            var actualResult = TypeHelpers.IsTypeImplicitlyConvertable(
                baseType,
                possibleConversion
            );

            Assert.IsTrue(actualResult);
        }

        [TestMethod]
        public void TryCreateTypeWithCtorTest()
        {
            var result = Activator.CreateInstance(
                typeof(Values<string, int>),
                new object[] { "testing" }
            );

            var unboxedResult = result is object
                ? (Values<uint, int>?)result
                : new Values<uint, int>();

            Assert.IsTrue(unboxedResult.HasValue);
        }

        [TestMethod]
        public void TimespanSerializerTest()
        {
            var timespanStr = "P0DT0H15M";

            TimeSpan.TryParse(timespanStr, out var result);
            var otherResult = XmlConvert.ToTimeSpan(timespanStr);

            Assert.AreNotEqual(TimeSpan.Zero, result);
        }

        public class Blarg
        {
            public int Test { get; set; }

            public static explicit operator int(Blarg obj)
            {
                return obj.Test;
            }
        }
    }
}
