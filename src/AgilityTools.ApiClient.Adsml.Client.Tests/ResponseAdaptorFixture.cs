using System;
using System.Xml.Linq;
using NUnit.Framework;
using AgilityTools.ApiClient.Adsml.Client.Responses;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class ResponseAdaptorFixture
    {
        [Test]
        public void Can_Adapt_XElement_To_AdsmlResponse()
        {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var response = new XElement("BatchResponse",
                new XAttribute("version", "5.1.16 build 116 (2010/05/27 14-36)"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XElement("CreateResponse",
                    new XElement("StructureContext",
                        new XAttribute("idPath", "12:1516:145842:180789"),
                        new XAttribute("name", "/Structures/Classification/JULA Produkter/xx Import/Produktimport/000111 TESTPRODUKT"),
                        new XAttribute("sortPath", "0:14:2:1"))));

            //Act
            var adsmlResponse = response.ToBasicResponse();

            //Assert
            Assert.That(adsmlResponse, Is.Not.Null);
            Assert.That(adsmlResponse, Is.InstanceOf<AdsmlResponse>());
            Assert.That(adsmlResponse.ErrorMessage, Is.Null.Or.Empty);
        }

        [Test]
        public void Can_Adapt_XElement_To_CreateResponse()
        {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var response = new XElement("BatchResponse",
                new XAttribute("version", "5.1.16 build 116 (2010/05/27 14-36)"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XElement("CreateResponse",
                    new XElement("StructureContext",
                        new XAttribute("idPath", "12:1516:145842:180789"),
                        new XAttribute("name", "/Structures/Classification/JULA Produkter/xx Import/Produktimport/000111 TESTPRODUKT"),
                        new XAttribute("sortPath", "0:14:2:1"))));


            //Act
            var adsmlResponse = response.ToCreateResponse();

            //Assert
            Assert.That(adsmlResponse, Is.Not.Null);
            Assert.That(adsmlResponse, Is.InstanceOf<CreateResponse>());
            Assert.That(adsmlResponse.CreatedObjectId, Is.EqualTo(180789));
        }

        [Test]
        public void Parses_Error_Details_Correctly() {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var response = new XElement("BatchResponse",
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XElement("ErrorResponse",
                    new XAttribute("id", "4024"),
                    new XAttribute("type", "malformedRequest"),
                    new XElement("Message", "Error at line 2 column 63. cvc-elt.1: Cannot find the declaration of element 'BatchRequest'..")),
                new XElement("CreateResponse",
                    new XElement("StructureContext",
                        new XAttribute("idPath", "12:1516:137713:138413"),
                        new XAttribute("name", "/Structures/Classification/JULA Produkter/xx Import/Produktimport/000111 TESTPRODUKT"),
                        new XElement("SimpleAttribute",
                            new XAttribute("name", "objectTypeId"),
                            new XAttribute("type", "integer"),
                            new XElement("Value", "12")))));


            //Act
            var adsmlResponse = response.ToBasicResponse();

            //Assert
            Assert.That(adsmlResponse.ErrorId, Is.EqualTo(4024));
            Assert.That(adsmlResponse.ErrorType, Is.EqualTo("malformedRequest"));
            Assert.That(adsmlResponse.ErrorMessage,
                        Is.EqualTo("Error at line 2 column 63. cvc-elt.1: Cannot find the declaration of element 'BatchRequest'.."));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Not a valid Adsml response.")]
        public void Throws_InvalidOperationException_If_XElement_Is_Not_A_Adsml_Reponse() {
            //Arrange
            var response = new XElement("foo");

            //Act
            response.ToBasicResponse();
        }
    }
}