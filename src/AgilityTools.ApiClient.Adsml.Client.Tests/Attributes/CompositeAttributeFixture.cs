using System;
using System.Collections.Generic;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Attributes
{
    [TestFixture]
    public class CompositeAttributeFixture
    {
        [Test]
        public void Can_Instantiate_New_CompositeAttribute() {
            //Act
            var ca = new CompositeAttribute();

            //Assert
            Assert.That(ca, Is.Not.Null);
        }

        [Test]
        public void Can_Generate_Api_Xml() {
            //Arrange
            string expected = 
                new XElement("CompositeAttribute",
                    new XAttribute("name", "foo"),
                    new XElement("CompositeValue",
                        new XElement("Field",
                            new XAttribute("name", "bar"),
                            new XAttribute("type", "text"),
                            new XCData("baz")))).ToString();


            var ca = new CompositeAttribute
                     {
                         Name = "foo",
                         CompositeValues = new List<CompositeValue>
                                           {
                                               new CompositeValue
                                               {
                                                   Fields = new List<Field>
                                                            {
                                                                new Field
                                                                {
                                                                    Name = "bar",
                                                                    Type = "text",
                                                                    Value = "baz"
                                                                }
                                                            }
                                               }
                                           }
                     };

            //Act
            string actual = ca.ToAdsml().ToString();

            Console.WriteLine(actual);

            //Assert
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Name not set.")]
        public void ToAdsml_Throws_InvalidOperationException_If_Name_Is_Not_Set() {
            //Arrange
            var ca = new CompositeAttribute
                     {
                         CompositeValues = new List<CompositeValue>
                                           {
                                               new CompositeValue
                                               {
                                                   Fields = new List<Field>
                                                            {
                                                                new Field
                                                                {
                                                                    Name = "bar",
                                                                    Type = "text",
                                                                    Value = "baz"
                                                                }
                                                            }
                                               }
                                           }
                     };

            //Act
            ca.ToAdsml();
        }
    }
}