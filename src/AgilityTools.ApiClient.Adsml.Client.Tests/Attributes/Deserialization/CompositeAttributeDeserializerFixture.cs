using System;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Attributes.Deserialization
{
  [TestFixture]
  public class CompositeAttributeDeserializerFixture
  {
    [Test]
    public void Instatiation() {
      //Act
      var cad = new CompositeAttributeDeserializer();
      
      //Assert
      Assert.That(cad, Is.Not.Null);
    }
    
    [Test]
    public void Can_Deserialze_CompositeAttribute() {
      //Arrange
      var compositeAttribute =
        new XElement("CompositeAttribute",
             new XAttribute("name", "members"),
             new XElement("CompositeValue",
             new XElement("Field",
             new XAttribute("name", "id"),
             new XAttribute("type", "integer"),
             new XCData("28")),
             new XElement("Field",
             new XAttribute("name", "name"),
             new XAttribute("type", "text"),
             new XCData("Säljtext2")),
             new XElement("Field",
             new XAttribute("name", "dataType"),
             new XAttribute("type", "text"),
             new XCData("Text"))));

      var deserializer = new CompositeAttributeDeserializer();
      
      //Act
      IAdsmlAttribute deserialized = deserializer.Deserialize(compositeAttribute);
      var actual = deserialized as CompositeAttribute;
      
      if (actual == null) 
        Assert.Fail("actual is not of type CompositeAttribute");
      
      var fields = actual.CompositeValues.First().Fields;
      
      //Assert
      Assert.That(deserialized is CompositeAttribute);
      Assert.That(actual.Name, Is.EqualTo("members"));
      Assert.That(actual.CompositeValues.Count(), Is.EqualTo(1));
      Assert.That(actual.CompositeValues.First().Fields.Count(), Is.EqualTo(3));
      
      Assert.That(fields.Any(f => f.Name == "id"));
      Assert.That(fields.Any(f => f.Name == "name"));
      Assert.That(fields.Any(f => f.Name == "dataType"));
      
      Assert.That(fields.Any(f => f.Value == "28"));
      Assert.That(fields.Any(f => f.Value == "Säljtext2"));
      Assert.That(fields.Any(f => f.Value == "Text"));
      
      Assert.That(fields.Any(f => f.Type == "text"));
      Assert.That(fields.Any(f => f.Type == "integer"));
    }
    
    [Test]
    public void Deserialize_Returns_Correct_Object_With_Less_Data_Input() {
      //Arrange
      var compositeAttribute = new XElement("CompositeAttribute", new XAttribute("name", "foo"));
      
      var des = new CompositeAttributeDeserializer();
      
      //Act
      var compAttr = (CompositeAttribute) des.Deserialize(compositeAttribute);
      
      //Assert
      Assert.That(compAttr, Is.Not.Null);
      Assert.That(compAttr.Name, Is.EqualTo("foo"));
    }
    
    [Test]
    public void Deserialize_Throws_ArgumentNullException_If_Element_Is_Null() {
      //Arrange
      var deserializer = new CompositeAttributeDeserializer();
      
      //Assert
      Assert.Throws<ArgumentNullException>(() => deserializer.Deserialize(null));
    }
    
    [Test]
    [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Not a valid CompositeAttribute.")]
    public void Deserialize_Throws_InvalidOperationException_If_ElementName_Is_Incorrect() {
      //Arrange
      var des = new CompositeAttributeDeserializer();
      
      //Act
      des.Deserialize(new XElement("foo"));
    }
  }
}
