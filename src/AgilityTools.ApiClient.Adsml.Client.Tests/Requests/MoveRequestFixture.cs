using System;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;
using NUnit.Framework;

using AgilityTools.ApiClient.Adsml.Client.Requests;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Requests
{
  [TestFixture]
  public class MoveRequestFixture
  {
    [Test]
     public void Can_Instantiate_New_MoveRequest() {
      //Act
      var linkRequest = new MoveRequest("source", "target");

      //Assert
      Assert.That(linkRequest, Is.Not.Null);
      Assert.That(linkRequest.SourcePath, Is.EqualTo("source"));
      Assert.That(linkRequest.TargetPath, Is.EqualTo("target"));
     }

     [Test]
     public void Ctor_Throws_ArgumentNullException_If_Required_Params_Are_Null() {
      //Assert
      Assert.Throws<ArgumentNullException>(() => new MoveRequest(null, null));
      Assert.Throws<ArgumentNullException>(() => new MoveRequest("foo", null));
     }
    
    [Test]
    public void Can_Add_RequestFilter() {
      //Arrange
      var request = new LinkRequest("foo", "bar");

      //Act
      request.RequestFilters.Add(Filter.ReturnNoAttributes());

      //Assert
      Assert.That(request.RequestFilters.Count(), Is.EqualTo(1));
      Assert.That(request.RequestFilters.ElementAt(0), Is.InstanceOf<ReturnNoAttributesFilter>());
    }

    [Test]
    public void Can_Add_CopyControls() {
      //Act
      var request = new MoveRequest("foo", "bar") {
        CopyControl = new CopyControl(null)
      };

      //Assert
      Assert.That(request.CopyControl, Is.Not.Null);
    }

    [Test]
    public void Can_Generate_Api_Xml() {
      //Arrange
      string expected = 
        new XElement("MoveRequest",
          new XAttribute("name", "foo"),
          new XAttribute("targetLocation", "bar")).ToString();

      var request = new MoveRequest("foo", "bar");

      //Act
      var actual = request.ToAdsml();
      var batchRequest = new BatchRequest(request);

      Console.WriteLine(actual.ToString());

      //Assert
      Assert.That(actual.ToString(), Is.EqualTo(expected));
      Assert.DoesNotThrow(() => batchRequest.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void Can_Generate_Api_Xml_With_RequestFilter() {
      //Arrange
      string expected = 
        new XElement("MoveRequest",
          new XAttribute("name", "foo"),
          new XAttribute("targetLocation", "bar"),
          new XAttribute("returnNoAttributes", "true")).ToString();

      var request = new MoveRequest("foo", "bar") {
        RequestFilters = new List<IMoveRequestFilter> { Filter.ReturnNoAttributes() }
      };

      //Act
      var actual = request.ToAdsml();
      var batchRequest = new BatchRequest(request);

      Console.WriteLine(actual.ToString());

      //Assert
      Assert.That(actual.ToString(), Is.EqualTo(expected));
      Assert.DoesNotThrow(() => batchRequest.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void Can_Generate_Api_Xml_With_CopyControl() {
      //Arrange
      string expected = 
        new XElement("MoveRequest",
          new XAttribute("name", "foo"),
          new XAttribute("targetLocation", "bar"),
          new XElement("CopyControls",
            new XAttribute("copyLocalAttributes", "true"))).ToString();

      var request = new MoveRequest("foo", "bar") {
        CopyControl = new CopyControl(new List<ICopyControlFilter> { Filter.CopyLocalAttributesFromSource() })
      };

      //Act
      var actual = request.ToAdsml();
      var batchRequest = new BatchRequest(request);

      Console.WriteLine(actual.ToString());

      //Assert
      Assert.That(actual.ToString(), Is.EqualTo(expected));
      Assert.DoesNotThrow(() => batchRequest.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void Can_Generate_Api_Xml_With_CopyControl_Containing_LookupControl() {
      //Arrange
      string expected = 
        new XElement("MoveRequest",
          new XAttribute("name", "foo"),
          new XAttribute("targetLocation", "bar"),
            new XElement("CopyControls",
              new XAttribute("copyLocalAttributes", "true"),
            new XElement("LookupControls",
              new XElement("AttributesToReturn",
                new XElement("Attribute",
                  new XAttribute("name", "Artikelnummer")))))).ToString();

      var lookupBuilder = new LookupControlBuilder();
      lookupBuilder.ReturnAttributes(AttributeToReturn.WithName("Artikelnummer"));

      var request = new MoveRequest("foo", "bar") {
        CopyControl =
        new CopyControl(new List<ICopyControlFilter> {
          Filter.CopyLocalAttributesFromSource()
        },
        lookupBuilder.Build())
      };

      //Act
      var actual = request.ToAdsml();
      var batchRequest = new BatchRequest(request);

      Console.WriteLine(actual.ToString());

      //Assert
      Assert.That(actual.ToString(), Is.EqualTo(expected));
      Assert.DoesNotThrow(() => batchRequest.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "A source path must be provided.")]
    public void Validate_Throws_ApiSerializationValidationException_If_Source_Is_Empty() {
      //Arrange
      var req = new MoveRequest(string.Empty, "foo");

      //Act
      req.ToAdsml();
    }

    [Test]
    [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "A target path must be provided.")]
    public void Validate_Throws_ApiSerializationValidationException_If_Target_Is_Empty() {
      //Arrange
      var req = new MoveRequest("foo", string.Empty);

      //Act
      req.ToAdsml();
    }
  }
}

