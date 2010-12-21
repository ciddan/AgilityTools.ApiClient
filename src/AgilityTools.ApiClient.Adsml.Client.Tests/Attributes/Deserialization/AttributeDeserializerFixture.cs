using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes.Deserialization;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Attributes.Deserialization
{
    [TestFixture]
    public class AttributeDeserializerFixture
    {
        [Test]
        public void Resolves_Correct_Deserializer_By_Element_Name() {
            //Arrange
            var element = new XElement("CompositeAttribute", new XAttribute("name", "foo"));

            //Act
            var attr = AttributeDeserializer.Deserialize(element);

            //Assert
            Assert.That(attr, Is.Not.Null);
            Assert.That(attr, Is.InstanceOf<CompositeAttribute>());
        }
    }
}