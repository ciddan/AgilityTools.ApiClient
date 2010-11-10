using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Helpers;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class AqlSearchRequestFixture
    {
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

            var aql = new AqlSearchRequest("/Structures/Classification/JULA Produkter", 
                                           QueryTypes.Below,
                                           new IdNameReference(10), 
                                           "#10 = \"foo\"", 
                                           null);

            //Act
            var actual = aql.ToAdsml();

            Console.WriteLine(actual.ToString());

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_Omission_Of_Attributes() {
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

            var aql = new AqlSearchRequest("/foo/bar",
                                           QueryTypes.Below,
                                           new IdNameReference(10),
                                           "#10 = \"foo\"",
                                           null) 
                                           {OmitStructureAttributes = true};

            //Act
            var actual = aql.ToAdsml();

            Console.WriteLine(actual.ToString());

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_SearchControl_Filters() {
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

            var builder = new SearchControlBuilder();
            builder.AddRequestFilters(Filter.ExcludeBin(), Filter.ExcludeDocument());

            var searchControls = builder.Build();

            var aql = new AqlSearchRequest("/foo/bar",
                               QueryTypes.Below,
                               new IdNameReference(10),
                               "#10 = \"foo\"",
                               searchControls) { OmitStructureAttributes = true };

            //Act
            var actual = aql.ToAdsml();

            Console.WriteLine(actual.ToString());

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_SearchControl_Filters_And_AttributesToReturn() {
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

            var builder = new SearchControlBuilder();

            builder.AddRequestFilters(Filter.ExcludeBin(), 
                                   Filter.ExcludeDocument())
                   .ReturnAttributes(AttributeToReturn.WithDefinitionId(10));

            var searchControls = builder.Build();

            var aql = new AqlSearchRequest("/foo/bar",
                   QueryTypes.Below,
                   new IdNameReference(10),
                   "#10 = \"foo\"",
                   searchControls) { OmitStructureAttributes = true };

            //Act
            var actual = aql.ToAdsml();

            Console.WriteLine(actual.ToString());

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_Api_Xml_With_SearchControl_ReferenceControls() {
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
                            new XElement("ReferenceControls",
                                new XAttribute("channelId", "3"),
                                new XAttribute("resolveAttributes", "true"),
                                new XAttribute("resolveSpecialCharacters", "true"),
                                new XAttribute("valueOnly", "true"))),
                        new XElement("Filter",
                            new XElement("FilterString", "FIND BELOW #10 WHERE (#10 = \"foo\")"))));

            var builder = new SearchControlBuilder();

            builder.ConfigureReferenceHandling(ReferenceOptions.UseChannel(3),
                                               ReferenceOptions.ResolveAttributes(),
                                               ReferenceOptions.ResolveSpecialCharacters(),
                                               ReferenceOptions.ReturnValuesOnly());

            var searchControls = builder.Build();

            var aql = new AqlSearchRequest("/foo/bar",
                                           QueryTypes.Below,
                                           new IdNameReference(10),
                                           "#10 = \"foo\"",
                                           searchControls) {OmitStructureAttributes = true};

            //Act
            var actual = aql.ToAdsml();

            Console.WriteLine(actual.ToString());

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }
    }
}