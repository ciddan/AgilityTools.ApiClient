using System.Collections.Generic;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using System.Linq;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Requests.Builders
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
    public void CanInstantiateNewCreateRequestBuilder() {
      //Act
      var builder = new CreateRequestBuilder();

      //Assert
      Assert.That(builder, Is.Not.Null);
    }

    [Test]
    public void CanSpecifyParentIdPath() {
      //Act
      _builder.ParentIdPath("/1/2/3");

      //Assert
      Assert.That(_builder.ParentPath, Is.EqualTo("/1/2/3"));
    }

    [Test]
    public void CanSpecifyNewContextName() {
      //Act
      _builder.NewContextName("foo");

      //Assert
      Assert.That(_builder.ContextName, Is.EqualTo("foo"));
    }

    [Test]
    public void CanSpecifyObjectTypeToCreate() {
      //Act
      _builder.ObjectTypeToCreate("bar");

      //Assert
      Assert.That(_builder.ObjectType, Is.EqualTo("bar"));
    }

    [Test]
    public void CanSetReturnNoAttributes() {
      //Arrange
      var builder = new CreateRequestBuilder();

      //Act
      builder.ReturnNoAttributes();

      //Assert
      Assert.That(builder.RequestFilters[0], Is.InstanceOf<ReturnNoAttributesFilter>());
    }

    [Test]
    public void CanSetFailOnError() {
      //Arrange
      var builder = new CreateRequestBuilder();

      //Act
      builder.FailOnError();

      //Assert
      Assert.That(builder.RequestFilters[0], Is.InstanceOf<FailOnErrorFilter>());
    }

    [Test]
    public void CanSetUpdateIfExists() {
      //Arrange
      var builder = new CreateRequestBuilder();

      //Act
      builder.UpdateIfExists();

      //Assert
      Assert.That(builder.RequestFilters[0], Is.InstanceOf<UpdateIfExistsFilter>());
    }

    [Test]
    public void CanSetAttributesToBeCreatedWithParamsOverload() {
      //Act
      _builder.AttributesToSet(StructureAttribute.New(10, new StructureValue(10, "foo")));

      //Assert
      Assert.That(_builder.Attributes.Count(), Is.EqualTo(1));
    }

    [Test]
    public void CanSetAttributesToBeCreatedWithListOverload() {
      //Arrange
      var builder = new CreateRequestBuilder();
      var attributes = new List<StructureAttribute> { StructureAttribute.New(10, new StructureValue(10, "foo")) };

      //Act
      builder.AttributesToSet(attributes);

      //Assert
      Assert.That(builder.Attributes.Count(), Is.EqualTo(1));
    }

    [Test]
    public void CanSetAttributesToBeCreatedWithListFactoryOverload() {
      //Arrange
      var builder = new CreateRequestBuilder();

      //Act
      builder.AttributesToSet(
        () => new List<IAdsmlAttribute> {
            StructureAttribute.New(10, new StructureValue(10, "foo")),
            SimpleAttribute.New(AttributeTypes.Text, "objectName", "foo")
          }
      );

      //Assert
      Assert.That(builder.Attributes.Count(), Is.EqualTo(2));
    }

    [Test]
    public void CanConfigureLookupControls() {
      //Act
      _builder.ConfigureLookupControls().ReturnLanguages(LanguageToReturn.WithLanguageId(10));

      //Assert
      Assert.That(_builder.LookupControlBuilder, Is.Not.Null);
    }

    [Test]
    public void CanCombineAllCommands() {
      //Arrange
      var builder = new CreateRequestBuilder();

      //Act
      builder.ParentIdPath("/1/2")
             .NewContextName("foo")
             .ObjectTypeToCreate("baz")
             .ReturnNoAttributes()
             .FailOnError()
             .UpdateIfExists()
             .AttributesToSet(
                  StructureAttribute.New(215, new StructureValue(10, "169010")))
             .ConfigureLookupControls()
                  .ReturnAttributes(AttributeToReturn.WithName("Artikelnummer"))
                  .ReturnLanguages(LanguageToReturn.WithLanguageId(10))
                  .ConfigureReferenceHandling(ReferenceOptions.ReturnValuesOnly());

      var request = new BatchRequest(builder.Build());

      //Assert
      Assert.DoesNotThrow(() => builder.Build());
      Assert.DoesNotThrow(() => request.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void CanBuildCreateRequest() {
      //Arrange
      var builder = new CreateRequestBuilder();

      //Act
      builder
        .NewContextName("/foo/bar")
        .ObjectTypeToCreate("baz")
        .ReturnNoAttributes()
        .FailOnError()
        .UpdateIfExists()
        .AttributesToSet(
          StructureAttribute.New(215, new StructureValue(10, "169010"))
        )
        .ConfigureLookupControls()
          .ReturnAttributes(AttributeToReturn.WithName("Artikelnummer"))
          .ReturnLanguages(LanguageToReturn.WithLanguageId(10))
          .ConfigureReferenceHandling(
            ReferenceOptions.ReturnValuesOnly()
          );

      var request = new BatchRequest(builder.Build());

      //Assert
      Assert.That(builder.Build(), Is.Not.Null);
      Assert.DoesNotThrow(() => request.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }
  }
}