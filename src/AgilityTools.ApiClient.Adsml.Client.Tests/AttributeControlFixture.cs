using System;
using System.Collections.Generic;
using System.Xml.Linq;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class AttributeControlFixture
    {
        [Test]
        public void Can_Instantiate_New_AttributeControl() {
            //Act
            var asc = new AttributeControl();

            //Assert
            Assert.That(asc, Is.Not.Null);
        }

        [Test]
        public void Ctor_Accepts_AttributeToReturn_Param_Array() {
            //Act
            var sc = new AttributeControl(new AttributeToReturn());

            //Assert
            Assert.That(sc, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: attributesToReturn")]
        public void Ctor_Throws_ArgumentNullException_If_ParamArray_Is_null() {
            //Act
            new AttributeControl(null);
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            var expected = new XElement("AttributesToReturn", new XElement("Attribute", new XAttribute("id", "20")));
            var asc = new AttributeControl(new AttributeToReturn {DefinitionId = 20});

            //Act
            var actual = asc.ToAdsml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_Outer_Node_XAttributes()
        {
            //Arrange
            var expected = new XElement("AttributesToReturn", new XAttribute("foo", "bar"),
                                        new XElement("Attribute", new XAttribute("id", "20")));
            
            var asc = new AttributeControl(new AttributeToReturn { DefinitionId = 20 })
                      {
                          OuterNodeAttributes = new List<XAttribute> {new XAttribute("foo", "bar")}
                      };

            //Act
            var actual = asc.ToAdsml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }
    }
}