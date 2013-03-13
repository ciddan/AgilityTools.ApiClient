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
            _client = new ApiClient(new ApiWebClient(), "http://192.168.32.85:9080/Agility/Directory", "admin", "qcteam");
        }

        [TearDown]
        public void Cleanup() {
            _client.Dispose();
        }

        [Test]
        public void Can_Instantiate_New_ApiClient() {
            //Act
            IApiClient client = new ApiClient(new ApiWebClient(), "http://192.168.32.85:9080/Agility/Directory", "admin", "qcteam");

            //Assert
            Assert.That(client, Is.Not.Null);
        }

        [Test]
        public void ApiClient_Ctor_Throws_ArgumentNullException_If_Any_Required_Parameter_Is_Null() {
            //Assert
            Assert.Throws<ArgumentNullException>(() => new ApiClient(null, "a", "a", "a"));
            Assert.Throws<ArgumentNullException>(() => new ApiClient(new ApiWebClient(), null, "a", "a"));
            Assert.Throws<ArgumentNullException>(() => new ApiClient(new ApiWebClient(), "a", null, "a"));
            Assert.Throws<ArgumentNullException>(() => new ApiClient(new ApiWebClient(), "a", "a", null));
        }

        [Test]
        public void Can_Dispose_Api_Client() {
            //Arrange
            var mockWebClient = new Mock<IApiWebClient>();
            mockWebClient.Setup(s => s.Dispose()).Verifiable();

            IApiClient client = new ApiClient(mockWebClient.Object, "foo", "bar", "baz");

            //Act
            client.Dispose();

            //Assert
            mockWebClient.Verify(s => s.Dispose(), Times.Once());
        }

        [Test]
        public void Can_Send_ApiRequests_Via_ApiClient() {
            //Arrange
            var builder = new AqlQueryBuilder();

            builder.BasePath("/Structures/Classification/JULA Produkter/")
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
        [ExpectedException(typeof (ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: request")]
        public void SendRequestAsync_Throws_ArgumentNullException_If_Request_Is_Null() {
            //Arrange
            var client = new ApiClient(new ApiWebClient(), "http://192.168.32.85:9080/Agility/Directory", "admin", "qcteam");

            //Act
            client.SendApiRequestAsync<CreateRequest>(null, null);
        }

        [Test]
        public void Can_Supply_Response_Converter_And_Receive_Correct_Response_Type() {
            //Arrange
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
                    AttributeToReturn.WithName("Artikelnummer"),
                    AttributeToReturn.WithName("2_Rubrik"));

            var converter = new ContextResponseConverter("adsml.xsd");

            //Act
            var reply = _client.SendApiRequest(request.Build(), converter.Convert);

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
                    new XAttribute("description", "ERROR_CODE_4013"),
                    new XAttribute("id", "4013"),
                    new XAttribute("type", "applicationError"),
                    new XElement("Message", "Structure type \"Classifiation\" does not exist"))).ToString();

            var mockWebClient = new Mock<IApiWebClient>();
            mockWebClient.Setup(m => m.UploadString(It.IsAny<string>(), It.IsAny<string>())).Returns(xml);

            var client = new ApiClient(mockWebClient.Object, "f", "b", "q");

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
                    AttributeToReturn.WithName("Artikelnummer"),
                    AttributeToReturn.WithName("2_Rubrik"));

            //Act
            client.SendApiRequest(request.Build());
        }

        [Test]
        public void Should_Not_Throw_Exception_If_Query_Returns_No_Results() {
            //Arrange
            AqlQueryBuilder builder = new AqlQueryBuilder();
            builder.BasePath("/Structures/Classification/JULA Produkter")
                   .QueryType(AqlQueryTypes.Below)
                   .ObjectTypeToFind(12)
                   .QueryString("#215 = \"000\"");

            var query = builder.Build();

            //Act
            _client.SendApiRequest(query);
        }
    }
}