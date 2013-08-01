using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Attributes
{
    [TestFixture]
    public class StructureValueFixture
    {
        [Test]
        public void Should_Be_Able_To_Instantiate_New_StructureValue() {
            //Act
            var value = new StructureValue();

            //Assert
            Assert.That(value, Is.Not.Null);
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            var expected = new XElement("StructureValue",
                                        new XAttribute("langId", "10"),
                                        new XAttribute("scope", "local"),
                                        new XCData("foo"));

            var value = new StructureValue {LanguageId = 10, Scope = Scopes.Local, Value = "foo"};

            //Act
            var actual = value.ToAdsml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Scope_Defaults_To_Global_If_Not_Set() {
            //Arrange
            var expected = new XElement("StructureValue",
                            new XAttribute("langId", "10"),
                            new XAttribute("scope", "global"),
                            new XCData("foo"));

            var value = new StructureValue { LanguageId = 10, Value = "foo" };

            //Act
            var actual = value.ToAdsml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Set_Scope_Using_Scope_Enum() {
            //Arrange
            var expected = new XElement("StructureValue",
                new XAttribute("langId", "10"),
                new XAttribute("scope", "edgespecific"),
                new XCData("foo"));

            var value = new StructureValue { LanguageId = 10, Value = "foo", Scope = Scopes.EdgeSpecific };

            //Act
            var actual = value.ToAdsml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "LanguageId has to be set.")]
        public void Validate_Throws_ApiSerializationValidationException_If_LanguageId_Is_Not_Set() {
            //Arrange
            var value = new StructureValue { Scope = Scopes.Local, Value = "foo" };

            //Act
            value.ToAdsml();
        }
    }
}