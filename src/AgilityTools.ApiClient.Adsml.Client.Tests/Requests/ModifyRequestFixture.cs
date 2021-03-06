using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Requests
{
  [TestFixture]
  public class ModifyRequestFixture
  {
    [Test]
    public void Can_Instantiate_New_ModifyRequest() {
      //Act
      var modReq = new ModifyRequest("foo", new List<ModificationItem>());

      //Assert
      Assert.That(modReq, Is.Not.Null);
    }

    [Test]
    public void Ctor_Throws_ArgumentNullException_If_Required_Params_Are_Null() {
      //Assert
      Assert.Throws<ArgumentNullException>(() => new ModifyRequest(null, new List<ModificationItem>()));
      Assert.Throws<ArgumentNullException>(() => new ModifyRequest("foo", null));
    }

    [Test]
    [ExpectedException(typeof (ApiSerializationValidationException), ExpectedMessage = "A context to modify must be specified.")]
    public void Validate_Throws_ApiSerializationValidationException_When_Context_Is_Empty() {
      //Arrange
      var modReq = new ModifyRequest(string.Empty, new List<ModificationItem> {
        ModificationItem.New(
          Modifications.AddValue,
          StructureAttribute.New(31, new StructureValue(10, "foo"))
        )
      });

      //Act
      modReq.ToAdsml();

      //Assert 
      Assert.Fail("Expected exception not thrown.");
    }

    [Test]
    [ExpectedException(typeof (ApiSerializationValidationException), ExpectedMessage = "At least one ModificationItem must be specified.")]
    public void Validate_Throws_ApiSerializationValidationException_When_Modifications_Is_Empty() {
      //Arrange
      var modReq = new ModifyRequest("foo", new List<ModificationItem>());

      //Act
      modReq.ToAdsml();

      //Assert 
      Assert.Fail("Expected exception not thrown.");
    }

    [Test]
    public void Can_Generate_Api_Xml() {
      //Arrange
      string expected =
        new XElement("ModifyRequest",
            new XAttribute("name", "/foo/bar"),
            new XElement("ModificationItem",
                new XAttribute("operation", "addValue"),
                new XElement("AttributeDetails",
                    new XElement("StructureAttribute",
                        new XAttribute("id", "31"),
                        new XElement("StructureValue",
                            new XAttribute("langId", "10"),
                            new XAttribute("scope", "global"),
                            new XCData("foo")))))).ToString();

      //Act
      var modReq = new ModifyRequest("/foo/bar", new List<ModificationItem> {
        ModificationItem.New(
          Modifications.AddValue,
          StructureAttribute.New(31, new StructureValue(10, "foo"))
        )
      });

      var actual = modReq.ToAdsml();
      var request = new BatchRequest(modReq);

      Console.WriteLine(actual.ToString());

      //Assert
      Assert.That(actual.ToString(), Is.EqualTo(expected));
      Assert.DoesNotThrow(() => request.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void Can_Generate_Api_Xml_With_LookupControls() {
      //Arrange
      string expected =
        new XElement("ModifyRequest",
            new XAttribute("name", "/foo/bar"),
            new XElement("ModificationItem",
                new XAttribute("operation", "addValue"),
                new XElement("AttributeDetails",
                    new XElement("StructureAttribute",
                        new XAttribute("id", "31"),
                        new XElement("StructureValue",
                            new XAttribute("langId", "10"),
                            new XAttribute("scope", "global"),
                            new XCData("foo"))))),
                new XElement("LookupControls",
                new XElement("AttributesToReturn",
                    new XElement("Attribute",
                        new XAttribute("name", "Artikelnummer"))),
                new XElement("LanguagesToReturn",
                    new XElement("Language",
                        new XAttribute("id", "10"))))).ToString();

      var lookupBuilder = new LookupControlBuilder();

      lookupBuilder.ReturnAttributes(AttributeToReturn.WithName("Artikelnummer"))
                   .ReturnLanguages(LanguageToReturn.WithLanguageId(10));

      //Act
      var modReq = new ModifyRequest("/foo/bar", new List<ModificationItem> {
        ModificationItem.New(
          Modifications.AddValue,
          StructureAttribute.New(31, new StructureValue(10, "foo"))
        )
      }) {
        LookupControl = lookupBuilder.Build()
      };

      var actual = modReq.ToAdsml();
      var request = new BatchRequest(modReq);

      Console.WriteLine(actual.ToString());

      //Assert
      Assert.That(actual.ToString(), Is.EqualTo(expected));
      Assert.DoesNotThrow(() => request.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void Can_Generate_Api_Xml_With_RequestFilters() {
      //Arrange
      string expected =
        new XElement("ModifyRequest",
            new XAttribute("name", "/foo/bar"),
            new XAttribute("returnNoAttributes", "true"),
            new XAttribute("failOnError", "true"),
            new XElement("ModificationItem",
                new XAttribute("operation", "addValue"),
                new XElement("AttributeDetails",
                    new XElement("StructureAttribute",
                        new XAttribute("id", "31"),
                        new XElement("StructureValue",
                            new XAttribute("langId", "10"),
                            new XAttribute("scope", "global"),
                            new XCData("foo")))))).ToString();

      //Act
      var modReq = new ModifyRequest("/foo/bar", new List<ModificationItem> {
        ModificationItem.New(
          Modifications.AddValue,
          StructureAttribute.New(31, new StructureValue(10, "foo"))
        )
      }) {
        RequestFilters = new List<IModifyRequestFilter> {
          Filter.ReturnNoAttributes(),
          Filter.FailOnError()
        }
      };

      var actual = modReq.ToAdsml();
      var request = new BatchRequest(modReq);

      Console.WriteLine(actual.ToString());

      //Assert
      Assert.That(actual.ToString(), Is.EqualTo(expected));
      Assert.DoesNotThrow(() => request.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }
  }
}