using System;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Responses;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Responses.Converters
{
  [TestFixture]
  public class ContextResponseConverterFixture
  {
    [Test]
    public void Can_Instantiate_New_ContextResponseConverter() {
      //Act
      var contextResponseConverter = new ContextResponseConverter("adsml.xsd");

      //Assert
      Assert.That(contextResponseConverter, Is.Not.Null);
      Assert.That(contextResponseConverter, Is.InstanceOf<IResponseConverter<XElement, ContextResponse>>());
    }
    
    [Test]
    public void Can_Convert_XElement_To_ContextResponse() {
      //Arrange
      const string xml =
        "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n" +
        "<BatchResponse version=\"5.2.05 build 22 (2013/03/07 15-37)\"\n" +
        "  xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:noNamespaceSchemaLocation=\"adsml.xsd\">\n" +
        "  <SearchResponse executionTime=\"16ms\" resultCount=\"1\">\n" +
        "    <SearchResults base=\"/Structures/Classification/JULA Produkter\">\n" +
        "      <StructureContext idPath=\"180325:-180325:204419:204423:204437:204520:204788:204842:42038\" name=\"/Structures/Publication/V12/Foo/242110 KAP@fs:GERINGSSÅG 250 MM 1600W\" sortPath=\"0:2147483647:57:4:3:1:1:4:1\">\n" +
        "        <SimpleAttribute name=\"name\" type=\"text\">\n" +
        "          <Value>\n" +
        "            <![CDATA[242110 KAP/GERINGSSÅG 250 MM 1600W]]>\n" +
        "          </Value>\n" +
        "        </SimpleAttribute>\n" +
        "        <SimpleAttribute name=\"id\" type=\"integer\">\n" +
        "          <Value>\n" +
        "            <![CDATA[42038]]>\n" +
        "          </Value>\n" +
        "        </SimpleAttribute>\n" +
        "      </StructureContext>\n" +
        "    </SearchResults>\n" +
        "  </SearchResponse>\n" +
        "</BatchResponse>";

      XElement response = XElement.Parse(xml);
      ContextResponseConverter crc = new ContextResponseConverter("adsml.xsd");

      //Act
      var converted = crc.Convert(response).Single();

      //Assert
      Assert.That(converted.IdPath, Is.EqualTo("180325:-180325:204419:204423:204437:204520:204788:204842:42038"));
      Assert.That(converted.Name, Is.EqualTo("/Structures/Publication/V12/Foo/242110 KAP@fs:GERINGSSÅG 250 MM 1600W"));
      Assert.That(converted.Attributes.Count, Is.EqualTo(2));
      Assert.That(converted.Attributes.ElementAt(0).GetName(), Is.EqualTo("name"));
    }

    [Test]
    public void Throws_ArgumentNullException_If_Source_Is_Null() {
      //Arrange
      var crc = new ContextResponseConverter("adsml.xsd");

      //Act
      Assert.Throws<ArgumentNullException>(() => crc.Convert(null));
    }

    [Test]
    public void Throws_InvalidOperationException_If_Response_Is_Not_ContextResponse() {
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

      var crc = new ContextResponseConverter("adsml.xsd");

      //Act
      Assert.Throws<InvalidOperationException>(() => crc.Convert(response));
    }
  }
}