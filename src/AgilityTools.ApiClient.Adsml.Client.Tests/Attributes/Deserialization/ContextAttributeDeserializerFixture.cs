using System;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Attributes.Deserialization
{
    [TestFixture]
    public class ContextAttributeDeserializerFixture
    {
        [Test]
        public void Instantiation() {
            //Arrange
            var cad = new ContextAttributeDeserializer();

            //Act
            Assert.That(cad, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: element")]
        public void Throws_ArgumentNullException_If_Element_Is_Null() {
            //Arrange
            var rad = new ContextAttributeDeserializer();

            //Act
            rad.Deserialize(null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Not a valid ContextAttribute.")]
        public void Throws_InvalidOperationException_If_Element_Name_Is_Not_ContextAttribute() {
            //Arrange
            var rad = new ContextAttributeDeserializer();

            //Act
            rad.Deserialize(new XElement("foo"));
        }

        [Test]
        public void Can_Deserialize_ContextAttribute_Xml() {
            //Arrange
            var xml = new XElement("ContextAttribute",
                        new XAttribute("name", "foo"),
                        new XElement("Value", "Foo/Bar"),
                        new XElement("Value", "Bar/Foo"));


            var expected = ContextAttribute.New("foo", new[] {"Foo/Bar", "Bar/Foo"});

            var rad = new ContextAttributeDeserializer();

            //Act
            var actual = (ContextAttribute) rad.Deserialize(xml);

            //Assert
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            
            Assert.That(actual.Values.ElementAt(0), Is.EqualTo(expected.Values.ElementAt(0)));
            Assert.That(actual.Values.ElementAt(1), Is.EqualTo(expected.Values.ElementAt(1)));
        }

        [Test]
        public void Deserialize_Returns_Correct_Object_With_Less_Data_Input() {
            //Arrange
            var xml = new XElement("ContextAttribute", new XAttribute("name", "foo"));

            var des = new ContextAttributeDeserializer();

            //Act
            var actual = (ContextAttribute) des.Deserialize(xml);

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Name, Is.EqualTo("foo"));
        }
    }
}