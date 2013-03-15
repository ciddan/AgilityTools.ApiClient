using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Components
{
  [TestFixture]
  public class AttributeControlFixture
  {
    [Test]
    public void CanSpecifyAttributesToReturnWithAttributeNodes() {
      //Arrange
      string expected = 
        new XElement("AttributesToReturn",
            new XElement("Attribute", new XAttribute("name", "foo"))).ToString();

      //Act
      var acon = new AttributeControl(AttributeToReturn.WithName("foo"));

      //Assert
      Assert.That(acon, Is.Not.Null);
      Assert.That(acon.ToAdsml().ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void CanSpecifyNodeName() {
      //Act
      var acon = new AttributeControl("Foo", new AttributeTypeToReturn {Type = AttributeDataType.StructureText});

      //Assert
      Assert.That(acon.ToAdsml().Name.ToString(), Is.EqualTo("Foo"));
    }

    [Test]
    public void CanSpecifyIdList() {
      //Arrange
      string expected = new XElement("Foo", new XAttribute("idlist", "1, 2, 3, 4")).ToString();

      //Act
      var acon = new AttributeControl("Foo", new [] {1, 2, 3, 4});

      //Assert
      Assert.That(acon.ToAdsml().ToString(), Is.EqualTo(expected));
    }

    [Test]
    public void Throws_ArgumentNullException_If_No_AttributesToReturn_Are_Specified() {
      //Assert
      Assert.Throws<ArgumentNullException>(() => new AttributeControl("AttributesToReturn", attributesToReturn: null));
    }
  }
}