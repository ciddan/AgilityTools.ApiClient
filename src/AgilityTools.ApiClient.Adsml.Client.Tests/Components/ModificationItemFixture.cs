using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Components
{
    [TestFixture]
    public class ModificationItemFixture
    {
        [Test]
        public void Can_Instantiate_New_ModificationItem() {
            //Act
            var modItem = new ModificationItem();

            //Assert
            Assert.That(modItem, Is.Not.Null);
        }

        [Test]
        public void Can_Instantiate_New_ModificationItem_With_FactoryMethod() {
            //Act
            var modItem = ModificationItem.New(Modifications.RemoveValue,
                                               SimpleAttribute.New(AttributeTypes.Text, "foo"));

            //Assert
            Assert.That(modItem, Is.Not.Null);
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            var expected = new XElement("ModificationItem",
                                new XAttribute("operation", "replaceValue"),
                                new XElement("AttributeDetails",
                                    new XElement("StructureAttribute",
                                        new XAttribute("id", "421"),
                                        new XAttribute("name", "yy Artikelstatus MMS001"),
                                        new XElement("StructureValue",
                                            new XAttribute("langId", "10"),
                                            new XAttribute("scope", "global"),
                                            new XCData("60"))))).ToString();


            //Act
            var actual = ModificationItem.New(Modifications.ReplaceValue, 
                StructureAttribute.New("yy Artikelstatus MMS001", 421, new StructureValue(10, "60"))).ToAdsml().ToString();

            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException(typeof(ApiSerializationValidationException), ExpectedMessage = "You must specify an attribute to modify.")]
        public void Validate_Throws_ASVE_If_No_Attribute_Is_Specified() {
            //Arrange
            var modItem = new ModificationItem();

            //Act
            modItem.ToAdsml();
        }
    }
}