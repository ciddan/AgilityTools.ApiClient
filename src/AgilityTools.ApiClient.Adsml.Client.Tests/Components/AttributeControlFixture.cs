using System;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Components.Interfaces;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Components
{
    [TestFixture]
    public class AttributeControlFixture
    {
        [Test]
        public void Instantiation() {
            //Act
            var acon = new AttributeControl(new IAttributeControl[] {AttributeToReturn.WithAttributeName("foo")});

            //Assert
            Assert.That(acon, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: attributesToReturn")]
        public void Throws_ArgumentNullException_If_No_AttributesToReturn_Are_Specified() {
            //Act
            new AttributeControl(null);
        }
    }
}