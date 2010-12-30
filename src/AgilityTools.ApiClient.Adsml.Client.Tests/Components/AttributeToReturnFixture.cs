using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Components
{
    [TestFixture]
    public class AttributeToReturnFixture
    {
        [Test]
        public void Instantiation() {
            //Act
            var atr = new AttributeToReturn();

            //Assert
            Assert.That(atr, Is.Not.Null);
        }

        [Test]
        public void Can_Use_Factory_Method_To_Create_AttributeToReturn_With_DefinitionId() {
            //Act
            var atr = AttributeToReturn.WithDefinitionId(10);

            //Assert
            Assert.That(atr.DefinitionId, Is.EqualTo(10));
        }

        [Test]
        public void Can_Use_Factory_Method_To_Create_AttributeToReturn_With_Name() {
            //Act
            var atr = AttributeToReturn.WithAttributeName("foo");

            //Assert
            Assert.That(atr.Name, Is.EqualTo("foo"));
        }

        [Test]
        public void Can_Use_Factory_Method_To_Create_AttributeToReturn_With_Name_And_DefinitionId() {
            //Act
            var atr = AttributeToReturn.WithNameAndId("foo", 10);

            //Assert
            Assert.That(atr.Name, Is.EqualTo("foo"));
            Assert.That(atr.DefinitionId, Is.EqualTo(10));
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            string expected = 
                new XElement("Attribute", 
                    new XAttribute("name", "foo"), 
                    new XAttribute("id", "10"))
                    .ToString();

            var atr = AttributeToReturn.WithNameAndId("foo", 10);

            //Act
            string actual = atr.ToAdsml().ToString();
            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "Invalid settings. Either DefinitionId or Name must be set.")]
        public void Validate_Throws_ApiSerializationValidationException_If_No_Name_Or_DefinitioId_Are_Set() {
            //Act
            var atr = new AttributeToReturn();
            atr.ToAdsml();
        }
    }
}