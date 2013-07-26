using System;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Responses;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Responses.Converters
{
  [TestFixture]
  public class UnlinkResponseConverterFixture
  {
    [Test]
    public void CanInstantiateNewUnlinkResponseConverter() {
      //Act
      var urc = new UnlinkResultResponseConverter();

      //Assert
      Assert.That(urc, Is.Not.Null);
      Assert.That(urc, Is.InstanceOf<IResponseConverter<XElement, UnlinkResponse>>());
    }

    [Test]
    public void CanConvertXElementToUnlinkResponse() {
      //Arrange
      XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
      var response =
        new XElement("BatchResponse",
            new XAttribute("version", "5.1.16 build 116 (2010/05/27 14-36)"),
            new XAttribute(XNamespace.Xmlns + "xsi", xsi),
            new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
            new XElement("UnlinkResponse",
                new XAttribute("code", "0"),
                new XAttribute("description", "Success"),
                new XElement("Message", "foo")));

      var urc = new UnlinkResultResponseConverter();

      //Act
      var converted = urc.Convert(response).Single();

      //Assert
      Assert.That(converted.Code, Is.EqualTo("0"));
      Assert.That(converted.Description, Is.EqualTo("Success"));
      Assert.That(converted.Messages.Count(), Is.EqualTo(1));
      Assert.That(converted.Messages.ElementAt(0), Is.EqualTo("foo"));
    }

    [Test]
    public void ThrowsArgumentNullExceptionIfSourceIsNull() {
      //Arrange
      var urc = new UnlinkResultResponseConverter();

      //Act
      Assert.Throws<ArgumentNullException>(() => urc.Convert(null));
    }

    [Test]
    public void ThrowsInvalidOperationExceptionIfResponseIsNotUnlinkResponse() {
      //Arrange
      XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
      var response =
          new XElement("BatchResponse",
              new XAttribute("version", "5.1.16 build 116 (2010/05/27 14-36)"),
              new XAttribute(XNamespace.Xmlns + "xsi", xsi),
              new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
              new XElement("AuthResponse",
                  new XAttribute("code", "0"),
                  new XAttribute("description", "Success"),
                  new XElement("Message", "foo")));

      var urc = new UnlinkResultResponseConverter();

      //Act
      Assert.Throws<InvalidOperationException>(() => urc.Convert(response));
    }

    [Test]
    public void ThrowsInvalidOperationExceptionIfResponseIsNotValid() {
      //Arrange
      XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
      var response =
          new XElement("BatchResponse",
              new XAttribute("version", "5.1.16 build 116 (2010/05/27 14-36)"),
              new XAttribute(XNamespace.Xmlns + "xsi", xsi),
              new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
              new XElement("FooResponse",
                  new XAttribute("code", "0"),
                  new XAttribute("description", "Success"),
                  new XElement("Message", "foo")));

      var urc = new UnlinkResultResponseConverter();

      //Act
      Assert.Throws<InvalidOperationException>(() => urc.Convert(response));
    }
  }
}