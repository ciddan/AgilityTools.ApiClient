using System;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class LookupRequestBuilderFixture
    {
        [Test]
        public void Can_Instantiate_New_LookupRequestBuilder() {
            //Act
            var lrb = new LookupRequestBuilder();

            //Assert
            Assert.That(lrb, Is.Not.Null);
        }

        [Test]
        public void Can_Set_ContextName() {
            //Arrange
            var lrb = new LookupRequestBuilder();

            //Act
            lrb.ContextName("foo");

            //Assert
            Assert.That(lrb.Name, Is.EqualTo("foo"));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "name cannot be null or empty")]
        public void ContextName_Throws_InvalidOperationException_If_Null() {
            //Arrange
            var lrb = new LookupRequestBuilder();

            //Act
            lrb.ContextName(null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "name cannot be null or empty")]
        public void ContextName_Throws_InvalidOperationException_If_Empty() {
            //Arrange
            var lrb = new LookupRequestBuilder();

            //Act
            lrb.ContextName(null);
        }

        [Test]
        public void Can_Configure_LookupControl() {
            //Arrange
            var lrb = new LookupRequestBuilder();

            //Act
            lrb.ConfigureLookupControls()
                .ReturnAttributes(AttributeToReturn.WithDefinitionId(10));

            //Assert
            Assert.That(lrb.LookupControlBuilder, Is.Not.Null);
        }

        [Test]
        public void Can_Build_LookupRequest() {
            //Arrange
            var lrb = new LookupRequestBuilder();
            
            lrb.ContextName("foo")
                .ConfigureLookupControls()
                .ReturnAttributes(AttributeToReturn.WithDefinitionId(10));

            //Act
            var lr = lrb.Build();

            //Assert
            Assert.That(lr, Is.Not.Null);
            Assert.That(lr.LookupControls, Is.Not.Null);
            Assert.That(lr.Name, Is.EqualTo("foo"));
        }
    }
}