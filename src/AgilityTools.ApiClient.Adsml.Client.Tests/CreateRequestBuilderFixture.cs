using System.Collections.Generic;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using AgilityTools.ApiClient.Adsml.Client.Filters;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using System.Linq;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class CreateRequestBuilderFixture
    {
        private CreateRequestBuilder _builder;

        [SetUp]
        public void Setup() {
            _builder = new CreateRequestBuilder();
        }

        [Test]
        public void Can_Instantiate_New_CreateRequestBuilder() {
            //Act
            var builder = new CreateRequestBuilder();

            //Assert
            Assert.That(builder, Is.Not.Null);
        }

        [Test]
        public void Can_Specify_New_Context_Name() {
            //Act
            _builder.NewContextName("foo");

            //Assert
            Assert.That(_builder.ContextName, Is.EqualTo("foo"));
        }

        [Test]
        public void Can_Specify_Object_Type_To_Create() {
            //Act
            _builder.ObjectTypeToCreate("bar");

            //Assert
            Assert.That(_builder.ObjectType, Is.EqualTo("bar"));
        }

        [Test]
        public void Can_Set_Return_No_Attributes() {
            //Arrange
            var builder = new CreateRequestBuilder();

            //Act
            builder.ReturnNoAttributes();

            //Assert
            Assert.That(builder.RequestFilters[0], Is.InstanceOf<ReturnNoAttributesFilter>());
        }

        [Test]
        public void Can_Set_Fail_On_Error() {
            //Arrange
            var builder = new CreateRequestBuilder();

            //Act
            builder.FailOnError();

            //Assert
            Assert.That(builder.RequestFilters[0], Is.InstanceOf<FailOnErrorFilter>());
        }

        [Test]
        public void Can_Set_Attributes_To_Be_Created_With_Params_Overload() {
            //Act
            _builder.AttributesToSet(StructureAttribute.New(10, new StructureValue(10, "foo")));

            //Assert
            Assert.That(_builder.Attributes.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Can_Set_Attributes_To_Be_Created_With_List_Overload() {
            //Arrange
            var builder = new CreateRequestBuilder();
            var attributes = new List<StructureAttribute>{StructureAttribute.New(10, new StructureValue(10, "foo"))};

            //Act
            builder.AttributesToSet(attributes);

            //Assert
            Assert.That(builder.Attributes.Count(), Is.EqualTo(1));
        }

        [Test]
        public void Can_Set_Attributes_To_Be_Created_With_ListFactory_Overload() {
            //Arrange
            var builder = new CreateRequestBuilder();
            
            //Act
            builder.AttributesToSet(() => new List<IAdsmlAttribute<XElement>>
                                          {
                                              StructureAttribute.New(10, new StructureValue(10, "foo")),
                                              SimpleAttribute.New(AttributeTypes.Text, "objectName", "foo")
                                          });

            //Assert
            Assert.That(builder.Attributes.Count(), Is.EqualTo(2));
        }

        [Test]
        public void Can_Configure_LookupControls() {
            //Act
            _builder.ConfigureLookupControls().ReturnLanguages(LanguageToReturn.WithLanguageId(10));

            //Assert
            Assert.That(_builder.LookupControlBuilder, Is.Not.Null);
        }

        [Test]
        public void Can_Combine_All_Commands() {
            //Arrange
            var builder = new CreateRequestBuilder();

            //Act
            builder.NewContextName("/foo/bar")
                   .ObjectTypeToCreate("baz")
                   .ReturnNoAttributes()
                   .FailOnError()
                   .AttributesToSet(
                        StructureAttribute.New(215, new StructureValue(10, "169010")))
                   .ConfigureLookupControls()
                        .ReturnAttributes(AttributeToReturn.WithDefinitionId(215))
                        .ReturnLanguages(LanguageToReturn.WithLanguageId(10))
                        .ConfigureReferenceHandling(ReferenceOptions.ReturnValuesOnly());

            //Assert
            Assert.Pass();
        }

        [Test]
        public void Can_Build_CreateRequest() {
            //Arrange
            var builder = new CreateRequestBuilder();

            builder.NewContextName("/foo/bar")
                   .ObjectTypeToCreate("baz")
                   .ReturnNoAttributes()
                   .FailOnError()
                   .AttributesToSet(
                       StructureAttribute.New(215, new StructureValue(10, "169010")))
                   .ConfigureLookupControls()
                       .ReturnAttributes(AttributeToReturn.WithDefinitionId(215))
                       .ReturnLanguages(LanguageToReturn.WithLanguageId(10))
                       .ConfigureReferenceHandling(ReferenceOptions.ReturnValuesOnly());

            //Act
            var request = builder.Build();

            //Assert
            Assert.That(request, Is.Not.Null);
        }
    }
}