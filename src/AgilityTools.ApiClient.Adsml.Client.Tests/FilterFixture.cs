using System.Xml.Linq;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
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
            Assert.That(actual.ToApiXml().ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_A_ExcludeDocument_Filter_Tag() {
            //Arrange
            var expected = new XAttribute("excludeDocument", "true");

            //Act
            var actual = Filter.ExcludeDocument();

            //Assert
            Assert.That(actual.ToApiXml().ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_A_OmitStructureAttributes_Filter_Tag() {
            //Arrange
            var expected = new XAttribute("returnAllAttributes", "false");

            //Act
            var actual = Filter.OmitStructureAttributes();

            //Assert
            Assert.That(actual.ToApiXml().ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_A_AllowPaging_Filter_Tag() {
            //Arrange
            var expected = new XAttribute("allowPaging", "true");

            //Act
            var actual = Filter.AllowPaging();

            //Assert
            Assert.That(actual.ToApiXml().ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_A_PageSize_Filter_Tag() {
            //Arrange
            var expected = new XAttribute("pageSize", "1");

            //Act
            var actual = Filter.PageSize(1);

            //Assert
            Assert.That(actual.ToApiXml().ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_A_CountLimit_Filter_Tag() {
            //Arrange
            var expected = new XAttribute("countLimit", "1");

            //Act
            var actual = Filter.CountLimit(1);

            //Assert
            Assert.That(actual.ToApiXml().ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        [ExpectedException(typeof (ApiSerializationValidationException))]
        public void PageSizeFilter_Throws_When_PageSize_Is_0() {
            //Act
            Filter.PageSize(0).ToApiXml();
        }

        [Test]
        [ExpectedException(typeof (ApiSerializationValidationException))]
        public void PageSizeFilter_Throws_When_PageSize_Is_Negative() {
            //Act
            Filter.PageSize(-1).ToApiXml();
        }

        [Test]
        [ExpectedException(typeof (ApiSerializationValidationException))]
        public void CountLimitFilter_Throws_When_CountLimit_Is_0() {
            //Act
            Filter.CountLimit(0).ToApiXml();
        }

        [Test]
        [ExpectedException(typeof (ApiSerializationValidationException))]
        public void CountLimitFilter_Throws_When_CountLimit_Is_Negative() {
            //Act
            Filter.CountLimit(-1).ToApiXml();
        }
    }
}