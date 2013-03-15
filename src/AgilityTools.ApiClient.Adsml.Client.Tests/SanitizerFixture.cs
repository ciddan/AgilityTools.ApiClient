using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class SanitizerFixture
    {
        [Test]
        public void SanitizeContextName_Extension_Correctly_Replaces_ForwardSlashes() {
            //Arrange
            const string expected = "foo@fs:bar";
            const string foo = "foo/bar";

            //Act
            string actual = foo.SanitizeContextName();

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void SanitizeContextName_Extension_Returns_Null_If_String_Is_Null() {
            //Arrange
            const string expected = null;
            const string foo = null;

            //Act
            string actual = foo.SanitizeContextName();

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void SanitizeContextName_Extension_Returns_Empty_String_If_String_Is_Empty() {
            //Arrange
            string expected = string.Empty;
            string foo = string.Empty;

            //Act
            string actual = foo.SanitizeContextName();

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}