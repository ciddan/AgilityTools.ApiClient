using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class CreateRequestFixture
    {
        [Test]
        public void Can_Instatiate_New_CreateRequest() {
            //Arrange
            var attrs = new List<StructureAttribute>();
            
            //Act
            var cr = new CreateRequest("foo", "bar", new StructureAttribute());

            //Assert
            Assert.That(cr, Is.Not.Null);
            Assert.That(cr.ObjectTypeName, Is.EqualTo("foo"));
            Assert.That(cr.CreationPath, Is.EqualTo("bar"));
            Assert.That(cr.AttributesToSet.Count, Is.AtLeast(1));
        }

        [Test]
        [ExpectedException(typeof (ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: attributesToSet")]
        public void CreateRequest_Ctor_Throwns_ArgumentNullException_If_AttributesToSet_Is_Null() {
            //Act
            new CreateRequest("foo", "bar", null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "ObjectTypeName cannot be null or empty.")]
        public void CreateRequest_Ctor_Throwns_InvalidOperationException_If_ObjectTypeName_Is_NullOrEmpty()
        {
            //Act
            new CreateRequest(null, "bar", new StructureAttribute());
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "CreationPath cannot be null or empty.")]
        public void CreateRequest_Ctor_Throwns_InvalidOperationException_If_CreationPath_Is_NullOrEmpty()
        {
            //Act
            new CreateRequest("foo", string.Empty, new StructureAttribute());
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            var expected = new XElement("BatchRequest",
                new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                new XElement("CreateRequest",
                    new XAttribute("name", "fooPath"),
                    new XAttribute("type", "fooObjectTypeName"),
                    new XElement("AttributesToSet",
                        new XElement("StructureAttribute",
                            new XAttribute("id", "215"),
                            new XAttribute("name", "fooAttributeName"),
                            new XElement("StructureValue",
                                new XAttribute("langId", "10"),
                                new XAttribute("scope", "global"),
                                "fooValue"
                            )
                        )
                    )
                )
            );

            var value = new StructureValue {LanguageId = 10, Value = "fooValue"};
            var attribute = new StructureAttribute
                            {DefinitionId = 215, Name = "fooAttributeName", Values = new List<StructureValue> {value}};

            var request = new CreateRequest("fooObjectTypeName", "fooPath", attribute);
            
            //Act
            var actual = request.ToAdsml();

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.ToString(), Is.EqualTo(expected.ToString()));
        }
    }
}