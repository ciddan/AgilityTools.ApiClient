using System;
using System.Threading;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Communication.Tests
{
    [TestFixture]
    public class ApiWebClientFixture
    {
        [Test]
        public void Can_Instantiate_New_ApiWebClient() {
            //Act
            IApiWebClient client = new ApiWebClient();

            //Assert
            Assert.That(client, Is.Not.Null);
        }

        [Test]
        public void Can_Upload_Data_With_ApiWebClient() {
            //Arrange
            IApiWebClient client = new ApiWebClient();

            //Act
            var result = client.UploadString("http://agilitytest:9080/Agility/Directory", "req");

            //Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Can_Upload_Data_Async_With_ApiWebClient() {
            //Arrange
            var manualEvent = new ManualResetEvent(false);

            IApiWebClient client = new ApiWebClient();

            bool callbackCalled = false;

            Action<string> callback = data => {
                                          callbackCalled = true;
                                          manualEvent.Set();
                                      };

            //Act
            client.UploadStringAsync("http://penny:9080/Agility/Directory", string.Empty, callback);

            manualEvent.WaitOne();

            //Assert
            Assert.That(callbackCalled, Is.True);
        }

        [Test]
        public void Can_Dispose_ApiWebClient() {
            //Arrange
            IApiWebClient client = new ApiWebClient();

            //Act
            client.Dispose();
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException), ExpectedMessage = "Url must be provided.")]
        public void ApiWebClient_UploadData_Throws_InvalidOprtationException_If_Url_Is_NullOrEmpty() {
            //Arrange
            IApiWebClient client = new ApiWebClient();

            //Act
            client.UploadString(string.Empty, string.Empty);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException),
            ExpectedMessage = "Value cannot be null.\r\nParameter name: request")]
        public void ApiWebClient_UploadData_Throws_ArgumentNullException_If_Data_Is_Null() {
            //Arrange
            IApiWebClient client = new ApiWebClient();

            //Act
            client.UploadString("http://agilitytest:9080/Agility/Directory", null);
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException), ExpectedMessage = "Url cannot be empty.")]
        public void ApiWebClient_UploadDataAsync_Throws_InvalidOprtationException_If_Url_Is_NullOrEmpty() {
            //Arrange
            IApiWebClient client = new ApiWebClient();

            //Act
            client.UploadStringAsync(string.Empty, string.Empty, d => { });
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException),
            ExpectedMessage = "Value cannot be null.\r\nParameter name: data")]
        public void ApiWebClient_UploadDataAsync_Throws_ArgumentNullException_If_Data_Is_Null() {
            //Arrange
            IApiWebClient client = new ApiWebClient();

            //Act
            client.UploadStringAsync("http://agilitytest:9080/Agility/Directory", null, d => { });
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException),
            ExpectedMessage = "Value cannot be null.\r\nParameter name: callback")]
        public void ApiWebClient_UploadDataAsync_Throws_ArgumentNullException_If_Callback_Is_Null() {
            //Arrange
            IApiWebClient client = new ApiWebClient();

            //Act
            client.UploadStringAsync("http://agilitytest:9080/Agility/Directory", "req", null);
        }
    }
}