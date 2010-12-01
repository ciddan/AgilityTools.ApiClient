using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using AgilityTools.ApiClient.Adsml.Client.Filters;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class ModifyRequestFixture
    {
        [Test]
        public void Can_Instantiate_New_ModifyRequest() {
            //Act
            var modReq = new ModifyRequest("foo", new List<ModificationItem>());

            //Assert
            Assert.That(modReq, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: contextToModify")]
        public void Ctor_Throws_ArgumentNullException_When_Context_Is_Null() {
            //Act
            new ModifyRequest(null, new List<ModificationItem>());
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: modifications")]
        public void Ctor_Throws_ArgumentNullException_When_Modifications_Are_Null() {
            //Act
            new ModifyRequest("foo", null);
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "A context to modify must be specified.")]
        public void Validate_Throws_ApiSerializationValidationException_When_Context_Is_Empty() {
            //Arrange
            var modReq = new ModifyRequest(string.Empty, new List<ModificationItem> {
                                                           ModificationItem.New(Modifications.AddValue,
                                                                                StructureAttribute.New(31,
                                                                                   new StructureValue(10, "foo")))});

            //Act
            modReq.ToAdsml();

            //Assert 
            Assert.Fail("Expected exception not thrown.");
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "At least one ModificationItem must be specified.")]
        public void Validate_Throws_ApiSerializationValidationException_When_Modifications_Is_Empty() {
            //Arrange
            var modReq = new ModifyRequest("foo", new List<ModificationItem>());

            //Act
            modReq.ToAdsml();

            //Assert 
            Assert.Fail("Expected exception not thrown.");
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            string expected =
                new XElement("BatchRequest",
                    new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XElement("ModifyRequest",
                        new XAttribute("name", "/foo/bar"),
                        new XElement("ModificationItem",
                            new XAttribute("operation", "addValue"),
                            new XElement("AttributeDetails",
                                new XElement("StructureAttribute",
                                    new XAttribute("id", "31"),
                                    new XElement("StructureValue",
                                        new XAttribute("langId", "10"),
                                        new XAttribute("scope", "global"),
                                        new XCData("foo"))))))).ToString();


            var modReq = new ModifyRequest("/foo/bar", new List<ModificationItem> {
                                                           ModificationItem.New(Modifications.AddValue,
                                                                                StructureAttribute.New(31,
                                                                                   new StructureValue(10, "foo")))});

            //Act
            string actual = modReq.ToAdsml().ToString();

            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_LookupControls()
        {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            string expected =
                new XElement("BatchRequest",
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XElement("ModifyRequest",
                    new XAttribute("name", "/foo/bar"),
                    new XElement("LookupControls",
                        new XElement("AttributesToReturn",
                            new XElement("Attribute",
                                new XAttribute("id", "215"))),
                        new XElement("LanguagesToReturn",
                            new XElement("Language",
                                new XAttribute("id", "10")))),
                    new XElement("ModificationItem",
                        new XAttribute("operation", "addValue"),
                        new XElement("AttributeDetails",
                            new XElement("StructureAttribute",
                                new XAttribute("id", "31"),
                                new XElement("StructureValue",
                                    new XAttribute("langId", "10"),
                                    new XAttribute("scope", "global"),
                                    new XCData("foo"))))))).ToString();


            var lookupBuilder = new LookupControlBuilder();

            lookupBuilder.ReturnAttributes(AttributeToReturn.WithDefinitionId(215))
                         .ReturnLanguages(LanguageToReturn.WithLanguageId(10));

            var modReq = new ModifyRequest("/foo/bar", new List<ModificationItem> 
                                                       {
                                                           ModificationItem.New(Modifications.AddValue,
                                                                                StructureAttribute.New(31,
                                                                                   new StructureValue(10, "foo")))
                                                       })
                         {
                             LookupControl = lookupBuilder.Build()
                         };

            //Act
            string actual = modReq.ToAdsml().ToString();

            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_RequestFilters()
        {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            string expected =
                new XElement("BatchRequest",
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XElement("ModifyRequest",
                    new XAttribute("name", "/foo/bar"),
                    new XAttribute("returnNoAttributes", "true"),
                    new XAttribute("failOnError", "true"),
                    new XElement("ModificationItem",
                        new XAttribute("operation", "addValue"),
                        new XElement("AttributeDetails",
                            new XElement("StructureAttribute",
                                new XAttribute("id", "31"),
                                new XElement("StructureValue",
                                    new XAttribute("langId", "10"),
                                    new XAttribute("scope", "global"),
                                    new XCData("foo"))))))).ToString();

            var modReq = new ModifyRequest("/foo/bar", new List<ModificationItem> 
                                                       {
                                                           ModificationItem.New(Modifications.AddValue,
                                                                                StructureAttribute.New(31,
                                                                                   new StructureValue(10, "foo")))
                                                       })
            {
                RequestFilters = new List<IModifyRequestFilter>
                                 {
                                     Filter.ReturnNoAttributes(),
                                     Filter.FailOnError()
                                 }
            };

            //Act
            string actual = modReq.ToAdsml().ToString();

            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}