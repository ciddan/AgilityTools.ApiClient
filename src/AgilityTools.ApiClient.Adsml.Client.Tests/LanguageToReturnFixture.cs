using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class LanguageToReturnFixture
    {
        [Test]
        public void Can_Instantiate_New_LanguageToReturn() {
            //Act
            var ltr = new LanguageToReturn(10);

            //Assert
            Assert.That(ltr, Is.Not.Null);
        }

        [Test]
        public void Can_Create_New_LanguageToReturn_With_Static_Factorymethod() {
            //Act
            var ltr = LanguageToReturn.WithLanguageId(10);

            //Assert
            Assert.That(ltr, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "LanguageId cannot be negative or 0.")]
        public void Validate_Throws_ASVE_If_LanguageId_Is_0() {
            //Arrange
            var ltr = LanguageToReturn.WithLanguageId(0);

            //Act
            ltr.Validate();
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "LanguageId cannot be negative or 0.")]
        public void Validate_Throws_ASVE_If_LanguageId_Is_Negative() {
            //Arrange
            var ltr = LanguageToReturn.WithLanguageId(-1);

            //Act
            ltr.Validate();
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            var expected = new XElement("Language", new XAttribute("id", "10")).ToString();
            var ltr = LanguageToReturn.WithLanguageId(10);

            //Act
            var actual = ltr.ToAdsml().ToString();

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}