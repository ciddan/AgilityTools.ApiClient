using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes.Deserialization;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Attributes.Deserialization
{
    [TestFixture]
    public class AttributeDeserializerFixture
    {
        [Test]
        public void Resolves_Correct_CompositeAttribute_Deserializer_By_Element_Name() {
            //Arrange
            var element = new XElement("CompositeAttribute", new XAttribute("name", "foo"));

            //Act
            var attr = AttributeDeserializer.Deserialize(element);

            //Assert
            Assert.That(attr, Is.Not.Null);
            Assert.That(attr, Is.InstanceOf<CompositeAttribute>());
        }

        [Test]
        public void Resolves_Correct_RelationAttribute_Deserializer_By_Element_Name() {
            //Arrange
            var element = new XElement("RelationAttribute", new XAttribute("name", "foo"));

            //Act
            var attr = AttributeDeserializer.Deserialize(element);

            //Assert
            Assert.That(attr, Is.Not.Null);
            Assert.That(attr, Is.InstanceOf<RelationAttribute>());
        }

        [Test]
        public void Resolves_Correct_SimpleAttribute_Deserializer_By_Element_Name() {
            //Arrange
            var element = new XElement("SimpleAttribute", new XAttribute("name", "foo"), new XAttribute("type", "date"));

            //Act
            var attr = AttributeDeserializer.Deserialize(element);

            //Assert
            Assert.That(attr, Is.Not.Null);
            Assert.That(attr, Is.InstanceOf<SimpleAttribute>());
        }

        [Test]
        public void Resolves_Correct_ContextAttribute_Deserializer_By_Element_Name() {
            //Arrange
            var element = new XElement("ContextAttribute", new XAttribute("name", "foo"));

            //Act
            var attr = AttributeDeserializer.Deserialize(element);

            //Assert
            Assert.That(attr, Is.Not.Null);
            Assert.That(attr, Is.InstanceOf<ContextAttribute>());
        }

        [Test]
        public void Resolves_Correct_StructureAttribute_Deserializer_By_Element_Name() {
            //Arrange
            var element = new XElement("StructureAttribute", new XAttribute("name", "foo"));

            //Act
            var attr = AttributeDeserializer.Deserialize(element);

            //Assert
            Assert.That(attr, Is.Not.Null);
            Assert.That(attr, Is.InstanceOf<StructureAttribute>());
        }

        [Test]
        public void Can_Deserialize_Collection_Of_Attributes_With_Linq_Statement() {
            //Arrange
            var data = GetTestData().Descendants().Where(r => r.Name.ToString().Contains("Attribute"));

            //Act
            IList<IAdsmlAttribute> attributes = new List<IAdsmlAttribute>(data.Select(AttributeDeserializer.Deserialize));

            //Assert
            Assert.That(attributes, Is.Not.Null);
            Assert.That(attributes.Count, Is.EqualTo(5));

            Assert.That(attributes.Any(a => a is StructureAttribute));
            Assert.That(attributes.Any(a => a is SimpleAttribute));
            Assert.That(attributes.Any(a => a is RelationAttribute));
            Assert.That(attributes.Any(a => a is ContextAttribute));
            Assert.That(attributes.Any(a => a is CompositeAttribute));
        }

        private static XElement GetTestData()
        {
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            return new XElement("BatchResponse",
                new XAttribute("version", "5.1.16 build 116 (2010/05/27 14-36)"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XElement("ErrorResponse",
                    new XAttribute("id", "4024"),
                    new XAttribute("type", "malformedRequest"),
                    new XElement("Message", "Error at line 1 column 111. cvc-elt.1: Cannot find the declaration of element 'BatchRequest'..")
                ),
                new XElement("SearchResponse",
                    new XAttribute("executionTime", "1094ms"),
                    new XAttribute("resultCount", "1"),
                    new XElement("SearchResults",
                        new XAttribute("base", "/Structures/Classification/JULA Produkter"),
                        new XElement("StructureContext",
                            new XAttribute("idPath", "12:75:119:121:1777"),
                            new XAttribute("name", "/Structures/Classification/JULA Produkter/Handverktyg/Mätinstrument/Regelsökare/169010 MULTIDETECTOR TRÄ@fs:METALL@fs:EL"),
                            new XAttribute("sortPath", "0:2:5:2:1"),
                            new XElement("StructureAttribute",
                                new XAttribute("id", "215"),
                                new XAttribute("name", "Artikelnummer"),
                                new XElement("StructureValue",
                                    new XAttribute("created", "2007-09-18 16:39:02"),
                                    new XAttribute("isInherited", "false"),
                                    new XAttribute("langId", "11"),
                                    new XAttribute("modified", "2007-08-27 10:40:48"),
                                    new XAttribute("scope", "global"),
                                    "169010"
                                ),
                                new XElement("StructureValue",
                                    new XAttribute("created", "2010-09-03 23:35:42"),
                                    new XAttribute("isInherited", "false"),
                                    new XAttribute("langId", "12"),
                                    new XAttribute("modified", "2007-08-27 10:40:48"),
                                    new XAttribute("scope", "global"),
                                    "169010"
                                ),
                                new XElement("StructureValue",
                                    new XAttribute("created", "2010-09-06 21:36:34"),
                                    new XAttribute("isInherited", "false"),
                                    new XAttribute("langId", "13"),
                                    new XAttribute("modified", "2007-08-27 10:40:48"),
                                    new XAttribute("scope", "global"),
                                    "169010"
                                ),
                                new XElement("StructureValue",
                                    new XAttribute("created", "2007-08-27 10:40:48"),
                                    new XAttribute("isInherited", "false"),
                                    new XAttribute("langId", "10"),
                                    new XAttribute("modified", "2007-08-27 10:40:48"),
                                    new XAttribute("scope", "global"),
                                    "169010"
                                )
                            ),
                            new XElement("SimpleAttribute",
                                new XAttribute("name", "name"),
                                new XAttribute("type", "text"),
                                new XElement("Value", "169010 MULTIDETECTOR TRÄ/METALL/EL")
                            ),
                            new XElement("CompositeAttribute",
                            new XAttribute("name", "members"),
                                new XElement("CompositeValue",
                                    new XElement("Field",
                                    new XAttribute("name", "id"),
                                    new XAttribute("type", "integer"),
                                    new XCData("28")),
                                new XElement("Field",
                                    new XAttribute("name", "name"),
                                    new XAttribute("type", "text"),
                                    new XCData("Säljtext2")),
                                new XElement("Field",
                                    new XAttribute("name", "dataType"),
                                    new XAttribute("type", "text"),
                                    new XCData("Text")))
                            ),
                            new XElement("RelationAttribute",
                                new XAttribute("id", "31"),
                                new XAttribute("name", "Tillverkarrelation"),
                                new XElement("Value", new XCData("/Structures/Vendor/JULA Tillverkare/Bosch")),
                                new XElement("Value", new XCData("/Structures/Vendor/JULA Tillverkare/Abac"))
                            ),
                            new XElement("ContextAttribute",
                                new XAttribute("nameParserClass", "foo"),
                                new XElement("Value", "Foo/Bar"),
                                new XElement("Value", "Bar/Foo")
                            )
                        )
                    )
                )
            );
        }
    }
}