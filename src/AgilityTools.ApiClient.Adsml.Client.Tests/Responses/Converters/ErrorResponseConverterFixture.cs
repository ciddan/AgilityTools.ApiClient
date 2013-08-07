using System;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Responses;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Responses.Converters
{
  [TestFixture]
  public class ErrorResponseConverterFixture
  {
    [Test]
    public void CanInstantiateNewErrorResponseConverter() {
      //Act
      var erc = new ErrorResponseConverter();

      //Assert
      Assert.That(erc, Is.Not.Null);
    }

    [Test]
    public void CanConvertErrorResponseXml() {
      //Arrange
      XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
      var xml = new XElement("BatchResponse",
          new XAttribute("version", "5.1.16 build 116 (2010/05/27 14-36)"),
          new XAttribute(XNamespace.Xmlns + "xsi", xsi),
          new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
          new XElement("ErrorResponse",
              new XAttribute("id", "1"),
              new XAttribute("type", "malformedRequest"),
              new XAttribute("description", "foo"),
              new XElement("Message", "Foo error")));

      var erc = new ErrorResponseConverter();

      //Act
      ErrorResponse errorResponse = erc.Convert(xml).Single();

      //Assert
      Assert.That(errorResponse, Is.Not.Null);
      Assert.That(errorResponse.Description, Is.EqualTo("foo"));
      Assert.That(errorResponse.ErrorId, Is.EqualTo("1"));
      Assert.That(errorResponse.ErrorType, Is.EqualTo(ErrorResponse.ErrorTypes.MalformedRequest));
      Assert.That(errorResponse.Message, Is.EqualTo("Foo error"));
    }

    [Test]
    public void ConvertThrowsArgumentNullExceptionIfSourceIsNull() {
      //Arrange
      var erc = new ErrorResponseConverter();

      //Act
      Assert.Throws<ArgumentNullException>(() => erc.Convert(null));
    }

    [Test]
    public void ConvertThrowsInvalidOperationExceptionIfResponseIsNotErrorResponse() {
      //Arrange
      XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
      var response = new XElement("BatchResponse",
          new XAttribute("version", "5.1.16 build 116 (2010/05/27 14-36)"),
          new XAttribute(XNamespace.Xmlns + "xsi", xsi),
          new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
          new XElement("AuthResponse",
              new XAttribute("code", "0"),
              new XAttribute("description", "Success"),
              new XElement("Message", "foo")));

      var erc = new ErrorResponseConverter();

      //Act
      Assert.Throws<InvalidOperationException>(() => erc.Convert(response));
    }

    [Test]
    public void ConvertThrowsInvalidOperationExceptionIfResponseIsNotValid() {
      //Arrange
      XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
      var response = new XElement("BatchResponse",
          new XAttribute("version", "5.1.16 build 116 (2010/05/27 14-36)"),
          new XAttribute(XNamespace.Xmlns + "xsi", xsi),
          new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
          new XElement("FooResponse",
              new XAttribute("code", "0"),
              new XAttribute("description", "Success"),
              new XElement("Message", "foo")));

      var erc = new ErrorResponseConverter();

      //Act
      Assert.Throws<InvalidOperationException>(() => erc.Convert(response));
    }
  }
}