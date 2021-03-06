using System;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Requests
{
  [TestFixture]
  public class AqlSearchRequestFixture
  {
    [Test]
    public void Can_Generate_Api_Xml_With_DefinitionId() {
      //Arrange
      var expected = 
        new XElement("SearchRequest",
          new XAttribute("base", "/Structures/Classification/JULA Produkter"),
          new XElement("Filter",
            new XElement("FilterString", "FIND BELOW #10 WHERE (#10 = \"foo\")")));

      var aql =
        new AqlSearchRequest(
          "/Structures/Classification/JULA Produkter",
          AqlQueryTypes.Below,
          new IdNameReference(10),
          "#10 = \"foo\"",
          null
        );

      //Act
      var actual = aql.ToAdsml();
      var request = new BatchRequest(aql).ToAdsml();

      Console.WriteLine(actual.ToString());

      //Assert
      Assert.That(actual, Is.Not.Null);
      Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
      Assert.DoesNotThrow(() => request.ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void Can_Generate_Api_Xml_With_Omission_Of_Attributes() {
      //Arrange
      var expected =
        new XElement("SearchRequest",
          new XAttribute("base", "/foo/bar"),
          new XAttribute("returnNoAttributes", "true"),
          new XElement("Filter",
            new XElement("FilterString", "FIND BELOW #10 WHERE (#10 = \"foo\")")));

      var aql =
        new AqlSearchRequest(
          "/foo/bar",
          new ISearchRequestFilter[] {Filter.ReturnNoAttributes()},
          AqlQueryTypes.Below,
          new IdNameReference(10),
          "#10 = \"foo\"",
          null
        );

      //Act
      var actual = aql.ToAdsml();
      var request = new BatchRequest(aql).ToAdsml();

      Console.WriteLine(actual.ToString());

      //Assert
      Assert.That(actual, Is.Not.Null);
      Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
      Assert.DoesNotThrow(() => request.ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void Can_Generate_Api_Xml_With_SearchControl_Filters() {
      //Arrange
      var expected = 
        new XElement("SearchRequest",
            new XAttribute("base", "/foo/bar"),
            new XAttribute("returnNoAttributes", "true"),
            new XElement("Filter",
                new XElement("FilterString", "FIND BELOW #10 WHERE (#10 = \"foo\")")),
            new XElement("SearchControls", new XAttribute("excludeBin", "true"), new XAttribute("excludeDocument", "true")));

      var builder = new SearchControlBuilder();
      builder.AddRequestFilters(Filter.ExcludeBin(), Filter.ExcludeDocument());

      var searchControls = builder.Build();

      var aql =
        new AqlSearchRequest(
          "/foo/bar",
          new ISearchRequestFilter[] {Filter.ReturnNoAttributes()},
          AqlQueryTypes.Below,
          new IdNameReference(10),
          "#10 = \"foo\"",
          searchControls
        );

      //Act
      var actual = aql.ToAdsml();
      var request = new BatchRequest(aql).ToAdsml();
      
      Console.WriteLine(actual.ToString());

      //Assert
      Assert.That(actual, Is.Not.Null);
      Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
      Assert.DoesNotThrow(() => request.ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void Can_Generate_Api_Xml_With_SearchControl_Filters_And_AttributesToReturn() {
      //Arrange
      var expected =
        new XElement("SearchRequest",
            new XAttribute("base", "/foo/bar"),
            new XAttribute("returnNoAttributes", "true"),
            new XElement("Filter",
              new XElement("FilterString", "FIND BELOW #10 WHERE (#10 = \"foo\")")),
            new XElement("SearchControls",
                new XAttribute("excludeBin", "true"),
                new XAttribute("excludeDocument", "true"),
                new XElement("AttributesToReturn",
                    new XElement("Attribute",
                        new XAttribute("name", "Foo")))));

      var builder = new SearchControlBuilder();

      builder
        .AddRequestFilters(
          Filter.ExcludeBin(),
          Filter.ExcludeDocument()
        )
        .ReturnAttributes(AttributeToReturn.WithName("Foo"));

      var searchControls = builder.Build();

      var aql =
        new AqlSearchRequest(
          "/foo/bar",
          new ISearchRequestFilter[] {Filter.ReturnNoAttributes()},
          AqlQueryTypes.Below,
          new IdNameReference(10),
          "#10 = \"foo\"",
          searchControls
        );

      //Act
      var actual = aql.ToAdsml();
      var request = new BatchRequest(aql).ToAdsml();
      
      Console.WriteLine(actual.ToString());

      //Assert
      Assert.That(actual, Is.Not.Null);
      Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
      Assert.DoesNotThrow(() => request.ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void Can_Generate_Api_Xml_With_SearchControl_ReferenceControls() {
      //Arrange
      var expected =
        new XElement("SearchRequest",
            new XAttribute("base", "/foo/bar"),
            new XAttribute("returnNoAttributes", "true"),
            new XElement("Filter",
                new XElement("FilterString", "FIND BELOW #10 WHERE (#10 = \"foo\")")),
            new XElement("SearchControls",
                new XElement("ReferenceControls",
                    new XAttribute("channelId", "3"),
                    new XAttribute("resolveAttributes", "true"),
                    new XAttribute("resolveSpecialCharacters", "true"),
                    new XAttribute("valueOnly", "true"))));

      var builder = new SearchControlBuilder();

      builder.ConfigureReferenceHandling(
        ReferenceOptions.UseChannel(3),
        ReferenceOptions.ResolveAttributes(),
        ReferenceOptions.ResolveSpecialCharacters(),
        ReferenceOptions.ReturnValuesOnly()
      );

      var searchControls = builder.Build();

      var aql =
        new AqlSearchRequest(
          "/foo/bar",
          new ISearchRequestFilter[] { Filter.ReturnNoAttributes() },
          AqlQueryTypes.Below,
          new IdNameReference(10),
          "#10 = \"foo\"",
          searchControls
        );

      //Act
      var actual = aql.ToAdsml();
      var request = new BatchRequest(aql).ToAdsml();
      
      Console.WriteLine(actual.ToString());

      //Assert
      Assert.That(actual, Is.Not.Null);
      Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
      Assert.DoesNotThrow(() => request.ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void Should_Default_ObjectTypeToMatch_To_Any_If_Not_Specified() {
      //Arrange
      var builder = new AqlQueryBuilder();
      builder.QueryString("foo");

      //Act
      var requestXml = builder.Build().ToAdsml();
      var filterString = requestXml.Descendants("Filter").Single().Value;

      //Assert
      Assert.That(filterString.Contains("ANY"));
    }

    [Test]
    [ExpectedException(typeof (ApiSerializationValidationException), ExpectedMessage = "An AQL QueryString must be provided.")]
    public void Validate_Should_Throw_ASVE_If_No_QueryString_Is_Provided() {
      //Arrange
      var builder = new AqlQueryBuilder();
      var request = builder.Build();

      //Act
      request.Validate();
    }

    [Test]
    [ExpectedException(typeof (ApiSerializationValidationException), ExpectedMessage = "To use a specific QueryType the base path must be provided.")]
    public void Validate_Should_Throw_ASVE_If_QueryType_But_No_BasePath_Is_Provided() {
      //Arrange
      var builder = new AqlQueryBuilder();
      builder.QueryType(AqlQueryTypes.Below)
             .QueryString("foo");

      var request = builder.Build();

      //Act
      request.Validate();
    }
  }
}