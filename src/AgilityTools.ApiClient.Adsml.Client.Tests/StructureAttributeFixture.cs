using System.Collections.Generic;
using System.Xml.Linq;
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
            var actual = attr.ToApiXml();

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }
    }
}