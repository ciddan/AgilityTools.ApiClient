using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Attributes
{
    [TestFixture]
    public class CompositeValueFixture
    {
        [Test]
        public void Can_Instantiate_New_CompositeValue() {
            //Act
            var cv = new CompositeValue();

            //Assert
            Assert.That(cv, Is.Not.Null);
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            string expected =
                new XElement("CompositeValue",
                    new XElement("Field",
                        new XAttribute("name", "foo"),
                        new XAttribute("type", "bar"),
                        new XCData("baz"))).ToString();



            var cv = new CompositeValue
                     {
                         Fields = new List<Field>
                                  {
                                      new Field
                                      {
                                          Name = "foo",
                                          Type = "bar",
                                          Value = "baz"
                                      }
                                  }
                     };

            //Act
            string actual = cv.ToAdsml().ToString();

            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ToAdsml_Returns_Empty_CompositeValue_Tag_If_No_Fields() {
            //Arrange
            string expected = new XElement("CompositeValue").ToString();
            var cv = new CompositeValue();

            //Act
            string actual = cv.ToAdsml().ToString();

            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}