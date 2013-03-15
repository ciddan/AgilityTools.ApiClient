using System;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class IdNameReferenceFixture
    {
        [Test]
        public void Can_Initialize_New_IdNameReference() {
            //Act
            var ttf = new IdNameReference(10);

            //Assert
            Assert.That(ttf, Is.Not.Null);
        }

        [Test]
        public void ToString_Returns_Correct_Value_With_DefId() {
            //Arrange
            var ttf = new IdNameReference(10);

            //Act
            var ttfStr = ttf.ToString();

            //Assert
            Assert.That(ttfStr, Is.EqualTo("#10"));
        }

        [Test]
        public void ToString_Returns_Correct_Value_With_Name() {
            //Arrange
            var ttf = new IdNameReference("foo");

            //Act
            var ttfStr = ttf.ToString();

            //Assert
            Assert.That(ttfStr, Is.EqualTo("\"foo\""));

            Console.WriteLine(ttfStr);
        }
    }
}