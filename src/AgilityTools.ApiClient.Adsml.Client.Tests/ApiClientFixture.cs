using System;
using System.Threading;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using AgilityTools.ApiClient.Adsml.Communication;
using Moq;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class ApiClientFixture
    {
        private IApiClient _client;

        [SetUp]
        public void Setup() {
            _client = new ApiClient(new ApiWebClient());
        }

        [TearDown]
        public void Cleanup() {
            _client.Dispose();
        }

        [Test]
        public void Can_Instantiate_New_ApiClient() {
            //Act
            IApiClient client = new ApiClient(new ApiWebClient());

            //Assert
            Assert.That(client, Is.Not.Null);
        }

        [Test]
        public void Can_Instantiate_New_ApiClient_And_Supply_Specific_Endpoint_Url() {
            //Act
            IApiClient client = new ApiClient(new ApiWebClient(), "http://agilitytest:9080/Agility/Directory");

            //Assert
            Assert.That(client, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: webClient")]
        public void ApiClient_Ctor_Throws_ArgumentNullException_If_ApiWebClient_Is_Null() {
            //Act
            new ApiClient(null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: request")]
        public void SendRequest_Throws_ArgumentNullException_If_Request_Is_Null() {
            //Arrange
            var client = new ApiClient(new ApiWebClient());

            //Act
            client.SendApiRequest<CreateRequest>(null);
        }

        [Test]
        public void Can_Dispose_Api_Client() {
            //Arrange
            var mockWebClient = new Mock<IApiWebClient>();
            mockWebClient.Setup(s => s.Dispose()).Verifiable();

            IApiClient client = new ApiClient(mockWebClient.Object);

            //Act
            client.Dispose();

            //Assert
            mockWebClient.Verify(s => s.Dispose(), Times.Once());
        }

        [Test]
        public void Can_Send_ApiRequests_Via_ApiClient() {
            //Arrange
            var request = new CreateRequest("lol", "lol", StructureAttribute.New(215, new StructureValue(10, "foo")));

            //Act
            var result = _client.SendApiRequest(request);

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<XElement>());
        }

        [Test]
        public void Can_Send_Async_ApiRequests_Via_ApiClient() {
            //Arrange
            var request = new CreateRequest("lol", "lol", StructureAttribute.New(215, new StructureValue(10, "foo")));

            var manualEvent = new ManualResetEvent(false);
            bool callbackCalled = false;

            //Act
            XElement result = null;

            _client.SendApiRequestAsync(request, data => {
                                                     result = data;
                                                     callbackCalled = true;
                                                     manualEvent.Set();
                                                 });

            manualEvent.WaitOne();

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(callbackCalled, Is.True);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: request")]
        public void SendRequestAsync_Throws_ArgumentNullException_If_Request_Is_Null() {
            //Arrange
            var client = new ApiClient(new ApiWebClient());

            //Act
            client.SendApiRequestAsync<CreateRequest>(null, null);
        }
    }
}