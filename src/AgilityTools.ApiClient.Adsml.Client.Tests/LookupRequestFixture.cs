using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class LookupRequestFixture
    {
        [Test]
        public void Can_Instatiate_New_LookupRequest() {
            //Act
            var request = new LookupRequest("foo");

            //Assert
            Assert.That(request, Is.Not.Null);
        }

        [Test]
        public void Can_Instatiate_New_LookupRequest_With_LookupControls() {
            //Act
            var request = new LookupRequest("foo", new LookupControl(null, null));

            //Assert
            Assert.That(request, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "name cannot be null or empty.")]
        public void Ctor_Throws_InvalidOperationException_If_Name_Is_Empty() {
            //Act
            new LookupRequest("");
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "name cannot be null or empty.")]
        public void Ctor_Throws_InvalidOperationException_If_Name_Is_Null() {
            //Act
            new LookupRequest(null);
        }

        [Test]
        public void Can_Generate_Basic_Api_Xml() {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            string expected = new XElement("BatchRequest",
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XElement("LookupRequest",
                    new XAttribute("name", "/foo/bar"))).ToString();


            var req = new LookupRequest("/foo/bar");

            //Act
            string actual = req.ToAdsml().ToString();

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_LookupControl() {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            string expexcted = new XElement("BatchRequest",
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XElement("LookupRequest",
                    new XAttribute("name", "/Schema/Attribute Sets/Översättningsattribut"),
                    new XElement("LookupControls",
                        new XElement("AttributesToReturn",
                            new XAttribute("namelist", "members")),
                        new XElement("LanguagesToReturn",
                            new XElement("Language",
                                new XAttribute("id", "10")))))).ToString();

            var lookupBuilder = new LookupControlBuilder();

            lookupBuilder.AttributeNamelist("members").ReturnLanguages(LanguageToReturn.WithLanguageId(10));

            var req = new LookupRequest("/Schema/Attribute Sets/Översättningsattribut", lookupBuilder.Build());

            //Act
            string actual = req.ToAdsml().ToString();
            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expexcted));
        }
    }
}