using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Filters;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Components
{
    [TestFixture]
    public class FilterFixture
    {
        [Test]
        public void Can_Generate_A_ExcludeBin_Filter_Tag() {
            //Arrange
            var expected = new XAttribute("excludeBin", "true");

            //Act
            var actual = Filter.ExcludeBin();

            //Assert
            Assert.That(actual.ToAdsml().ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_A_ExcludeDocument_Filter_Tag() {
            //Arrange
            var expected = new XAttribute("excludeDocument", "true");

            //Act
            var actual = Filter.ExcludeDocument();

            //Assert
            Assert.That(actual.ToAdsml().ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_A_OmitStructureAttributes_Filter_Tag() {
            //Arrange
            var expected = new XAttribute("returnNoAttributes", "true");

            //Act
            var actual = Filter.ReturnNoAttributes();

            //Assert
            Assert.That(actual.ToAdsml().ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_A_AllowPaging_Filter_Tag() {
            //Arrange
            var expected = new XAttribute("allowPaging", "true");

            //Act
            var actual = Filter.AllowPaging();

            //Assert
            Assert.That(actual.ToAdsml().ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_A_PageSize_Filter_Tag() {
            //Arrange
            var expected = new XAttribute("pageSize", "1");

            //Act
            var actual = Filter.PageSize(1);

            //Assert
            Assert.That(actual.ToAdsml().ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_A_CountLimit_Filter_Tag() {
            //Arrange
            var expected = new XAttribute("countLimit", "1");

            //Act
            var actual = Filter.CountLimit(1);

            //Assert
            Assert.That(actual.ToAdsml().ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        [ExpectedException(typeof (ApiSerializationValidationException))]
        public void PageSizeFilter_Throws_When_PageSize_Is_0() {
            //Act
            Filter.PageSize(0).ToAdsml();
        }

        [Test]
        [ExpectedException(typeof (ApiSerializationValidationException))]
        public void PageSizeFilter_Throws_When_PageSize_Is_Negative() {
            //Act
            Filter.PageSize(-1).ToAdsml();
        }

        [Test]
        [ExpectedException(typeof (ApiSerializationValidationException))]
        public void CountLimitFilter_Throws_When_CountLimit_Is_0() {
            //Act
            Filter.CountLimit(0).ToAdsml();
        }

        [Test]
        [ExpectedException(typeof (ApiSerializationValidationException))]
        public void CountLimitFilter_Throws_When_CountLimit_Is_Negative() {
            //Act
            Filter.CountLimit(-1).ToAdsml();
        }
    }
}