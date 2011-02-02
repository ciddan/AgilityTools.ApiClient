using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public class DeserializationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store) {
            container.Register(
                Component
                    .For<IAdsmlAttributeDeserializer>()
                    .ImplementedBy<CompositeAttributeDeserializer>()
                    .Named("CompositeAttribute")
                    .LifeStyle.Singleton,
                Component
                    .For<IAdsmlAttributeDeserializer>()
                    .ImplementedBy<RelationAttributeDeserializer>()
                    .Named("RelationAttribute")
                    .LifeStyle.Singleton,
                Component
                    .For<IAdsmlAttributeDeserializer>()
                    .ImplementedBy<SimpleAttributeDeserializer>()
                    .Named("SimpleAttribute")
                    .LifeStyle.Singleton,
                Component
                    .For<IAdsmlAttributeDeserializer>()
                    .ImplementedBy<ContextAttributeDeserializer>()
                    .Named("ContextAttribute")
                    .LifeStyle.Singleton,
                Component
                    .For<IAdsmlAttributeDeserializer>()
                    .ImplementedBy<StructureAttributeDeserializer>()
                    .Named("StructureAttribute")
                    .LifeStyle.Singleton
                );
        }
    }
}