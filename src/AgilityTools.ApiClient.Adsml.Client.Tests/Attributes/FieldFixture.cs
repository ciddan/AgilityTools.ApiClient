using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Attributes
{
    [TestFixture]
    public class FieldFixture
    {
        [Test]
        public void Can_Instantiate_New_CompositeField() {
            //Act
            var field = new Field();

            //Assert
            Assert.That(field, Is.Not.Null);
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            string expected = 
                new XElement("Field",
                    new XAttribute("name", "name"),
                    new XAttribute("type", "text"),
                    new XCData("foo")).ToString();


            var field = new Field
                     {
                         Name = "name",
                         Type = "text",
                         Value = "foo"
                     };

            //Act
            string actual = field.ToAdsml().ToString();

            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Name not set.")]
        public void ToAdsml_Throws_InvalidOperationException_If_Name_Is_Not_Set() {
            //Arrange
            var field = new Field {Type = "foo"};

            //Act
            field.ToAdsml();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Type not set.")]
        public void ToAdsml_Throws_InvalidOperationException_If_Type_Is_Not_Set() {
            //Arrange
            var field = new Field { Name = "foo" };

            //Act
            field.ToAdsml();
        }
    }
}