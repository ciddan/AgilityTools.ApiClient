using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class StructureAttributeFixture
    {
        [Test]
        public void Can_Instantiate_New_StructureAttribute() {
            //Act
            var sAttr = new StructureAttribute();

            //Assert
            Assert.That(sAttr, Is.Not.Null);
        }

        [Test]
        public void Can_Create_New_StructureAttribute_Using_Factory_Method_Providing_Attribute_Name() {
            //Act
            var sa = StructureAttribute.New("foo", new StructureValue(10, "bar"));

            //Assert
            Assert.That(sa.Name, Is.EqualTo("foo"));
            Assert.That(sa.Values.Count(), Is.EqualTo(1));
            Assert.That(sa.Values[0].Value, Is.EqualTo("bar"));
        }

        [Test]
        public void Can_Create_New_StructureAttribute_Using_Factory_Method_Providing_DefinitionId() {
            //Act
            var sa = StructureAttribute.New(10, new StructureValue(10, "bar"));

            //Assert
            Assert.That(sa.DefinitionId, Is.EqualTo(10));
            Assert.That(sa.Values.Count(), Is.EqualTo(1));
            Assert.That(sa.Values[0].Value, Is.EqualTo("bar"));
        }

        [Test]
        public void Should_Be_Able_To_Add_StructureValues_In_Constructor() {
            //Act
            var structAttribute =
                new StructureAttribute(new List<StructureValue>
                                       {
                                           new StructureValue
                                           {
                                               LanguageId = 10,
                                               Scope = "global",
                                               Value = "foo"
                                           }
                                       });

            //Assert
            Assert.That(structAttribute, Is.Not.Null);
            Assert.That(structAttribute.Values.Count, Is.EqualTo(1));
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            var expected = new XElement("StructureAttribute",
                                        new XAttribute("id", "46"),
                                        new XAttribute("name", "bar"),
                                        new XElement("StructureValue",
                                                     new XAttribute("langId", "10"),
                                                     new XAttribute("scope", "global"),
                                                     "foo"));

            var value = new StructureValue {LanguageId = 10, Value = "foo"};
            var attr = new StructureAttribute{DefinitionId = 46, Name = "bar", Values = new List<StructureValue>{value}};
            
            //Act
            var actual = attr.ToAdsml();

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        public void Can_Generate_Api_Xml_Without_Specifying_Attribute_Name()
        {
            //Arrange
            var expected = new XElement("StructureAttribute",
                                        new XAttribute("id", "46"),
                                        new XElement("StructureValue",
                                                     new XAttribute("langId", "10"),
                                                     new XAttribute("scope", "global"),
                                                     "foo"));

            var value = new StructureValue { LanguageId = 10, Value = "foo" };
            var attr = new StructureAttribute { DefinitionId = 46, Values = new List<StructureValue> { value } };

            //Act
            var actual = attr.ToAdsml();

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }

        [Test]
        [ExpectedException(typeof (ApiSerializationValidationException), ExpectedMessage = "DefinitionId has to be set.")]
        public void ToApiXml_Throws_ASVE_If_DefinitionId_Is_Not_Set() {
            //Arrange
            var value = new StructureValue { LanguageId = 10, Value = "foo" };
            var attr = new StructureAttribute { Values = new List<StructureValue> { value } };

            //Act
            attr.ToAdsml();
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "At least one StructureAttribute Value must be specified.")]
        public void ToApiXml_Throws_ASVE_If_Values_Is_Not_Set()
        {
            //Arrange
            var value = new StructureValue { LanguageId = 10, Value = "foo" };
            var attr = new StructureAttribute { DefinitionId = 10 };

            //Act
            attr.ToAdsml();
        }
    }
}