using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Components
{
    [TestFixture]
    public class ReferenceOptionsFixture
    {
        [Test]
        public void Can_Generate_A_UseChannel_Filter() {
            //Arrange
            var expected = new XAttribute("channelId", "3");
            var filter = ReferenceOptions.UseChannel(3);

            //Act
            var actual = filter.ToAdsml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_A_ValuesOnly_Filter() {
            //Arrange
            var expected = new XAttribute("valueOnly", "true");
            var filter = ReferenceOptions.ReturnValuesOnly();

            //Act
            var actual = filter.ToAdsml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_A_ResolveAttributes_Filter() {
            //Arrange
            var expected = new XAttribute("resolveAttributes", "true");
            var filter = ReferenceOptions.ResolveAttributes();

            //Act
            var actual = filter.ToAdsml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_A_ResolveSpecialCharacters_Filter() {
            //Arrange
            var expected = new XAttribute("resolveSpecialCharacters", "true");
            var filter = ReferenceOptions.ResolveSpecialCharacters();

            //Act
            var actual = filter.ToAdsml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_A_ResolvePrices_Filter() {
            //Arrange
            var expected = new XAttribute("resolvePrices", "true");
            var filter = ReferenceOptions.ResolvePrices();

            //Act
            var actual = filter.ToAdsml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_A_ResolvePriceFields_Filter() {
            //Arrange
            var expected = new XAttribute("resolvePriceFields", "true");
            var filter = ReferenceOptions.ResolvePriceFields();

            //Act
            var actual = filter.ToAdsml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }
    }
}