using System;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
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
            Assert.That(attribute.AttributeExtensions.Count(), Is.EqualTo(1));
            Assert.That(attribute.AttributeExtensions.ElementAt(0).ToString(),
                        Is.EqualTo(new XAttribute("nameParserClass", "foo").ToString()));
            Assert.That(attribute.ElementName, Is.EqualTo("ContextAttribute"));
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            var expected = new XElement("ContextAttribute",
                                        new XAttribute("name", "defaultDesign"),
                                        new XElement("Value", new XCData("foo"))).ToString();

            var attribute = new ContextAttribute { Name = "defaultDesign", Value = "foo" };

            //Act
            var actual = attribute.ToAdsml().ToString();
            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}