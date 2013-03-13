using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Requests
{
  [TestFixture]
  public class CreateRequestFixture
  {
    [Test]
    public void Can_Instatiate_New_CreateRequest() {
      //Act
      var cr = new CreateRequest("foo", "bar", "/1/2", new StructureAttribute());

      //Assert
      Assert.That(cr, Is.Not.Null);
      Assert.That(cr.ObjectTypeName, Is.EqualTo("foo"));
      Assert.That(cr.ContextName, Is.EqualTo("bar"));
      Assert.That(cr.ParentIdPath, Is.EqualTo("/1/2"));
      Assert.That(cr.AttributesToSet.Count, Is.AtLeast(1));
    }

    [Test]
    public void Parameter_Guards() {
      //Assert
      Assert.Throws<ArgumentNullException>(() => new CreateRequest("foo", "bar", null, null));
      Assert.Throws<InvalidOperationException>(() => new CreateRequest(null, "bar", null, new StructureAttribute()));
      Assert.Throws<InvalidOperationException>(() => new CreateRequest(string.Empty, "bar", null, new StructureAttribute()));
      Assert.Throws<InvalidOperationException>(() => new CreateRequest("foo", null, null, new StructureAttribute()));
      Assert.Throws<InvalidOperationException>(() => new CreateRequest("foo", string.Empty, null, new StructureAttribute()));
    }

    [Test]
    public void Can_Generate_Api_Xml() {
      //Arrange
      XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
      var expected = 
        new XElement("BatchRequest",
          new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
          new XAttribute(XNamespace.Xmlns + "xsi", xsi),
          new XElement("CreateRequest",
              new XAttribute("name", "fooName"),
              new XAttribute("type", "fooObjectTypeName"),
              new XAttribute("parentIdPath", "fooPath"),
              new XElement("AttributesToSet",
                  new XElement("StructureAttribute",
                      new XAttribute("id", "215"),
                      new XAttribute("name", "fooAttributeName"),
                      new XElement("StructureValue",
                          new XAttribute("langId", "10"),
                          new XAttribute("scope", "global"),
                          new XCData("fooValue"))))));

      var value = new StructureValue {LanguageId = 10, Value = "fooValue"};
      var attribute = new StructureAttribute {
        DefinitionId = 215,
        Name = "fooAttributeName",
        Values = new List<StructureValue> {value}
      };

      var request = new CreateRequest("fooObjectTypeName", "fooName", "fooPath", attribute);

      //Act
      var actual = request.ToAdsml();

      //Assert
      Assert.That(actual, Is.Not.Null);
      Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
      Assert.DoesNotThrow(() => actual.ValidateAdsmlResponse("adsml.xsd"));
    }

    [Test]
    public void Can_Generate_Api_Xml_With_Request_Filters() {
      //Arrange
      XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
      var expected = 
        new XElement("BatchRequest",
            new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
            new XAttribute(XNamespace.Xmlns + "xsi", xsi),
            new XElement("CreateRequest",
                new XAttribute("name", "fooPath"),
                new XAttribute("type", "fooObjectTypeName"),
                new XAttribute("returnNoAttributes", "true"),
                new XAttribute("failOnError", "true"),
                new XElement("AttributesToSet",
                    new XElement("StructureAttribute",
                        new XAttribute("id", "215"),
                        new XAttribute("name", "fooAttributeName"),
                        new XElement("StructureValue",
                            new XAttribute("langId", "10"),
                            new XAttribute("scope", "global"),
                            new XCData("fooValue"))))));

      var value = new StructureValue {LanguageId = 10, Value = "fooValue"};
      var attribute = new StructureAttribute {
        DefinitionId = 215,
        Name = "fooAttributeName",
        Values = new List<StructureValue> {value}
      };

      var request = new CreateRequest("fooObjectTypeName", "fooPath", null, attribute) {
        RequestFilters =
          new List<ICreateRequestFilter> {
            Filter.ReturnNoAttributes(),
            Filter.FailOnError()
          }
      };

      //Act
      var actual = request.ToAdsml();

      //Assert
      Assert.That(actual, Is.Not.Null);
      Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
      Assert.DoesNotThrow(() => actual.ValidateAdsmlResponse("adsml.xsd"));
    }

    [Test]
    public void Can_Generate_Api_Xml_With_LookupControl() {
      //Arrange
      XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
      var expected = 
        new XElement("BatchRequest",
            new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
            new XAttribute(XNamespace.Xmlns + "xsi", xsi),
            new XElement("CreateRequest",
                new XAttribute("name", "fooPath"),
                new XAttribute("type", "fooObjectTypeName"),
                new XElement("LookupControls",
                    new XElement("AttributesToReturn",
                        new XElement("Attribute",
                            new XAttribute("name", "fooAttributeName")))),
                new XElement("AttributesToSet",
                    new XElement("StructureAttribute",
                        new XAttribute("id", "215"),
                        new XAttribute("name", "fooAttributeName"),
                        new XElement("StructureValue",
                            new XAttribute("langId", "10"),
                            new XAttribute("scope", "global"),
                            new XCData("fooValue"))))));
      
      var value = new StructureValue {LanguageId = 10, Value = "fooValue"};
      var attribute = new StructureAttribute {
        DefinitionId = 215,
        Name = "fooAttributeName",
        Values = new List<StructureValue> {value}
      };

      var lcb = new LookupControlBuilder();
      lcb.ReturnAttributes(AttributeToReturn.WithName("fooAttributeName"));
      var lc = lcb.Build();

      var request = new CreateRequest("fooObjectTypeName", "fooPath", null, attribute) {LookupControl = lc};

      //Act
      var actual = request.ToAdsml();
      Console.WriteLine(actual.ToString());

      //Assert
      Assert.That(actual, Is.Not.Null);
      Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
      Assert.DoesNotThrow(() => actual.ValidateAdsmlResponse("adsml.xsd"));
    }
  }
}