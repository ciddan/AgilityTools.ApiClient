using System;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Responses;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Responses.Converters
{
  [TestFixture]
  public class DeleteResponseConverterFixture
  {
    [Test]
    public void Can_Instantiate_New_DeleteResponseConverter() {
      //Act
      var drc = new DeleteResultResponseConverter("adsml.xsd");

      //Assert
      Assert.That(drc, Is.Not.Null);
      Assert.That(drc, Is.InstanceOf<IResponseConverter<XElement, DeleteResponse>>());
    }

    [Test]
    public void Can_Convert_XElement_To_DeleteResponse() {
      //Arrange
      XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
      var response = new XElement("BatchResponse",
          new XAttribute("version", "5.1.16 build 116 (2010/05/27 14-36)"),
          new XAttribute(XNamespace.Xmlns + "xsi", xsi),
          new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
          new XElement("DeleteResponse",
              new XAttribute("code", "0"),
              new XAttribute("description", "Success"),
              new XElement("Message", "foo")));

      var drc = new DeleteResultResponseConverter("adsml.xsd");

      //Act
      var converted = drc.Convert(response).Single();

      //Assert
      Assert.That(converted.Code, Is.EqualTo("0"));
      Assert.That(converted.Description, Is.EqualTo("Success"));
      Assert.That(converted.Messages.Count(), Is.EqualTo(1));
      Assert.That(converted.Messages.ElementAt(0), Is.EqualTo("foo"));
    }

    [Test]
    public void Throws_ArgumentNullException_If_Source_Is_Null() {
      //Arrange
      var drc = new DeleteResultResponseConverter("adsml.xsd");

      //Act
      Assert.Throws<ArgumentNullException>(() => drc.Convert(null));
    }

    [Test]
    public void Throws_InvalidOperationException_If_Response_Is_Not_DeleteResponse() {
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

      var drc = new DeleteResultResponseConverter("adsml.xsd");

      //Act
      Assert.Throws<InvalidOperationException>(() => drc.Convert(response));
    }

    [Test]
    public void Throws_InvalidOperationException_If_Response_Is_Not_Valid() {
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

      var drc = new DeleteResultResponseConverter("adsml.xsd");

      //Act
      Assert.Throws<InvalidOperationException>(() => drc.Convert(response));
    }
  }
}