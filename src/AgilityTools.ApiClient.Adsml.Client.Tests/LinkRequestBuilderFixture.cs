using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Filters;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class LinkRequestBuilderFixture
    {
        private LinkRequestBuilder _builder;

        [SetUp]
        public void Setup() {
            _builder = new LinkRequestBuilder();
        }

        [Test]
        public void Can_Instantiate_New_LinkRequestBuilder() {
            //Act
            var builder = new LinkRequestBuilder();

            //Assert
            Assert.That(builder, Is.Not.Null);
        }

        [Test]
        public void Can_Specify_SourceContext() {
            //Act
            _builder.SourceContext("/foo");

            //Assert
            Assert.That(_builder.Source, Is.EqualTo("/foo"));
        }

        [Test]
        public void Can_Specify_TargetContext() {
            //Act
            _builder.TargetPath("/bar");

            //Assert
            Assert.That(_builder.Target, Is.EqualTo("/bar"));
        }

        [Test]
        public void Can_Add_OmitStructureAttributes_RequestFilter() {
            //Act
            _builder.ReturnNoAttributes();

            //Assert
            Assert.That(_builder.RequestFilters.Count(), Is.EqualTo(1));
            Assert.That(_builder.RequestFilters.ElementAt(0), Is.InstanceOf<ILinkRequestFilter>());
        }

        [Test]
        public void Can_Add_CopyControl() {
            //Act
            _builder.ConfigureCopyControls();

            //Assert
            Assert.That(_builder.CopyControlBuilder, Is.Not.Null);
            Assert.That(_builder.CopyControlBuilder, Is.InstanceOf<CopyControlBuilder>());
        }

        [Test]
        public void Can_Combine_All_Options() {
            //Act
            _builder.SourceContext("/foo")
                    .TargetPath("/bar")
                    .ReturnNoAttributes()
                    .ConfigureCopyControls()
                        .CopyLocalAttributesFromSource()
                        .ConfigureLookupControls()
                            .ReturnAttributes(AttributeToReturn.WithDefinitionId(10));

            //Assert
            Assert.Pass("Compilation test only");
        }
        
        [Test]
        public void Can_Build_Basic_LinkRequest() {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            string expected = new XElement("BatchRequest",
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XElement("LinkRequest",
                    new XAttribute("name", "/foo"),
                    new XAttribute("targetLocation", "/bar"))).ToString();


            _builder.SourceContext("/foo")
                    .TargetPath("/bar");

            //Act
            var request = _builder.Build();
            string actual = _builder.Build().ToAdsml().ToString();

            //Assert
            Assert.That(request.SourcePath, Is.EqualTo(_builder.Source));
            Assert.That(request.TargetPath, Is.EqualTo(_builder.Target));
            Assert.That(request.RequestFilters.Count(), Is.EqualTo(_builder.RequestFilters.Count()));

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Can_Build_Advanced_LinkRequest() {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            string expected = new XElement("BatchRequest",
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XElement("LinkRequest",
                    new XAttribute("name", "/foo"),
                    new XAttribute("targetLocation", "/bar"),
                    new XAttribute("returnNoAttributes", "true"),
                    new XElement("CopyControls",
                        new XAttribute("copyLocalAttributes", "true"),
                        new XElement("LookupControls",
                            new XElement("LanguagesToReturn",
                                new XElement("Language",
                                    new XAttribute("id", "10"))))))).ToString();

            _builder.SourceContext("/foo")
                    .TargetPath("/bar")
                    .ReturnNoAttributes()
                    .ConfigureCopyControls()
                        .CopyLocalAttributesFromSource()
                        .ConfigureLookupControls()
                            .ReturnLanguages(LanguageToReturn.WithLanguageId(10));

            //Act
            var request = _builder.Build();
            string actual = _builder.Build().ToAdsml().ToString();

            //Assert
            Assert.That(request.SourcePath, Is.EqualTo(_builder.Source));
            Assert.That(request.TargetPath, Is.EqualTo(_builder.Target));
            Assert.That(request.RequestFilters.Count(), Is.EqualTo(_builder.RequestFilters.Count()));
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "A Source context must be specified.")]
        public void Validate_Throws_ApiSerializationValidationException_If_Source_Is_Not_Set() {
            //Act
            _builder.Build();
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "A Target path must be specified.")]
        public void Validate_Throws_ApiSerializationValidationException_If_Target_Is_Not_Set() {
            //Arrange
            _builder.SourceContext("/foo");
            
            //Act
            _builder.Build();
        }
    }
}