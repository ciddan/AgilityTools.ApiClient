using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Requests
{
  [TestFixture]
  public class LookupRequestFixture
  {
    [Test]
    public void CanInstatiateNewLookupRequest() {
      //Act
      var request = new LookupRequest("foo");
      
      //Assert
      Assert.That(request, Is.Not.Null);
    }
    
    [Test]
    public void CanInstatiateNewLookupRequestWithLookupControls() {
      //Act
      var request = new LookupRequest("foo", new LookupControl(null, null));
      
      //Assert
      Assert.That(request, Is.Not.Null);
    }
    
    [Test]
    [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "name cannot be null or empty.")]
    public void CtorThrowsInvalidOperationExceptionIfNameIsEmpty() {
      //Act
      new LookupRequest("");
    }
    
    [Test]
    [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "name cannot be null or empty.")]
    public void CtorThrowsInvalidOperationExceptionIfNameIsNull() {
      //Act
      new LookupRequest(null);
    }
    
    [Test]
    public void CanGenerateBasicApiXml() {
      //Arrange
      string expected = new XElement("LookupRequest", new XAttribute("name", "/foo/bar")).ToString();
      
      //Act
      var req = new LookupRequest("/foo/bar");
      var actual = req.ToAdsml();
      var batchRequest = new BatchRequest(req);
      
      Console.WriteLine(actual);
      
      //Assert
      Assert.That(actual.ToString(), Is.EqualTo(expected));
      Assert.DoesNotThrow(() => batchRequest.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void CanGenerateApiXmlWithFilter() {
      //Arrange
      string expected = 
        new XElement("LookupRequest", 
          new XAttribute("name", "/foo/bar"), 
          new XAttribute("returnNoAttributes", "true")
        ).ToString();

      //Act
      var req = new LookupRequest("/foo/bar", null, Filter.ReturnNoAttributes(true));
      var actual = req.ToAdsml();
      var batchRequest = new BatchRequest(req);

      Console.WriteLine(actual);

      //Assert
      Assert.That(actual.ToString(), Is.EqualTo(expected));
      Assert.DoesNotThrow(() => batchRequest.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }


    [Test]
    public void CanGenerateApiXmlWithLookupControl() {
      //Arrange
      string expexcted = 
        new XElement("LookupRequest",
          new XAttribute("name", "/Schema/Attribute Sets/Översättningsattribut"),
          new XElement("LookupControls",
            new XElement("AttributesToReturn",
              new XAttribute("namelist", "members")),
          new XElement("LanguagesToReturn",
            new XElement("Language",
              new XAttribute("id", "10"))))).ToString();
      
      var lookupBuilder = new LookupControlBuilder();
      lookupBuilder.AttributeNamelist("members").ReturnLanguages(LanguageToReturn.WithLanguageId(10));
      
      //Act
      var req = new LookupRequest("/Schema/Attribute Sets/Översättningsattribut", lookupBuilder.Build());
      var actual = req.ToAdsml();
      var batchRequest = new BatchRequest(req);

      Console.WriteLine(actual);
      
      //Assert
      Assert.That(actual.ToString(), Is.EqualTo(expexcted));
      Assert.DoesNotThrow(() => batchRequest.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }
  }
}