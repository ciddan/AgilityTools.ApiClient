using System;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes.Deserialization;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Attributes.Deserialization
{
    [TestFixture]
    public class SimpleAttributeDeserializerFixture
    {
        [Test]
        public void Instantiation() {
            //Act
            var sad = new SimpleAttributeDeserializer();

            //Assert
            Assert.That(sad, Is.Not.Null);
        }

        [Test]
        public void Can_Deserialize_SimpleAttribute() {
            //Arrange
            var xml = new XElement("SimpleAttribute",
                        new XAttribute("name", "name"),
                        new XAttribute("type", "text"),
                        new XElement("Value", "foo"));

            var expected = SimpleAttribute.New(AttributeTypes.Text, "name", "foo");

            var sad = new SimpleAttributeDeserializer();

            //Act
            var actual = (SimpleAttribute) sad.Deserialize(xml);

            //Assert
            Assert.That(actual, Is.InstanceOf<SimpleAttribute>());

            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.AttributeType, Is.EqualTo(expected.AttributeType));

            Assert.That(actual.Values.ElementAt(0), Is.EqualTo(expected.Values.ElementAt(0)));
        }

        [Test]
        public void Deserialize_Returns_Correct_Object_With_Less_Data_Input() {
            //Arrange
            var xml = new XElement("SimpleAttribute", new XAttribute("name", "foo"), new XAttribute("type", "text"));

            var des = new SimpleAttributeDeserializer();

            //Act
            var actual = (SimpleAttribute) des.Deserialize(xml);

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Name, Is.EqualTo("foo"));
            Assert.That(actual.AttributeType, Is.EqualTo(AttributeTypes.Text));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: element")] 
        public void Throws_ArgumentNullException_If_Element_Is_Null() {
            //Arrange
            var sad = new SimpleAttributeDeserializer();

            //Act
            sad.Deserialize(null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Not a valid SimpleAttribute.")]
        public void Throws_InvalidOperationException_If_Element_Name_Is_Not_RelationAttribute()
        {
            //Arrange
            var sad = new SimpleAttributeDeserializer();

            //Act
            sad.Deserialize(new XElement("foo"));
        }
    }
}