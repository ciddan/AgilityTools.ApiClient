using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    public class LookupControlBuilderFixture
    {
        [Test]
        public void Can_Instantiate_New_LookupControlBuilder() {
            //Act
            var builder = new LookupControlBuilder();

            //Assert
            Assert.That(builder, Is.Not.Null);
        }

        [Test]
        public void Can_Add_Request_Filters() {
            //Arrange
            var expected = new XElement("LookupControls", new XAttribute("returnRelationsAsAttributes", "true"));
            var builder = new LookupControlBuilder();

            //Act
            builder.AddRequestFilters(Filter.ReturnRelationsAsAttributes());

            var request = builder.Build().ToAdsml();

            //Assert
            Assert.That(request.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Specify_AttributeNames_To_Return() {
            //Arrange
            string expected = new XElement("LookupControls",
                            new XElement("AttributesToReturn",
                                new XAttribute("namelist", "members"))).ToString();
            var builder = new LookupControlBuilder();

            //Act
            builder.AttributeNamelist("members");
            var actual = builder.Build().ToAdsml().ToString();

            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Can_Specify_AttributeNamelist_And_AttributesToReturn()
        {
            //Arrange
            string expected = new XElement("LookupControls",
                                new XElement("AttributesToReturn",
                                    new XAttribute("namelist", "members"),
                                    new XElement("Attribute",
                                        new XAttribute("id", "35")))).ToString();

            var builder = new LookupControlBuilder();

            //Act
            builder.AttributeNamelist("members").ReturnAttributes(AttributeToReturn.WithDefinitionId(35));
            var actual = builder.Build().ToAdsml().ToString();

            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Can_Restrict_Returned_Attributes() {
            //Arrange
            var expected = new XElement("LookupControls", 
                           new XAttribute("returnRelationsAsAttributes", "true"),
                             new XElement("AttributesToReturn",
                                new XElement("Attribute", new XAttribute("id", "215"))));

            var builder = new LookupControlBuilder();

            //Act
            builder.AddRequestFilters(Filter.ReturnRelationsAsAttributes())
                   .ReturnAttributes(AttributeToReturn.WithDefinitionId(215));

            var request = builder.Build().ToAdsml();

            //Assert
            Assert.That(request.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Restrict_Returned_Languages()
        {
            //Arrange
            var expected = new XElement("LookupControls",
                           new XAttribute("returnRelationsAsAttributes", "true"),
                             new XElement("AttributesToReturn",
                                new XElement("Attribute", new XAttribute("id", "215"))),
                             new XElement("LanguagesToReturn",
                                new XElement("Language", new XAttribute("id", "10"))));

            var builder = new LookupControlBuilder();

            //Act
            builder.AddRequestFilters(Filter.ReturnRelationsAsAttributes())
                   .ReturnAttributes(AttributeToReturn.WithDefinitionId(215))
                   .ReturnLanguages(LanguageToReturn.WithLanguageId(10));

            var request = builder.Build().ToAdsml();

            //Assert
            Assert.That(request.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Configure_ReferenceControls()
        {
            //Arrange
            var expected = new XElement("LookupControls",
                           new XAttribute("returnRelationsAsAttributes", "true"),
                             new XElement("AttributesToReturn",
                                new XElement("Attribute", new XAttribute("id", "215"))),
                             new XElement("LanguagesToReturn",
                                new XElement("Language", new XAttribute("id", "10"))),
                             new XElement("ReferenceControls", new XAttribute("valueOnly", "true")));

            var builder = new LookupControlBuilder();

            //Act
            builder.AddRequestFilters(Filter.ReturnRelationsAsAttributes())
                   .ReturnAttributes(AttributeToReturn.WithDefinitionId(215))
                   .ReturnLanguages(LanguageToReturn.WithLanguageId(10))
                   .ConfigureReferenceHandling(ReferenceOptions.ReturnValuesOnly());

            var request = builder.Build().ToAdsml();

            //Assert
            Assert.That(request.ToString(), Is.EqualTo(expected.ToString()));
        }
    }
}