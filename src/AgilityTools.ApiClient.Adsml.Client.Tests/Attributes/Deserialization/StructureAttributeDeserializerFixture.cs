using System;
using System.Linq;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes.Deserialization;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Attributes.Deserialization
{
    [TestFixture]
    public class StructureAttributeDeserializerFixture
    {
        [Test]
        public void Instantiation() {
            //Act
            var sad = new StructureAttributeDeserializer();

            //Assert
            Assert.That(sad, Is.Not.Null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException), ExpectedMessage = "Value cannot be null.\r\nParameter name: element")]
        public void Throws_ArgumentNullException_If_Element_Is_Null() {
            //Arrange
            var sad = new StructureAttributeDeserializer();

            //Act
            sad.Deserialize(null);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException), ExpectedMessage = "Not a valid StructureAttribute.")]
        public void Throws_InvalidOperationException_If_Element_Name_Is_Not_StructureAttribute()
        {
            //Arrange
            var sad = new StructureAttributeDeserializer();

            //Act
            sad.Deserialize(new XElement("foo"));
        }

        [Test]
        public void Can_Deserialize_StructureAttribute_Xml() {
            //Arrange
            XElement xml = GetTestData();

            var deserializer = new StructureAttributeDeserializer();

            //Act
            var deserialized = deserializer.Deserialize(xml);
            var actual = (StructureAttribute) deserialized;

            //Assert
            Assert.That(deserialized, Is.Not.Null);
            Assert.That(deserialized, Is.InstanceOf<StructureAttribute>());

            Assert.That(actual.Name, Is.EqualTo("Brödtext"));
            Assert.That(actual.DefinitionId, Is.EqualTo(26));

            Assert.That(actual.Values.Count, Is.EqualTo(4));

            Assert.That(actual.Values.Any(v => v.LanguageId == 10));
            Assert.That(actual.Values.Any(v => v.LanguageId == 11));
            Assert.That(actual.Values.Any(v => v.LanguageId == 12));
            Assert.That(actual.Values.Any(v => v.LanguageId == 13));

            Assert.That(actual.Values.Where(v => v.LanguageId == 10).Select(v => v.Value).Single(), Is.Not.EqualTo(string.Empty));
            Assert.That(actual.Values.Where(v => v.LanguageId == 11).Select(v => v.Value).Single(), Is.Not.EqualTo(string.Empty));
            Assert.That(actual.Values.Where(v => v.LanguageId == 12).Select(v => v.Value).Single(), Is.EqualTo(string.Empty));
            Assert.That(actual.Values.Where(v => v.LanguageId == 13).Select(v => v.Value).Single(), Is.EqualTo(string.Empty));
        }

        [Test]
        public void Deserialize_Returns_Correct_Object_With_Less_Data_Input() {
            //Arrange
            var xml = new XElement("StructureAttribute", new XAttribute("name", "foo"), new XAttribute("id", "26"));

            var des = new StructureAttributeDeserializer();

            //Act
            var actual = (StructureAttribute) des.Deserialize(xml);

            //Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Name, Is.EqualTo("foo"));
            Assert.That(actual.DefinitionId, Is.EqualTo(26));
        }

        private static XElement GetTestData() {
            return new XElement("StructureAttribute",
                                new XAttribute("id", "26"),
                                new XAttribute("name", "Brödtext"),
                                new XElement("StructureValue",
                                             new XAttribute("created", "2007-08-27 10:40:48"),
                                             new XAttribute("isInherited", "false"),
                                             new XAttribute("langId", "10"),
                                             new XAttribute("modified", "2010-12-16 14:23:24"),
                                             new XAttribute("scope", "global"),
                                             new XAttribute("stateId", "17"),
                                             "Lokaliserar snabbt och enkelt spänningssatta ledningar, vattenledningar, reglar och balkar av trä och metall. Röd/grön diod visar var du kan borra. Inbyggd blyertspenna gör att du direkt kan markera var du ska borra. Överskådlig display som visar batterinivå, lokaliseringssätt och ström/spänningsindikering. Automatisk kalibrering. Kapacitet: stål 4 cm, koppar 6 cm, elkablar 4 cm, trä 2 cm. 1 st. 9V-6LR6."
                                    ),
                                new XElement("StructureValue",
                                             new XAttribute("created", "2007-09-18 16:39:02"),
                                             new XAttribute("isInherited", "false"),
                                             new XAttribute("langId", "11"),
                                             new XAttribute("modified", "2010-12-16 14:23:35"),
                                             new XAttribute("scope", "global"),
                                             new XAttribute("stateId", "19"),
                                             "Lokaliserer hurtig og enkelt spenningssatte ledninger, vannledninger, stendere og bjelker i tre og metall. Rød/grønn diode viser hvor du kan bore. Innebygd blyant gjør at du straks kan markere hvor du skal bore. Oversiktlig skjerm som viser batterinivå, lokaliseringsmåte og strøm/spenningsindikasjon. Automatisk kalibrering. Kapasitet: stål, 4 cm, kobber, 6 cm, elkabler, 4 cm, tre, 2 cm. 1 stk. 9V–6LR6."
                                    ),
                                new XElement("StructureValue",
                                             new XAttribute("created", "2010-09-03 23:35:42"),
                                             new XAttribute("isInherited", "false"),
                                             new XAttribute("langId", "12"),
                                             new XAttribute("modified", "2010-09-03 23:35:42"),
                                             new XAttribute("scope", "global"),
                                             new XAttribute("stateId", "30")
                                    ),
                                new XElement("StructureValue",
                                             new XAttribute("created", "2010-09-06 21:36:34"),
                                             new XAttribute("isInherited", "false"),
                                             new XAttribute("langId", "13"),
                                             new XAttribute("modified", "2010-09-06 21:36:34"),
                                             new XAttribute("scope", "global"),
                                             new XAttribute("stateId", "33")
                                    )
                );

        }
    }
}