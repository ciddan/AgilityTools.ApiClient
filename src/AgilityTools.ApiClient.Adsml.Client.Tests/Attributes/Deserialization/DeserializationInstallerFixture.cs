using AgilityTools.ApiClient.Adsml.Client.Components;
using Castle.MicroKernel;
using Castle.Windsor;
using NUnit.Framework;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Attributes.Deserialization
{
    [TestFixture]
    public class DeserializationInstallerFixture
    {
        [Test]
        public void Installer_Installs_CompositeAttributeDeserializer() {
            //Arrange
            var container = new WindsorContainer();
            container.Install(new DeserializationInstaller());

            //Act
            try {
                //Assert

                var actual = container.Resolve<IAdsmlAttributeDeserializer>("CompositeAttribute");

                Assert.That(actual, Is.Not.Null);
                Assert.That(actual, Is.InstanceOf<CompositeAttributeDeserializer>());
            }
            catch (ComponentNotFoundException) {
                Assert.Fail("Component not installed.");
            }
        }

        [Test]
        public void Installer_Installs_RelationAttributeDeserializer() {
            //Arrange
            var container = new WindsorContainer();
            container.Install(new DeserializationInstaller());

            //Act
            try {
                //Assert

                var actual = container.Resolve<IAdsmlAttributeDeserializer>("RelationAttribute");

                Assert.That(actual, Is.Not.Null);
                Assert.That(actual, Is.InstanceOf<RelationAttributeDeserializer>());
            }
            catch (ComponentNotFoundException) {
                Assert.Fail("Component not installed.");
            }
        }

        [Test]
        public void Installer_Installs_SimpleAttributeDeserializer() {
            //Arrange
            var container = new WindsorContainer();
            container.Install(new DeserializationInstaller());

            //Act
            try {
                //Assert

                var actual = container.Resolve<IAdsmlAttributeDeserializer>("SimpleAttribute");

                Assert.That(actual, Is.Not.Null);
                Assert.That(actual, Is.InstanceOf<SimpleAttributeDeserializer>());
            }
            catch (ComponentNotFoundException) {
                Assert.Fail("Component not installed.");
            }
        }

        [Test]
        public void Installer_Installs_ContextAttributeDeserializer() {
            //Arrange
            var container = new WindsorContainer();
            container.Install(new DeserializationInstaller());

            //Act
            try {
                //Assert

                var actual = container.Resolve<IAdsmlAttributeDeserializer>("ContextAttribute");

                Assert.That(actual, Is.Not.Null);
                Assert.That(actual, Is.InstanceOf<ContextAttributeDeserializer>());
            }
            catch (ComponentNotFoundException) {
                Assert.Fail("Component not installed.");
            }
        }

        [Test]
        public void Installer_Installs_StructureAttributeDeserializer() {
            //Arrange
            var container = new WindsorContainer();
            container.Install(new DeserializationInstaller());

            //Act
            try {
                //Assert

                var actual = container.Resolve<IAdsmlAttributeDeserializer>("StructureAttribute");

                Assert.That(actual, Is.Not.Null);
                Assert.That(actual, Is.InstanceOf<StructureAttributeDeserializer>());
            }
            catch (ComponentNotFoundException) {
                Assert.Fail("Component not installed.");
            }
        }
    }
}