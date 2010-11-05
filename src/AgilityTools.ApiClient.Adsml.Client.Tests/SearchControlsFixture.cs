using System;
using System.Collections.Generic;
using System.Xml.Linq;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class SearchControlsFixture
    {
        [Test]
        public void Can_Instantiate_New_SearchControls() {
            //Act
            var sc = new SearchControl(null, null);

            //Assert
            Assert.That(sc, Is.Not.Null);
        }

        [Test]
        public void Can_Instantiate_New_SearchControls_And_Supply_SearchControls() {
            //Act
            var sc = new SearchControl(null, new List<ISearchControlComponent>());

            //Assert
            Assert.That(sc, Is.Not.Null);
        }

        [Test]
        public void Can_Generate_Api_Xml_With_AttributesToReturn() {
            //Arrange
            var expected =
                new XElement("SearchControls",
                    new XElement("AttributesToReturn",
                        new XElement("Attribute", new XAttribute("id", "20"))));

            var builder = new SearchControlBuilder();
            builder.ReturnedAttributes(AttributeToReturn.WithDefinitionId(20));

            var controls = builder.Build();

            //Act
            var actual = controls.ToApiXml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));

            Console.WriteLine(actual.ToString());
        }

        [Test]
        public void Can_Generate_Api_Xml_With_AttributesToReturn_And_Exclusion_Filters() {
            //Arrange
            var expected =
                new XElement("SearchControls",
                    new XAttribute("excludeBin", "true"),
                    new XAttribute("excludeDocument", "true"),
                    new XElement("AttributesToReturn",
                        new XElement("Attribute", new XAttribute("id", "20"))));

            var builder = new SearchControlBuilder();

            builder.RequestFilters(Filter.ExcludeBin(),
                                   Filter.ExcludeDocument())
                .ReturnedAttributes(AttributeToReturn.WithDefinitionId(20));

            var controls = builder.Build();

            //Act
            var actual = controls.ToApiXml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));

            Console.WriteLine(actual.ToString());
        }
    }
}