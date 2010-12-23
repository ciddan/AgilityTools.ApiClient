using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Responses;
using AgilityTools.ApiClient.Adsml.Client.Responses.Converters;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Responses.Converters
{
    [TestFixture]
    public class DeleteResponseConverterFixture
    {
        [Test]
        public void Can_Instantiate_New_DeleteResponseConverter() {
            //Act
            var drc = new DeleteResultResponseConverter();

            //Assert
            Assert.That(drc, Is.Not.Null);
            Assert.That(drc, Is.InstanceOf<IResponseConverter<XElement, DeleteResponse>>());
        }

        [Test]
        public void Can_Convert_XElement_To_DeleteResponse() {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var response = new XElement("BatchResponse",
                new XAttribute("version", "5.1.16 build 116 (2010/05/27 14-36)"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XElement("DeleteResponse",
                    new XAttribute("code", "0"),
                    new XAttribute("description", "Success"),
                    new XElement("Message", "foo")));

            var drc = new DeleteResultResponseConverter();

            //Act
            var converted = drc.ConvertSingle(response);

            //Assert
            Assert.That(converted.Code, Is.EqualTo("0"));
            Assert.That(converted.Description, Is.EqualTo("Success"));
            Assert.That(converted.Messages.Count(), Is.EqualTo(1));
            Assert.That(converted.Messages.ElementAt(0), Is.EqualTo("foo"));
        }

        [Test]
        [ExpectedException(typeof (System.ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: source")]
        public void Throws_ArgumentNullException_If_Source_Is_Null() {
            //Arrange
            var drc = new DeleteResultResponseConverter();

            //Act
            drc.ConvertSingle(null);
        }

        [Test]
        [ExpectedException(typeof(System.InvalidOperationException), ExpectedMessage = "Not a valid DeleteResponse.")]
        public void Throws_InvalidOperationException_If_Response_Is_Not_DeleteResponse() {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var response = new XElement("BatchResponse",
                new XAttribute("version", "5.1.16 build 116 (2010/05/27 14-36)"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XElement("AuthResponse",
                    new XAttribute("code", "0"),
                    new XAttribute("description", "Success"),
                    new XElement("Message", "foo")));

            var drc = new DeleteResultResponseConverter();

            //Act
            drc.ConvertSingle(response);
        }

        [Test]
        [ExpectedException(typeof(System.InvalidOperationException), ExpectedMessage = "Not a valid Adsml response.")]
        public void Throws_InvalidOperationException_If_Response_Is_Not_Valid()
        {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var response = new XElement("BatchResponse",
                new XAttribute("version", "5.1.16 build 116 (2010/05/27 14-36)"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XElement("FooResponse",
                    new XAttribute("code", "0"),
                    new XAttribute("description", "Success"),
                    new XElement("Message", "foo")));

            var drc = new DeleteResultResponseConverter();

            //Act
            drc.ConvertSingle(response);
        }
    }
}