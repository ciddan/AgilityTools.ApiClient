using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes.Deserialization;
using NUnit.Framework;
using System;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Attributes.Deserialization
{
    [TestFixture]
    public class RelationAttributeDeserializerFixture
    {
        [Test]
        public void Instantiation() {
            //Act
            var rad = new RelationAttributeDeserializer();

            //Assert
            Assert.That(rad, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: element")] 
        public void Throws_ArgumentNullException_If_Element_Is_Null() {
            //Arrange
            var rad = new RelationAttributeDeserializer();

            //Act
            rad.Deserialize(null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Not a valid RelationAttribute.")]
        public void Throws_InvalidOperationException_If_Element_Name_Is_Not_RelationAttribute() {
            //Arrange
            var rad = new RelationAttributeDeserializer();

            //Act
            rad.Deserialize(new XElement("foo"));
        }

        [Test]
        public void Can_Deserialize_RelationAttribute_Xml() {
            //Arrange
            var xml = new XElement("RelationAttribute",
                        new XAttribute("id", "31"),
                        new XAttribute("name", "Tillverkarrelation"),
                        new XAttribute("nameParserClass", "foo"),
                        new XElement("Value", new XCData("foo")),
                        new XElement("Value", new XCData("bar")));

            var expected = RelationAttribute.New("Tillverkarrelation", new[] {"foo", "bar"}, 31, "foo");

            var rad = new RelationAttributeDeserializer();

            //Act
            var actual = (RelationAttribute) rad.Deserialize(xml);

            //Assert
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.DefinitionId, Is.EqualTo(expected.DefinitionId));
            Assert.That(actual.NameParserClass, Is.EqualTo(expected.NameParserClass));

            Assert.That(actual.Values.ElementAt(0), Is.EqualTo(expected.Values.ElementAt(0)));
            Assert.That(actual.Values.ElementAt(1), Is.EqualTo(expected.Values.ElementAt(1)));
        }

        [Test]
        public void Deserialize_Returns_Correct_Object_With_Less_Data_Input() {
            //Arrange
            var xml = new XElement("RelationAttribute", new XAttribute("name", "foo"));

            var des = new RelationAttributeDeserializer();

            //Act
            var actual = (RelationAttribute)des.Deserialize(xml);

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Name, Is.EqualTo("foo"));
        }
    }
}