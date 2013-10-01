using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Requests
{
  [TestFixture]
  public class SimpleSearchRequestFixture
  {
    [Test]
    public void CanGenerateBasicSearchRequest() {
      //Arrange
      var expected = 
        new XElement("SearchRequest", new XAttribute("base", "/Structures/Classification/JULA Produkter"));

      var searchRequest = new SimpleSearchRequest("/Structures/Classification/JULA Produkter");

      //Act
      var actual = searchRequest.ToAdsml();
      var request = new BatchRequest(searchRequest).ToAdsml();

      Console.WriteLine(actual);

      //Assert
      Assert.That(actual, Is.Not.Null);
      Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
      Assert.DoesNotThrow(() => request.ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void CanGenerateSearchRequestWithRequestFilter() {
      //Arrange
      var expected = 
        new XElement("SearchRequest", 
          new XAttribute("base", "/Structures/Classification/JULA Produkter"),
          new XAttribute("returnNoAttributes", "true")
        );

      var searchRequest = 
        new SimpleSearchRequest("/Structures/Classification/JULA Produkter");
      searchRequest.RequestFilters.Add(Filter.ReturnNoAttributes());

      //Act
      var actual = searchRequest.ToAdsml();
      var request = new BatchRequest(searchRequest).ToAdsml();

      Console.WriteLine(actual);

      //Assert
      Assert.That(actual, Is.Not.Null);
      Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
      Assert.DoesNotThrow(() => request.ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void CanGenerateSearchRequestWithRequestFilterAndSearchControls() {
      //Arrange
      var expected = 
        new XElement("SearchRequest", 
          new XAttribute("base", "/Structures/Classification/JULA Produkter"),
          new XAttribute("returnNoAttributes", "true"),
            new XElement("SearchControls",
              new XElement("LanguagesToReturn",
                new XElement("Language", new XAttribute("id", "10"))))
        );

      var scb = new SearchControlBuilder();
      scb.ReturnLanguages(LanguageToReturn.WithLanguageId(10));

      var searchRequest = 
        new SimpleSearchRequest(
          "/Structures/Classification/JULA Produkter",
          scb.Build()
        );
      searchRequest.RequestFilters.Add(Filter.ReturnNoAttributes());

      //Act
      var actual = searchRequest.ToAdsml();
      var request = new BatchRequest(searchRequest).ToAdsml();

      Console.WriteLine(actual);

      //Assert
      Assert.That(actual, Is.Not.Null);
      Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
      Assert.DoesNotThrow(() => request.ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void CanGenerateSearchRequestWithRequestFilterSearchControlsAndAttributesToMatch() {
      //Arrange
      var expected = 
        new XElement("SearchRequest", 
          new XAttribute("base", "/Structures/Classification/JULA Produkter"),
          new XAttribute("returnNoAttributes", "true"),
            new XElement("Filter",
              new XElement("AttributesToMatch",
                new XElement("SimpleAttribute",
                  new XAttribute("name", "id"),
                  new XAttribute("type", "integer"),
                  new XElement("Value", new XCData("10"))))),
            new XElement("SearchControls",
              new XElement("LanguagesToReturn",
                new XElement("Language", new XAttribute("id", "10"))))
        );

      var scb = new SearchControlBuilder();
      scb.ReturnLanguages(LanguageToReturn.WithLanguageId(10));
      var atm = new AttributesToMatch(SimpleAttribute.New(AttributeTypes.Integer, "id", "10"));

      var searchRequest = 
        new SimpleSearchRequest(
          "/Structures/Classification/JULA Produkter",
          scb.Build(),
          atm
        );
      searchRequest.RequestFilters.Add(Filter.ReturnNoAttributes());

      //Act
      var actual = searchRequest.ToAdsml();
      var request = new BatchRequest(searchRequest).ToAdsml();

      Console.WriteLine(actual);

      //Assert
      Assert.That(actual, Is.Not.Null);
      Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
      Assert.DoesNotThrow(() => request.ValidateAdsmlDocument("adsml.xsd"));
    }
  }
}
