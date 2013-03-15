using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Attributes
{
    [TestFixture]
    public class RelationAttributeFixture
    {
        [Test]
        public void Can_Instantiate_New_ContextAttribute() {
            //Act
            var attribute = new RelationAttribute("foo");

            //Assert
            Assert.That(attribute, Is.Not.Null);
        }

        [Test]
        public void Can_Instantiate_New_RelationAttribute_With_FactoryMethod() {
            //Act
            var attribute = RelationAttribute.New("Tillverkarrelation", "foo", 31);

            //Assert
            Assert.That(attribute, Is.Not.Null);
        }

        [Test]
        public void Instantiation_Sets_Correct_Values_On_BaseType_Props() {
            //Act
            var attribute = new RelationAttribute("foo", 10, "foo");

            //Assert
            Assert.That(attribute.NameParserClass, Is.EqualTo("foo"));
            Assert.That(attribute.DefinitionId, Is.EqualTo(10));
            Assert.That(attribute.ElementName, Is.EqualTo("RelationAttribute"));
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            var expected = new XElement("RelationAttribute",
                                        new XAttribute("name", "Tillverkarrelation"),
                                        new XAttribute("nameParserClass", "foo"),
                                        new XAttribute("id", "31"),
                                        new XElement("Value", new XCData("foo"))).ToString();

            var attribute = RelationAttribute.New("Tillverkarrelation", "foo", 31, "foo");

            //Act
            var actual = attribute.ToAdsml().ToString();
            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_Multiple_Values() {
            //Arrange
            var expected = new XElement("RelationAttribute",
                                        new XAttribute("name", "Tillverkarrelation"),
                                        new XAttribute("id", "31"),
                                        new XElement("Value", new XCData("foo")),
                                        new XElement("Value", new XCData("bar"))).ToString();


            var attribute = RelationAttribute.New("Tillverkarrelation", new[] {"foo", "bar"}, 31);

            //Act
            var actual = attribute.ToAdsml().ToString();
            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}