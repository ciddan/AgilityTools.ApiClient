using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Components
{
    [TestFixture]
    public class AttributeToReturnFixture
    {
        [Test]
        public void Can_Instantiate_New_AttributeToReturn() {
            //Act
            var atr = new AttributeToReturn();

            //Assert
            Assert.That(atr, Is.Not.Null);
        }

        [Test]
        public void Can_Generate_Api_Xml_With_DefinitionId() {
            //Arrange
            var expected = new XElement("Attribute", new XAttribute("id", "10"));
            var atr = AttributeToReturn.WithDefinitionId(10);

            //Act
            var acutal = atr.ToAdsml();

            //Assert
            Assert.That(acutal.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_Name()
        {
            //Arrange
            var expected = new XElement("Attribute", new XAttribute("name", "foo"));
            var atr = AttributeToReturn.WithAttributeName("foo");

            //Act
            var acutal = atr.ToAdsml();

            //Assert
            Assert.That(acutal.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_DefinitionId_And_Name()
        {
            //Arrange
            var expected = new XElement("Attribute", new XAttribute("name", "foo"), new XAttribute("id", "10"));
            var atr = AttributeToReturn.WithNameAndId("foo", 10);

            //Act
            var acutal = atr.ToAdsml();

            //Assert
            Assert.That(acutal.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "Invalid settings. Either DefinitionId or Name must be set.")]
        public void ToAdsml_Throws_ASVE_If_DefinitionId_And_Name_Are_Unset() {
            //Arrange
            var atr = new AttributeToReturn();

            //Act
            atr.ToAdsml();
        }
    }
}