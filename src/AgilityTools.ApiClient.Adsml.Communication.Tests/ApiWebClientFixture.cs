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
            var result = client.UploadData("http://agilitytest:9080/Agility/Directory", "POST", new byte[] {});

            //Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void Can_Upload_Data_Async_With_ApiWebClient() {
            //Arrange
            var manualEvent = new ManualResetEvent(false);

            IApiWebClient client = new ApiWebClient();

            bool callbackCalled = false;

            Action<byte[]> callback = data => {
                                          callbackCalled = true;
                                          manualEvent.Set();
                                      };

            //Act
            client.UploadDataAsync("http://penny:9080/Agility/Directory", "POST", new byte[] {}, callback);

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
            client.UploadData(string.Empty, "POST", new byte[] {});
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException), ExpectedMessage = "A method must be provided.")]
        public void ApiWebClient_UploadData_Throws_InvalidOprtationException_If_Method_Is_NullOrEmpty() {
            //Arrange
            IApiWebClient client = new ApiWebClient();

            //Act
            client.UploadData("http://agilitytest:9080/Agility/Directory", string.Empty, new byte[] {});
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException),
            ExpectedMessage = "Value cannot be null.\r\nParameter name: data")]
        public void ApiWebClient_UploadData_Throws_ArgumentNullException_If_Data_Is_Null() {
            //Arrange
            IApiWebClient client = new ApiWebClient();

            //Act
            client.UploadData("http://agilitytest:9080/Agility/Directory", "POST", null);
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException), ExpectedMessage = "Url must be provided.")]
        public void ApiWebClient_UploadDataAsync_Throws_InvalidOprtationException_If_Url_Is_NullOrEmpty() {
            //Arrange
            IApiWebClient client = new ApiWebClient();

            //Act
            client.UploadDataAsync(string.Empty, "POST", new byte[] {}, d => { });
        }

        [Test]
        [ExpectedException(typeof (InvalidOperationException), ExpectedMessage = "A method must be provided.")]
        public void ApiWebClient_UploadDataAsync_Throws_InvalidOprtationException_If_Method_Is_NullOrEmpty() {
            //Arrange
            IApiWebClient client = new ApiWebClient();

            //Act
            client.UploadDataAsync("http://agilitytest:9080/Agility/Directory", string.Empty, new byte[] {}, d => { });
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException),
            ExpectedMessage = "Value cannot be null.\r\nParameter name: data")]
        public void ApiWebClient_UploadDataAsync_Throws_ArgumentNullException_If_Data_Is_Null() {
            //Arrange
            IApiWebClient client = new ApiWebClient();

            //Act
            client.UploadDataAsync("http://agilitytest:9080/Agility/Directory", "POST", null, d => { });
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException),
            ExpectedMessage = "Value cannot be null.\r\nParameter name: callback")]
        public void ApiWebClient_UploadDataAsync_Throws_ArgumentNullException_If_Callback_Is_Null() {
            //Arrange
            IApiWebClient client = new ApiWebClient();

            //Act
            client.UploadDataAsync("http://agilitytest:9080/Agility/Directory", "POST", new byte[] {}, null);
        }
    }
}