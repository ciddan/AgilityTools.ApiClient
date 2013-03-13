using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Requests.Builders
{
    [TestFixture]
    public class SearchControlBuilderFixture
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
                        new XElement("Attribute", new XAttribute("name", "Bar"))));

            var builder = new SearchControlBuilder();
            builder.ReturnAttributes(AttributeToReturn.WithName("Bar"));

            var controls = builder.Build();

            //Act
            var actual = controls.ToAdsml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));

            Console.WriteLine(actual.ToString());
        }

        [Test]
        public void Can_Generate_Api_Xml_With_LanguagesToReturn() {
            //Arrange
            var expected =
                new XElement("SearchControls",
                    new XElement("LanguagesToReturn",
                        new XElement("Language", new XAttribute("id", "10"))));

            var builder = new SearchControlBuilder();
            builder.ReturnLanguages(LanguageToReturn.WithLanguageId(10));

            var controls = builder.Build();

            //Act
            var actual = controls.ToAdsml();

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
                        new XElement("Attribute", new XAttribute("id", "Bar"))));

            var builder = new SearchControlBuilder();

            builder.AddRequestFilters(Filter.ExcludeBin(),
                                   Filter.ExcludeDocument())
                .ReturnAttributes(AttributeToReturn.WithName("Bar"));

            var controls = builder.Build();

            //Act
            var actual = controls.ToAdsml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));

            Console.WriteLine(actual.ToString());
        }

        [Test]
        public void Can_Generate_Api_Xml_With_ReferenceControls() {
            //Arrange
            var expected =
                new XElement("SearchControls",
                 new XElement("ReferenceControls",
                    new XAttribute("channelId", "3"),
                    new XAttribute("resolveAttributes", "true"),
                    new XAttribute("resolveSpecialCharacters", "true"),
                    new XAttribute("valueOnly", "true")));

            var builder = new SearchControlBuilder();

            builder.ConfigureReferenceHandling(ReferenceOptions.UseChannel(3),
                                               ReferenceOptions.ResolveAttributes(),
                                               ReferenceOptions.ResolveSpecialCharacters(),
                                               ReferenceOptions.ReturnValuesOnly());

            var controls = builder.Build();

            //Act
            var actual = controls.ToAdsml();

            //Assert
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));

            Console.WriteLine(actual.ToString());
        }

        [Test]
        public void Can_Generate_Api_Xml_With_All_Controls() {
            //Arrange

            var expected =
                new XElement("SearchControls",
                             new XAttribute("excludeBin", "true"),
                             new XAttribute("excludeDocument", "true"),
                             new XAttribute("returnNoAttributes", "true"),
                             new XAttribute("allowPaging", "true"),
                             new XAttribute("pageSize", "2"),
                             new XAttribute("countLimit", "5"),
                             new XElement("AttributesToReturn",
                                          new XElement("Attribute", new XAttribute("name", "Artikelnummer")),
                                          new XElement("Attribute", new XAttribute("name", "Sammanslagna dataattribut"))),
                             new XElement("LanguagesToReturn",
                                          new XElement("Language", new XAttribute("id", "10")),
                                          new XElement("Language", new XAttribute("id", "11"))),
                             new XElement("ReferenceControls",
                                          new XAttribute("channelId", "3"),
                                          new XAttribute("resolveAttributes", "true"),
                                          new XAttribute("resolveSpecialCharacters", "true"),
                                          new XAttribute("valueOnly", "true"))).ToString();

            var builder = new SearchControlBuilder();

            builder
                .AddRequestFilters(
                    Filter.ExcludeBin(),
                    Filter.ExcludeDocument(),
                    Filter.ReturnNoAttributes(),
                    Filter.AllowPaging(),
                    Filter.PageSize(2),
                    Filter.CountLimit(5))
                .ReturnAttributes(
                    AttributeToReturn.WithName("Artikelnummer"),
                    AttributeToReturn.WithName("Sammanslagna dataattribut")
                )
                .ReturnLanguages(
                    LanguageToReturn.WithLanguageId(10),
                    LanguageToReturn.WithLanguageId(11)
                )
                .ConfigureReferenceHandling(
                    ReferenceOptions.UseChannel(3),
                    ReferenceOptions.ResolveAttributes(),
                    ReferenceOptions.ResolveSpecialCharacters(),
                    ReferenceOptions.ReturnValuesOnly());

            var searchControl = builder.Build();
            //Act
            var actual = searchControl.ToAdsml().ToString();

            //Assert
            Assert.That(actual, Is.EqualTo(expected));

            Console.WriteLine(actual);
        }
    }
}