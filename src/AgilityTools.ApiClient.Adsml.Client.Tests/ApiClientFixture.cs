using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using AgilityTools.ApiClient.Adsml.Client.Responses;
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
        [ExpectedException(typeof (ArgumentNullException),
            ExpectedMessage = "Value cannot be null.\r\nParameter name: webClient")]
        public void ApiClient_Ctor_Throws_ArgumentNullException_If_ApiWebClient_Is_Null() {
            //Act
            new ApiClient(null);
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException),
            ExpectedMessage = "Value cannot be null.\r\nParameter name: request")]
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
            var builder = new AqlQueryBuilder();

            builder.BasePath("/Structures/Classification/JULA Produkter")
                .QueryType(AqlQueryTypes.Below)
                .ObjectTypeToFind(12)
                .QueryString("#215 = \"169010\"")
                .ConfigureSearchControls()
                    .AddRequestFilters(Filter.ReturnNoAttributes());

            //Act
            var result = _client.SendApiRequest(builder.Build());

            //Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<XElement>());
        }

        [Test]
        public void Can_Send_Async_ApiRequests_Via_ApiClient() {
            //Arrange
            var builder = new AqlQueryBuilder();

            builder.BasePath("/Structures/Classification/JULA Produkter")
                .QueryType(AqlQueryTypes.Below)
                .ObjectTypeToFind(12)
                .QueryString("#215 = \"169010\"")
                .ConfigureSearchControls()
                    .AddRequestFilters(Filter.ReturnNoAttributes());

            var manualEvent = new ManualResetEvent(false);
            bool callbackCalled = false;

            //Act
            XElement result = null;

            _client.SendApiRequestAsync(builder.Build(), data => {
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
        [ExpectedException(typeof (ArgumentNullException),
            ExpectedMessage = "Value cannot be null.\r\nParameter name: request")]
        public void SendRequestAsync_Throws_ArgumentNullException_If_Request_Is_Null() {
            //Arrange
            var client = new ApiClient(new ApiWebClient());

            //Act
            client.SendApiRequestAsync<CreateRequest>(null, null);
        }

        [Test]
        public void Can_Supply_Response_Converter_And_Receive_Correct_Response_Type() {
            //Arrange
            var client = new ApiClient(new ApiWebClient());
            var request = new AqlQueryBuilder();

            request.BasePath("/Structures/Classification/JULA Produkter")
                .QueryType(AqlQueryTypes.Below)
                .ObjectTypeToFind(12)
                .QueryString("#215 = \"169010\"")
                .ConfigureSearchControls()
                .AddRequestFilters(
                    Filter.CountLimit(1),
                    Filter.ExcludeBin())
                .ReturnAttributes(
                    AttributeToReturn.WithDefinitionId(215),
                    AttributeToReturn.WithDefinitionId(24));

            var converter = new ContextResponseConverter();

            //Act
            var reply = client.SendApiRequest(request.Build(), converter.Convert);

            //Assert
            Assert.That(reply, Is.Not.Null);
            Assert.That(reply, Is.InstanceOf<IEnumerable<ContextResponse>>());
            Assert.That(reply.Count(), Is.EqualTo(1));
        }

        [Test]
        [ExpectedException(typeof(AdsmlException), ExpectedMessage = "The request failed:\nErrorType: ApplicationError, id: 4013, desc: ERROR_CODE_4013. Message: Structure type \"Classifiation\" does not exist.")]
        public void Should_Throw_AdsmlException_If_An_ErrorResponse_Is_Returned() {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            string xml = new XElement("BatchResponse",
                new XAttribute("version", "5.1.16 build 116 (2010/05/27 14-36)"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XElement("ErrorResponse",
                    new XAttribute("id", "4024"),
                    new XAttribute("type", "malformedRequest"),
                    new XElement("Message", "Error at line 1 column 111. cvc-elt.1: Cannot find the declaration of element 'BatchRequest'..")
                ),
                new XElement("ErrorResponse",
                    new XAttribute("description", "ERROR_CODE_4013"),
                    new XAttribute("id", "4013"),
                    new XAttribute("type", "applicationError"),
                    new XElement("Message", "Structure type \"Classifiation\" does not exist"))).ToString();


            var errorResponse = System.Text.Encoding.Default.GetBytes(xml);

            var mockWebClient = new Mock<IApiWebClient>();
            mockWebClient.Setup(m => m.UploadData(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(
                errorResponse);

            var client = new ApiClient(mockWebClient.Object);

            var request = new AqlQueryBuilder();

            request.BasePath("/Structures/Classification/JULA Produkter")
                .QueryType(AqlQueryTypes.Below)
                .ObjectTypeToFind(12)
                .QueryString("#215 = \"169010\"")
                .ConfigureSearchControls()
                .AddRequestFilters(
                    Filter.CountLimit(1),
                    Filter.ExcludeBin())
                .ReturnAttributes(
                    AttributeToReturn.WithDefinitionId(215),
                    AttributeToReturn.WithDefinitionId(24));

            //Act
            client.SendApiRequest(request.Build());
        }
    }
}