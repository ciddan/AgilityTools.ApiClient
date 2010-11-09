using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class AqlSearchRequestFixture
    {
        [Test]
        public void AqlSearchRequest_Ctor_Has_Overload_For_DefinitionId_SearchTerm_TypeId() {
            //Act
            var aql = new AqlSearchRequest(10, 10, "foo");

            //Assert
            Assert.That(aql, Is.Not.Null);
            Assert.That(aql.DefinitionIdToMatch, Is.EqualTo(10));
            Assert.That(aql.ObjectTypeId, Is.EqualTo(10));
            Assert.That(aql.SearchTerm, Is.EqualTo("foo"));
            Assert.That(aql.BasePath, Is.EqualTo("/Structures/Classification/JULA Produkter"));
        }

        [Test]
        public void AqlSearchRequest_Ctor_Has_Overload_For_AttributeName_SearchTerm_TypeId()
        {
            //Act
            var aql = new AqlSearchRequest(10, "bar", "foo");

            //Assert
            Assert.That(aql, Is.Not.Null);
            Assert.That(aql.AttributeName, Is.EqualTo("bar"));
            Assert.That(aql.ObjectTypeId, Is.EqualTo(10));
            Assert.That(aql.SearchTerm, Is.EqualTo("foo"));
            Assert.That(aql.BasePath, Is.EqualTo("/Structures/Classification/JULA Produkter"));
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "SearchTerm cannot be null or empty.")]
        public void AqlSearchRequest_Validate_Throws_ASVE_If_SearchTerm_Is_NullOrEmpty() {

            //Arrange
            var aql = new AqlSearchRequest(10, 10, null);

            //Act
            aql.Validate();
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "AttributeName cannot be null or empty.")]
        public void AqlSearchRequest_Validate_Throws_ASVE_If_AttributeName_Is_NullOrEmpty() {
            //Arrange
            var aql = new AqlSearchRequest(10, string.Empty, "foo");

            //Act
            aql.Validate();
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "DefinitionIdToMatch must be set.")]
        public void AqlSearchRequest_Validate_Throws_ASVE_If_DefinitionId_Is_Not_Set() {
            //Arrange
            var aql = new AqlSearchRequest(10, 0, "foo");

            //Act
            aql.Validate();
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "ObjectTypeId must be set.")]
        public void AqlSearchRequest_Validate_Throws_ASVE_If_ObjectTypeId_Is_Not_Set() {
            //Arrange
            var aql = new AqlSearchRequest(0, 10, "foo");

            //Act
            aql.Validate();
        }

        [Test]
        public void Can_Generate_Api_Xml_With_DefinitionId() {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var expected = new XElement("BatchRequest",
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XElement("SearchRequest",
                    new XAttribute("base", "/Structures/Classification/JULA Produkter"),
                    new XElement("Filter",
                        new XElement("FilterString", "FIND BELOW #10 WHERE (#10 = \"foo\")")
                    )
                )
            );

            var aql = new AqlSearchRequest(10, 10, "foo");

            //Act
            var actual = aql.ToApiXml();

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_AttributeName()
        {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var expected = new XElement("BatchRequest",
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XElement("SearchRequest",
                    new XAttribute("base", "/Structures/Classification/JULA Produkter"),
                    new XElement("Filter",
                        new XElement("FilterString", "FIND BELOW #10 WHERE (\"bar\" = \"foo\")")
                    )
                )
            );

            var aql = new AqlSearchRequest(10, "bar", "foo");

            //Act
            var actual = aql.ToApiXml();

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_Specific_BasePath()
        {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var expected = new XElement("BatchRequest",
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XElement("SearchRequest",
                    new XAttribute("base", "/foo/bar"),
                    new XElement("Filter",
                        new XElement("FilterString", "FIND BELOW #10 WHERE (#10 = \"foo\")")
                    )
                )
            );

            var aql = new AqlSearchRequest(10, 10, "foo", null, "/foo/bar");

            //Act
            var actual = aql.ToApiXml();

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_Omission_Of_Attributes()
        {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var expected = new XElement("BatchRequest",
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XElement("SearchRequest",
                    new XAttribute("base", "/foo/bar"),
                    new XAttribute("returnNoAttributes", "true"),
                    new XElement("Filter",
                        new XElement("FilterString", "FIND BELOW #10 WHERE (#10 = \"foo\")")
                    )
                )
            );

            var aql = new AqlSearchRequest(10, 10, "foo", null, "/foo/bar") { OmitStructureAttributes = true };

            //Act
            var actual = aql.ToApiXml();

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));

            Console.WriteLine(actual.ToString());
        }

        [Test]
        public void Can_Generate_Api_Xml_With_SearchControl_Filters()
        {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var expected = new XElement("BatchRequest",
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XElement("SearchRequest",
                    new XAttribute("base", "/foo/bar"),
                    new XAttribute("returnNoAttributes", "true"),
                    new XElement("SearchControls", new XAttribute("excludeBin", "true"), new XAttribute("excludeDocument", "true")),
                    new XElement("Filter",
                        new XElement("FilterString", "FIND BELOW #10 WHERE (#10 = \"foo\")")
                    )
                )
            );

            var searchControls = new SearchControls {ExcludeResultsInBin = true, ExcludeResultsInDocumentFolder = true};

            var aql = new AqlSearchRequest(10, 10, "foo", searchControls, "/foo/bar") { OmitStructureAttributes = true };

            //Act
            var actual = aql.ToApiXml();

            Console.WriteLine(actual.ToString());

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_SearchControl_Filters_And_Components()
        {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var expected =
                new XElement("BatchRequest",
                    new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XElement("SearchRequest",
                        new XAttribute("base", "/foo/bar"),
                        new XAttribute("returnNoAttributes", "true"),
                        new XElement("SearchControls",
                            new XAttribute("excludeBin", "true"),
                            new XAttribute("excludeDocument", "true"),
                            new XElement("AttributesToReturn",
                                new XElement("Attribute",
                                    new XAttribute("id", "10")))),
                        new XElement("Filter",
                            new XElement("FilterString", "FIND BELOW #10 WHERE (#10 = \"foo\")"))));


            var searchControls =
                new SearchControls(new AttributeSearchControls(new AttributeToReturn {DefinitionId = 10}))
                {
                    ExcludeResultsInBin = true,
                    ExcludeResultsInDocumentFolder = true
                };

            var aql = new AqlSearchRequest(10, 10, "foo", searchControls, "/foo/bar") { OmitStructureAttributes = true };

            //Act
            var actual = aql.ToApiXml();

            Console.WriteLine(actual.ToString());

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }
    }
}