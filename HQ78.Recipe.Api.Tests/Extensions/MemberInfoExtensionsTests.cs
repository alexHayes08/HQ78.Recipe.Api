using HQ78.Recipe.Api.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace HQ78.Recipe.Api.Tests.Extensions
{
    [TestClass]
    public class MemberInfoExtensionsTests
    {
        #region Tests

        [TestMethod]
        [DynamicData(nameof(IsMemberNullableDataProvider), DynamicDataSourceType.Method)]
        public void IsMemberNullable(MemberInfo memberInfo, bool expectedResult)
        {
            var actualResult = memberInfo.IsMemberNullable();

            Assert.AreEqual(expectedResult, actualResult);
        }

        #endregion

        #region Helper Methods

        public static IEnumerable<object[]> IsMemberNullableDataProvider()
        {
            yield return new object[]
            {
                typeof(TestClass).GetProperty(nameof(TestClass.NullableStringProp)) ?? throw new Exception(),
                true
            };

            yield return new object[]
            {
                typeof(TestClass).GetProperty(nameof(TestClass.NonNullableStringProp)) ?? throw new Exception(),
                false
            };

            yield return new object[]
            {
                typeof(TestClass).GetProperty(nameof(TestClass.NullableIntProp)) ?? throw new Exception(),
                true
            };

            yield return new object[]
            {
                typeof(TestClass).GetProperty(nameof(TestClass.NonNullableIntProp)) ?? throw new Exception(),
                false
            };

            yield return new object[]
            {
                typeof(TestClass).GetProperty(nameof(TestClass.NullableRefProp)) ?? throw new Exception(),
                true
            };

            yield return new object[]
            {
                typeof(TestClass).GetProperty(nameof(TestClass.NonNullableRefProp)) ?? throw new Exception(),
                false
            };
        }

        #endregion

        #region Nested Classes

        public class TestClass
        {
            public TestClass()
            {
                NonNullableStringProp = string.Empty;
                NonNullableRefProp = new TestClass2();
            }

            public string? NullableStringProp { get; set; }

            public string NonNullableStringProp { get; set; }

            public int? NullableIntProp { get; set; }

            public int NonNullableIntProp { get; set; }

            public TestClass2? NullableRefProp { get; set; }

            public TestClass2 NonNullableRefProp { get; set; }
        }

        public class TestClass2
        {
            public int? NullableIntProp { get; set; }
        }

        #endregion
    }
}
