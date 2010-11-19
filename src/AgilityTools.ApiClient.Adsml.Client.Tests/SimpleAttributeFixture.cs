using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    public class SimpleAttributeFixture
    {
        [Test]
        public void Can_Instantiate_New_Simple_Attribute() {
            //Act
            var attribute = new SimpleAttribute(SimpleAttributeType.Text);

            //Assert
            Assert.That(attribute, Is.Not.Null);
        }

        [Test]
        public void Instantiation_Sets_Correct_Values_On_BaseType_Props() {
            //Act
            var attribute = new SimpleAttribute(SimpleAttributeType.Date);

            //Assert
            Assert.That(attribute.AttributeExtensions.Count(), Is.EqualTo(1));
            Assert.That(attribute.AttributeExtensions.ElementAt(0).ToString(), Is.EqualTo(new XAttribute("simpleAttributeType", "date").ToString()));
            Assert.That(attribute.ElementName, Is.EqualTo("SimpleAttribute"));
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            var expected = new XElement("SimpleAttribute",
                                new XAttribute("name", "objectId"),
                                new XAttribute("simpleAttributeType", "integer"),
                                "1777").ToString();

            var attribute = new SimpleAttribute(SimpleAttributeType.Integer)
                            {
                                Value = 1777, Name = "objectId"
                            };

            //Act
            var actual = attribute.ToAdsml().ToString();

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException(typeof (ApiSerializationValidationException), ExpectedMessage = "Name must be set.")]
        public void Validate_Throws_ASVE_If_Name_Is_Not_Set() {
            //Arrange
            var attribute = new SimpleAttribute(SimpleAttributeType.Integer)
                            {
                                Value = 1777
                            };

            //Act
            attribute.ToAdsml();
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "Value must be set.")]
        public void Validate_Throws_ASVE_If_Value_Is_Not_Set() {
            //Arrange
            var attribute = new SimpleAttribute(SimpleAttributeType.Integer)
                            {
                                Name = "objectId"
                            };

            //Act
            attribute.ToAdsml();
        }
    }
}