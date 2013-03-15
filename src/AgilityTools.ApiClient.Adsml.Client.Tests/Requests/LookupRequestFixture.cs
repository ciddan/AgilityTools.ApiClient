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
    public void Can_Instatiate_New_LookupRequest() {
      //Act
      var request = new LookupRequest("foo");
      
      //Assert
      Assert.That(request, Is.Not.Null);
    }
    
    [Test]
    public void Can_Instatiate_New_LookupRequest_With_LookupControls() {
      //Act
      var request = new LookupRequest("foo", new LookupControl(null, null));
      
      //Assert
      Assert.That(request, Is.Not.Null);
    }
    
    [Test]
    [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "name cannot be null or empty.")]
    public void Ctor_Throws_InvalidOperationException_If_Name_Is_Empty() {
      //Act
      new LookupRequest("");
    }
    
    [Test]
    [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "name cannot be null or empty.")]
    public void Ctor_Throws_InvalidOperationException_If_Name_Is_Null() {
      //Act
      new LookupRequest(null);
    }
    
    [Test]
    public void Can_Generate_Basic_Api_Xml() {
      //Arrange
      string expected = new XElement("LookupRequest", new XAttribute("name", "/foo/bar")).ToString();
      
      //Act
      var req = new LookupRequest("/foo/bar");
      var actual = req.ToAdsml();
      var batchRequest = new BatchRequest(req);
      
      Console.WriteLine(actual.ToString());
      
      //Assert
      Assert.That(actual.ToString(), Is.EqualTo(expected));
      Assert.DoesNotThrow(() => batchRequest.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }
    
    [Test]
    public void Can_Generate_Api_Xml_With_LookupControl() {
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