using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Responses;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class UnlinkResponseConverterFixture
    {
        [Test]
        public void Can_Instantiate_New_UnlinkResponseConverter()
        {
            //Act
            var urc = new UnlinkResponseConverter();

            //Assert
            Assert.That(urc, Is.Not.Null);
            Assert.That(urc, Is.InstanceOf<IResponseConverter<XElement, UnlinkResponse>>());
        }

        [Test]
        public void Can_Convert_XElement_To_UnlinkResponse()
        {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var response = new XElement("BatchResponse",
                new XAttribute("version", "5.1.16 build 116 (2010/05/27 14-36)"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XElement("UnlinkResponse",
                    new XAttribute("code", "0"),
                    new XAttribute("description", "Success"),
                    new XElement("Message", "foo")));

            var urc = new UnlinkResponseConverter();

            //Act
            var converted = urc.Convert(response);

            //Assert
            Assert.That(converted.Code, Is.EqualTo("0"));
            Assert.That(converted.Description, Is.EqualTo("Success"));
            Assert.That(converted.Messages.Count(), Is.EqualTo(1));
            Assert.That(converted.Messages.ElementAt(0), Is.EqualTo("foo"));
        }

        [Test]
        [ExpectedException(typeof(System.ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: source")]
        public void Throws_ArgumentNullException_If_Source_Is_Null()
        {
            //Arrange
            var urc = new UnlinkResponseConverter();

            //Act
            urc.Convert(null);
        }

        [Test]
        [ExpectedException(typeof(System.InvalidOperationException), ExpectedMessage = "Not a valid UnlinkResponse.")]
        public void Throws_InvalidOperationException_If_Response_Is_Not_UnlinkResponse()
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

            var urc = new UnlinkResponseConverter();

            //Act
            urc.Convert(response);
        }
    }
}