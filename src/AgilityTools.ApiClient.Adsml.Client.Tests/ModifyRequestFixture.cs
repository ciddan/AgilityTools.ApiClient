using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class ModifyRequestFixture
    {
        [Test]
        public void Can_Instantiate_New_ModifyRequest() {
            //Act
            var modReq = new ModifyRequest("foo", new List<ModificationItem>());

            //Assert
            Assert.That(modReq, Is.Not.Null);
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
            string expected =
                new XElement("BatchRequest",
                    new XAttribute(xsi + "noNamespaceSchemaLocation", "adsml.xsd"),
                    new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                    new XElement("ModifyRequest",
                        new XAttribute("name", "/foo/bar"),
                        new XElement("ModificationItem",
                            new XAttribute("operation", "addValue"),
                            new XElement("AttributeDetails",
                                new XElement("StructureAttribute",
                                    new XAttribute("id", "31"),
                                    new XElement("StructureValue",
                                        new XAttribute("langId", "10"),
                                        new XAttribute("scope", "global"),
                                        new XCData("foo"))))))).ToString();


            var modReq = new ModifyRequest("/foo/bar", new List<ModificationItem> {
                                                           ModificationItem.New(Modifications.AddValue,
                                                                                StructureAttribute.New(31,
                                                                                   new StructureValue(10, "foo")))});

            //Act
            string actual = modReq.ToAdsml().ToString();

            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}