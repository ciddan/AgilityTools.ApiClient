using System;
using System.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using System.Collections.Generic;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Requests.Builders
{
  [TestFixture]
  public class ModifyRequestBuilderFixture
  {
    [Test]
    public void Can_Instantiate_New_ModifyRequestBuilder() {
      //Act
      var builder = new ModifyRequestBuilder();

      //Assert
      Assert.That(builder, Is.Not.Null);
    }

    [Test]
    public void Can_Specify_Which_Context_To_Modify() {
      //Arrange
      var builder = new ModifyRequestBuilder();

      //Act
      builder.Context("/foo/bar");

      //Assert
      Assert.That(builder.ContextToModify, Is.EqualTo("/foo/bar"));
    }

    [Test]
    public void Can_Set_ReturnNoAttributes_Filter() {
      //Arrange
      var builder = new ModifyRequestBuilder();

      //Act
      builder.ReturnNoAttributes();

      //Assert
      Assert.That(builder.RequestFilters.Count(), Is.EqualTo(1));
      Assert.That(builder.RequestFilters.ElementAt(0), Is.InstanceOf<ReturnNoAttributesFilter>());
    }

    [Test]
    public void Can_Set_FailOnError_Filter() {
      //Arrange
      var builder = new ModifyRequestBuilder();

      //Act
      builder.FailOnError();

      //Assert
      Assert.That(builder.RequestFilters.Count(), Is.EqualTo(1));
      Assert.That(builder.RequestFilters.ElementAt(0), Is.InstanceOf<FailOnErrorFilter>());
    }

    [Test]
    public void Can_Add_ModificationItem() {
      //Arrange
      var builder = new ModifyRequestBuilder();

      //Act
      builder.AddModification(Modifications.RemoveValue, SimpleAttribute.New(AttributeTypes.Integer, "objectId"));

      //Assert
      Assert.That(builder.Modifications.Count(), Is.EqualTo(1));
    }

    [Test]
    public void Can_Add_ModificationItems_With_ListFactory() {
      //Arrange
      var builder = new ModifyRequestBuilder();

      //Act
      builder.AddModifications(() => new List<ModificationItem> {
        ModificationItem.New(
          Modifications.ReplaceAttribute,
          SimpleAttribute.New(AttributeTypes.Integer, "foo", "bar")
        ),
        ModificationItem.New(
          Modifications.AddAttribute,
          RelationAttribute.New("foo", "bar")
        )
      });

      //Assert
      Assert.That(builder.Modifications.Count(), Is.EqualTo(2));
    }

    [Test]
    public void Can_Specify_LookupControls() {
      //Arrange
      var builder = new ModifyRequestBuilder();

      //Act
      builder.ConfigureLookupControls();

      //Assert
      Assert.That(builder.LookupControlBuilder, Is.Not.Null);
      Assert.That(builder.LookupControlBuilder, Is.InstanceOf<LookupControlBuilder>());
    }

    [Test]
    public void Can_Combine_All_Operations() {
      //Arrange
      var builder = new ModifyRequestBuilder();

      //Act
      builder.Context("/foo/bar")
             .ReturnNoAttributes()
             .FailOnError()
             .AddModification(Modifications.RemoveAttribute, SimpleAttribute.New(AttributeTypes.Integer, "objectId"))
             .AddModifications(() => new List<ModificationItem> {
               ModificationItem.New(
                 Modifications.AdvanceState, StructureAttribute.New(390, new StructureValue(10))
               ),
               ModificationItem.New(
                 Modifications.RegressState, StructureAttribute.New(24, new StructureValue(10))
               )
             })
             .ConfigureLookupControls()
                .ReturnAttributes(390, 24)
                .ReturnLanguages(10);

      var request = new BatchRequest(builder.Build());
      Console.WriteLine(builder.Build().ToAdsml().ToString());

      //Assert
      Assert.DoesNotThrow(() => builder.Build());
      Assert.DoesNotThrow(() => request.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void Can_Build_ModifyRequest() {
      //Arrange
      var builder = new ModifyRequestBuilder();

      //Act
      builder.Context("/foo/bar")
             .AddModification(Modifications.RemoveAttribute, SimpleAttribute.New(AttributeTypes.Integer, "objectId"));

      var request = builder.Build();
      var batchRequest = new BatchRequest(request);

      //Assert
      Assert.That(request, Is.Not.Null);
      Assert.That(request, Is.InstanceOf<ModifyRequest>());

      Assert.DoesNotThrow(() => batchRequest.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void Can_Build_ModifyRequest_With_Request_Filters() {
      //Arrange
      var builder = new ModifyRequestBuilder();

      //Act
      builder.Context("/foo/bar")
             .ReturnNoAttributes()
             .FailOnError()
             .AddModification(Modifications.RemoveAttribute, SimpleAttribute.New(AttributeTypes.Integer, "objectId"));

      var request = builder.Build();
      var batchRequest = new BatchRequest(request);

      //Assert
      Assert.That(request.RequestFilters.Count(), Is.EqualTo(2));
      Assert.That(request.RequestFilters.ElementAt(0), Is.InstanceOf<ReturnNoAttributesFilter>());
      Assert.That(request.RequestFilters.ElementAt(1), Is.InstanceOf<FailOnErrorFilter>());

      Assert.DoesNotThrow(() => batchRequest.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void Can_Build_ModifyRequest_With_LookupControls() {
      //Arrange
      var builder = new ModifyRequestBuilder();

      //Act
      builder.Context("/foo/bar")
             .AddModification(Modifications.RemoveAttribute, SimpleAttribute.New(AttributeTypes.Integer, "objectId"))
             .ConfigureLookupControls();

      var request = builder.Build();
      var batchRequest = new BatchRequest(request);

      //Assert
      Assert.That(request.LookupControl, Is.Not.Null);
      Assert.That(request.LookupControl, Is.InstanceOf<LookupControl>());

      Assert.DoesNotThrow(() => batchRequest.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }
  }
}