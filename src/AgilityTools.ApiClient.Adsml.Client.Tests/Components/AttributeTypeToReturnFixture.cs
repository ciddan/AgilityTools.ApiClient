using System;
using System.Xml.Linq;
using NUnit.Framework;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Components
{
    [TestFixture]
    public class AttributeTypeToReturnFixture
    {
        [Test]
        public void Instantiation() {
            //Act
            var attr = new AttributeTypeToReturn();

            //Assert
            Assert.That(attr, Is.Not.Null);
        }

        [Test]
        public void CanUseFactoryMethodToCreateNewAttributeTypeToReturn() {
            //Act
            var attr = AttributeTypeToReturn.OfType(AttributeDataType.StructureTable);

            //Assert
            Assert.That(attr.Type, Is.EqualTo(AttributeDataType.StructureTable));
        }

        [Test]
        public void Can_Generate_Api_Xml()
        {
            //Arrange
            string expected =
                new XElement("AttributeType",
                    new XAttribute("name", "structure-text"))
                    .ToString();

            var attr = AttributeTypeToReturn.OfType(AttributeDataType.StructureText);

            //Act
            string actual = attr.ToAdsml().ToString();
            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}