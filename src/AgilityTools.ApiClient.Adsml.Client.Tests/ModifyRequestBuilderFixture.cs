using System.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using AgilityTools.ApiClient.Adsml.Client.Filters;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using System.Collections.Generic;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
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
        public void Can_Add_ModificationItem()
        {
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
                                              ModificationItem.New(Modifications.ReplaceAttribute,
                                                                   SimpleAttribute.New(AttributeTypes.Integer, "foo", "bar")),
                                              ModificationItem.New(Modifications.AddAttribute,
                                                                   RelationAttribute.New("foo", "bar"))
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
                .AddModifications(() => new List<ModificationItem>())
                .ConfigureLookupControls();

            //Assert
            Assert.Pass();
        }
    }
}