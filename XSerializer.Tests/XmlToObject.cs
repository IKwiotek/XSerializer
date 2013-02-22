﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using NUnit.Framework;

namespace XSerializer.Tests
{
    public abstract class XmlToObject
    {
        [TestCaseSource("TestCaseData")]
        public void SerializesCorrectly(string xml, Type type, object expectedObject)
        {
            var customSerializer = CustomSerializer.GetSerializer(type, null, null, null);

            var customObject = customSerializer.DeserializeObject(xml);

            Assert.That(customObject, Has.PropertiesEqualTo(expectedObject));
        }

        protected IEnumerable<TestCaseData> TestCaseData
        {
            get
            {
                return GetTestCaseData().Select(testCaseData =>
                {
                    if (string.IsNullOrWhiteSpace(testCaseData.TestName))
                    {
                        var instanceType = testCaseData.Arguments[0].GetType();
                        var type = (Type)testCaseData.Arguments[1];

                        return testCaseData.SetName(type == instanceType ? type.Name : string.Format("{0} as {1}", instanceType.Name, type.Name));
                    }

                    return testCaseData;
                });
            }
        }

        protected abstract IEnumerable<TestCaseData> GetTestCaseData();
    }
}