using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Attributes
{
    [TestFixture]
    public class ContextAttributeFixture
    {
        [Test]
        public void Can_Instantiate_New_ContextAttribute() {
            //Act
            var attribute = new ContextAttribute();

            //Assert
            Assert.That(attribute, Is.Not.Null);
        }

        [Test]
        public void Can_Instantiate_New_ContextAttribute_With_FactoryMethod() {
            //Act
            var attribute = ContextAttribute.New("defaultDesign", "foo");

            //Assert
            Assert.That(attribute, Is.Not.Null);
        }

        [Test]
        public void Instantiation_Sets_Correct_Values_On_BaseType_Props() {
            //Act
            var attribute = new ContextAttribute("foo");

            //Assert
            Assert.That(attribute.NameParserClass, Is.EqualTo("foo"));
            Assert.That(attribute.ElementName, Is.EqualTo("ContextAttribute"));
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            var expected = new XElement("ContextAttribute",
                                        new XAttribute("name", "defaultDesign"),
                                        new XElement("Value", new XCData("foo"))).ToString();

            var attribute = new ContextAttribute { Name = "defaultDesign", Values = new List<string>{"foo"} };

            //Act
            var actual = attribute.ToAdsml().ToString();
            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_Multiple_Values() {
            //Arrange
            var expected = new XElement("ContextAttribute",
                                        new XAttribute("name", "defaultDesign"),
                                        new XElement("Value", new XCData("foo")),
                                        new XElement("Value", new XCData("bar"))).ToString();

            var attribute = ContextAttribute.New(name: "defaultDesign", nameParserClass: null, values: new[] {"foo", "bar"});

            //Act
            var actual = attribute.ToAdsml().ToString();
            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}