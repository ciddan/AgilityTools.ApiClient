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
    }

    [Test]
    public void Can_Build_Api_Xml() {
      //Arrange
      string expected = new XElement("DeleteRequest", new XAttribute("name", "foo")).ToString();

      //Act
      var deleteRequest = new DeleteRequest("foo");
      var batchRequest = new BatchRequest(deleteRequest);
      var actual = deleteRequest.ToAdsml();

      Console.WriteLine(actual);

      //Assert
      Assert.That(actual.ToString(), Is.EqualTo(expected));
      Assert.DoesNotThrow(() => batchRequest.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
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