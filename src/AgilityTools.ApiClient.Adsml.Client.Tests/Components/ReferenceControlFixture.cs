using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Components
{
    [TestFixture]
    public class ReferenceControlFixture
    {
        [Test]
        public void Can_Instantiate_New_ReferenceControl() {
            //Act
            var asc = new ReferenceControl();

            //Assert
            Assert.That(asc, Is.Not.Null);
        }

        [Test]
        public void Ctor_Accepts_ReferenceOptions_Param_Array() {
            //Act
            var sc = new ReferenceControl(ReferenceOptions.ResolveAttributes());

            //Assert
            Assert.That(sc, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: referenceOptions")]
        public void Ctor_Throws_ArgumentNullException_If_ParamArray_Is_null()
        {
            //Act
            new ReferenceControl(null);
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            var expected = new XElement("ReferenceControls", new XAttribute("valueOnly", "true"));
            var asc = new ReferenceControl(ReferenceOptions.ReturnValuesOnly());

            //Act
            var actual = asc.ToAdsml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_Outer_Node_XAttributes() {
            //Arrange
            var expected = new XElement("ReferenceControls", new XAttribute("foo", "bar"), new XAttribute("valueOnly", "true"));
            var asc = new ReferenceControl(ReferenceOptions.ReturnValuesOnly())
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