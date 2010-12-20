using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace AgilityTools.ApiClient.Adsml.Client.Components.Attributes.Deserialization
{
    public class DeserializationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store) {
            container.Register(
                Component
                    .For<IAdsmlAttributeDeserializer>()
                    .ImplementedBy<CompositeAttributeDeserializer>()
                    .Named("CompositeAttribute")
                    .LifeStyle.Singleton
                );
        }
    }
}