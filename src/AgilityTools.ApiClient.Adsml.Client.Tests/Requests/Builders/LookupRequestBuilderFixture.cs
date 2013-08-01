using System;
using NUnit.Framework;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using System.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Requests.Builders
{
  [TestFixture]
  public class LookupRequestBuilderFixture
  {
    [Test]
    public void CanInstantiateNewLookupRequestBuilder() {
      //Act
      var lrb = new LookupRequestBuilder();

      //Assert
      Assert.That(lrb, Is.Not.Null);
    }

    [Test]
    public void CanSetContextName() {
      //Arrange
      var lrb = new LookupRequestBuilder();

      //Act
      lrb.Context("foo");

      //Assert
      Assert.That(lrb.Name, Is.EqualTo("foo"));
    }

    [Test]
    [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "name cannot be null or empty")]
    public void ContextNameThrowsInvalidOperationExceptionIfNull() {
      //Arrange
      var lrb = new LookupRequestBuilder();

      //Act
      lrb.Context(null);
    }

    [Test]
    [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "name cannot be null or empty")]
    public void ContextNameThrowsInvalidOperationExceptionIfEmpty() {
      //Arrange
      var lrb = new LookupRequestBuilder();

      //Act
      lrb.Context(null);
    }

    [Test]
    public void CanConfigureLookupControl() {
      //Arrange
      var lrb = new LookupRequestBuilder();

      //Act
      lrb.ConfigureLookupControls()
        .ReturnAttributes(AttributeToReturn.WithName("foo"));

      //Assert
      Assert.That(lrb.LookupControlBuilder, Is.Not.Null);
    }

    [Test]
    public void CanAddRequestFilter() {
      //Arrange
      var lrb = new LookupRequestBuilder();

      //Act
      lrb.ReturnNoAttributes(true);

      //Assert
      Assert.That(lrb.Filters, Is.Not.Null);
      Assert.That(lrb.Filters.Any(), Is.True);
    }

    [Test]
    public void CanBuildLookupRequest() {
      //Arrange
      var lrb = new LookupRequestBuilder();

      lrb.Context("foo")
        .ReturnNoAttributes(true)
        .ConfigureLookupControls()
          .ReturnAttributes(AttributeToReturn.WithName("bar"));

      //Act
      var lr = lrb.Build();

      //Assert
      Assert.That(lr, Is.Not.Null);
      Assert.That(lr.Name, Is.EqualTo("foo"));
      Assert.That(lr.LookupControls, Is.Not.Null);
      Assert.That(lr.RequestFilters, Is.Not.Null);
      Assert.That(lr.RequestFilters.Any(), Is.True);
    }
  }
}