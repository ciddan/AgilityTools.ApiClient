using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class ApiSerializationValidationExceptionFixture
    {
        [Test]
        public void Can_Instantiate_New_ApiSerializationValidationException() {
            //Act
            var aex = new ApiSerializationValidationException();

            //Assert
            Assert.That(aex, Is.Not.Null);
        }

        [Test]
        public void Can_Instantiate_New_ApiSerializationValidationException_With_Message() {
            //Act
            var aex = new ApiSerializationValidationException("foo");

            //Assert
            Assert.That(aex.Message, Is.EqualTo("foo"));
        }

        [Test]
        public void Can_Instantiate_New_ApiSerializationValidationException_With_Message_And_InnerEx() {
            //Act
            var aex = new ApiSerializationValidationException("foo", new Exception("bar"));

            //Assert
            Assert.That(aex.Message, Is.EqualTo("foo"));
            Assert.That(aex.InnerException, Is.InstanceOf<Exception>());
            Assert.That(aex.InnerException.Message, Is.EqualTo("bar"));
        }

        [Test]
        public void ApiSerializationValidationException_Implements_Serialization() {
            //Arrange
            var aex = new ApiSerializationValidationException("foo");
            var formatter = new BinaryFormatter();
            var str = new MemoryStream();

            //Act
            formatter.Serialize(str, aex);

            //Assert
            Assert.That(str.Length, Is.AtLeast(1));
        }
    }
}