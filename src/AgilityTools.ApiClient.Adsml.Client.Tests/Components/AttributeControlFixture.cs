using System;
using AgilityTools.ApiClient.Adsml.Client.Components;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Components
{
    [TestFixture]
    public class AttributeControlFixture
    {
        [Test]
        public void Instantiation() {
            //Act
            var acon = new AttributeControl(AttributeToReturn.WithName("foo"));

            //Assert
            Assert.That(acon, Is.Not.Null);
        }

        [Test]
        public void CanSpecifyNodeName() {
            //Act
            var acon = new AttributeControl("Foo", new AttributeTypeToReturn {Type = AttributeDataType.StructureText});

            //Assert
            Assert.That(acon.ToAdsml().Name.ToString(), Is.EqualTo("Foo"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: attributesToReturn")]
        public void Throws_ArgumentNullException_If_No_AttributesToReturn_Are_Specified() {
            //Act
            new AttributeControl(attributesToReturn: null);
        }
    }
}