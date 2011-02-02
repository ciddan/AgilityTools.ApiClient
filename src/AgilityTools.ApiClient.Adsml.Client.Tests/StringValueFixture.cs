using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests
{
    [TestFixture]
    public class StringValueAttributeFixture
    {
        [Test]
        public void Can_Get_StringValue_From_Enum() {
            //Act
            var val = Foo.Foo.GetStringValue();

            //Assert
            Assert.That(val, Is.EqualTo("foo"));
        }

        [Test]
        public void GetStringValue_Returns_Null_If_No_StringValue() {
            //Act
            var val = Foo.Bar.GetStringValue();

            //Assert
            Assert.That(val, Is.Null);
        }
    }

    internal enum Foo
    {
        [StringValue("foo")]
        Foo,
        Bar
    }
}