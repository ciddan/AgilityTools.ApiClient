using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Attributes
{
    [TestFixture]
    public class SimpleAttributeFixture
    {
        [Test]
        public void Can_Instantiate_New_SimpleAttribute() {
            //Act
            var attribute = new SimpleAttribute(AttributeTypes.Text);

            //Assert
            Assert.That(attribute, Is.Not.Null);
        }

        [Test]
        public void Can_Instantiate_New_SimpleAttribute_With_FactoryMethod() {
            //Act
            var attribute = SimpleAttribute.New(AttributeTypes.Integer, "objectId", "1777");

            //Assert
            Assert.That(attribute, Is.Not.Null);
        }

        [Test]
        public void Instantiation_Sets_Correct_Values_On_BaseType_Props() {
            //Act
            var attribute = new SimpleAttribute(AttributeTypes.Date);

            //Assert
            Assert.That(attribute.AttributeType, Is.EqualTo(AttributeTypes.Date));
            Assert.That(attribute.ElementName, Is.EqualTo("SimpleAttribute"));
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            var expected = new XElement("SimpleAttribute",
                                        new XAttribute("name", "objectTypeId"),
                                        new XAttribute("type", "integer"),
                                        new XElement("Value", new XCData("12"))).ToString();

            var attribute = new SimpleAttribute(AttributeTypes.Integer)
                            {
                                Values = new List<string> {"12"},
                                Name = "objectTypeId"
                            };

            //Act
            var actual = attribute.ToAdsml().ToString();
            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Can_Generate_Api_Xml_Without_Value() {
            //Arrange
            var expected = new XElement("SimpleAttribute",
                                        new XAttribute("name", "objectTypeId"),
                                        new XAttribute("type", "integer")).ToString();

            var attribute = new SimpleAttribute(AttributeTypes.Integer) { Name = "objectTypeId" };

            //Act
            var actual = attribute.ToAdsml().ToString();
            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_Multiple_Values()
        {
            //Arrange
            var expected = new XElement("SimpleAttribute",
                                                    new XAttribute("name", "objectTypeId"),
                                                    new XAttribute("type", "integer"),
                                                    new XElement("Value", new XCData("12")),
                                                    new XElement("Value", new XCData("24"))).ToString();

            var attribute = SimpleAttribute.New(AttributeTypes.Integer, "objectTypeId", new[] {"12", "24"});

            //Act
            var actual = attribute.ToAdsml().ToString();
            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "Name must be set.")]
        public void Validate_Throws_ASVE_If_Name_Is_Not_Set() {
            //Arrange
            var attribute = new SimpleAttribute(AttributeTypes.Binary) {Values = new List<string> {"1777"}};

            //Act
            attribute.ToAdsml();
        }
    }
}