using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Requests
{
  [TestFixture]
  public class DeleteRequestFixture
  {
    [Test]
    public void Can_Instantiate_New_DeleteRequest() {
      //Act
      var dr = new DeleteRequest("foo");

      //Assert
      Assert.That(dr, Is.Not.Null);
    }

    [Test]
    public void Ctor_Takes_ContextName() {
      //Act
      var dr = new DeleteRequest("/foo/bar");

      //Assert
      Assert.That(dr.ContextToDelete, Is.EqualTo("/foo/bar"));
      Assert.DoesNotThrow(() => dr.ToAdsml().ValidateAdsmlResponse("adsml.xsd"));
    }

    [Test]
    public void Can_Build_Api_Xml() {
      //Arrange
      XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
      string expected = 
        new XElement("BatchRequest",
            new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
            new XAttribute(XNamespace.Xmlns + "xsi", xsi),
            new XElement("DeleteRequest",
                new XAttribute("name", "foo"))).ToString();

      var request = new DeleteRequest("foo");

      //Act
      var actual = request.ToAdsml();

      Console.WriteLine(actual);

      //Assert
      Assert.That(actual.ToString(), Is.EqualTo(expected));
      Assert.DoesNotThrow(() => actual.ValidateAdsmlResponse("adsml.xsd"));
    }

    [Test]
    [ExpectedException(typeof (InvalidOperationException), ExpectedMessage = "ContextToDelete must be set.")]
    public void Throws_InvalidOperationException_If_ContextToDelete_Is_Not_Set() {
      //Arrange
      var request1 = new DeleteRequest(string.Empty);
      var request2 = new DeleteRequest(null);

      //Act
      try {
        request1.ToAdsml();
      } 
      catch (InvalidOperationException) {
      }
      catch (Exception) {
        Assert.Fail();
      }

      request2.ToAdsml();
    }
  }
}