using System;
using System.Collections.Generic;
using System.Xml.Linq;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class LanguageControlFixture
    {
        [Test]
        public void Can_Instantiate_New_LanguageControl() {
            //Act
            var asc = new LanguageControl();

            //Assert
            Assert.That(asc, Is.Not.Null);
        }

        [Test]
        public void Ctor_Accepts_LanguageToReturn_Param_Array() {
            //Act
            var sc = new LanguageControl(new LanguageToReturn(10));

            //Assert
            Assert.That(sc, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: languagesToReturn")]
        public void Ctor_Throws_ArgumentNullException_If_ParamArray_Is_null() {
            //Act
            new LanguageControl(null);
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            var expected = new XElement("LanguagesToReturn", new XElement("Language", new XAttribute("id", "10")));
            var asc = new LanguageControl(LanguageToReturn.WithLanguageId(10));

            //Act
            var actual = asc.ToAdsml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_Outer_Node_XAttributes() {
            //Arrange
            var expected = new XElement("LanguagesToReturn", new XAttribute("foo", "bar"),
                                        new XElement("Language", new XAttribute("id", "10")));

            var asc = new LanguageControl(LanguageToReturn.WithLanguageId(10))
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