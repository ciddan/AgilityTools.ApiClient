using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Requests
{
    [TestFixture]
    public class LinkRequestFixture
    {
        [Test]
        public void Can_Instantiate_New_LinkRequest() {
            //Act
            var linkRequest = new LinkRequest("source", "target");

            //Assert
            Assert.That(linkRequest, Is.Not.Null);
            Assert.That(linkRequest.SourcePath, Is.EqualTo("source"));
            Assert.That(linkRequest.TargetPath, Is.EqualTo("target"));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: source")]
        public void Ctor_Throws_ArgumentNullException_If_Source_Is_Null() {
            //Act
            new LinkRequest(null, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: target")]
        public void Ctor_Throws_ArgumentNullException_If_Target_Is_Null() {
            //Act
            new LinkRequest("foo", null);
        }

        [Test]
        public void Can_Add_RequestFilter() {
            //Arrange
            var request = new LinkRequest("foo", "bar");

            //Act
            request.RequestFilters.Add(Filter.ReturnNoAttributes());

            //Assert
            Assert.That(request.RequestFilters.Count(), Is.EqualTo(1));
            Assert.That(request.RequestFilters.ElementAt(0), Is.InstanceOf<ReturnNoAttributesFilter>());
        }

        [Test]
        public void Can_Add_CopyControls() {
            //Act
            var request = new LinkRequest("foo", "bar")
                          {
                              CopyControl = new CopyControl(null)
                          };

            //Assert
            Assert.That(request.CopyControl, Is.Not.Null);
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            string expected = new XElement("BatchRequest",
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XElement("LinkRequest",
                    new XAttribute("name", "foo"),
                    new XAttribute("targetLocation", "bar"))).ToString();

            var request = new LinkRequest("foo", "bar");

            //Act
            string actual = request.ToAdsml().ToString();

            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_RequestFilter() {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            string expected = new XElement("BatchRequest",
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XElement("LinkRequest",
                    new XAttribute("name", "foo"),
                    new XAttribute("targetLocation", "bar"),
                    new XAttribute("returnNoAttributes", "true"))).ToString();

            var request = new LinkRequest("foo", "bar")
                          {
                              RequestFilters = new List<ILinkRequestFilter> {Filter.ReturnNoAttributes()}
                          };

            //Act
            string actual = request.ToAdsml().ToString();

            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_CopyControl() {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            string expected = new XElement("BatchRequest",
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XElement("LinkRequest",
                    new XAttribute("name", "foo"),
                    new XAttribute("targetLocation", "bar"),
                    new XElement("CopyControls", 
                        new XAttribute("copyLocalAttributes", "true")))).ToString();

            var request = new LinkRequest("foo", "bar")
                          {
                              CopyControl = new CopyControl(new List<ICopyControlFilter>{Filter.CopyLocalAttributesFromSource()})
                          };

            //Act
            string actual = request.ToAdsml().ToString();

            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_CopyControl_Containing_LookupControl()
        {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            string expected = new XElement("BatchRequest",
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XElement("LinkRequest",
                    new XAttribute("name", "foo"),
                    new XAttribute("targetLocation", "bar"),
                        new XElement("CopyControls",
                            new XAttribute("copyLocalAttributes", "true"),
                            new XElement("LookupControls", 
                                new XElement("AttributesToReturn", 
                                    new XElement("Attribute", 
                                    new XAttribute("id", "215"))))))).ToString();

            var lookupBuilder = new LookupControlBuilder();
            lookupBuilder.ReturnAttributes(AttributeToReturn.WithDefinitionId(215));

            var request = new LinkRequest("foo", "bar")
                          {
                              CopyControl =
                                  new CopyControl(new List<ICopyControlFilter>
                                                  {
                                                      Filter.CopyLocalAttributesFromSource()
                                                  },
                                  lookupBuilder.Build())
                          };

            //Act
            string actual = request.ToAdsml().ToString();

            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "A source path must be provided.")]
        public void Validate_Throws_ApiSerializationValidationException_If_Source_Is_Empty() {
            //Arrange
            var req = new LinkRequest(string.Empty, "foo");

            //Act
            req.ToAdsml();
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "A target path must be provided.")]
        public void Validate_Throws_ApiSerializationValidationException_If_Target_Is_Empty() {
            //Arrange
            var req = new LinkRequest("foo", string.Empty);

            //Act
            req.ToAdsml();
        }
    }
}