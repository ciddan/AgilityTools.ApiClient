using System.Xml.Linq;
using Castle.Windsor;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public static class AttributeDeserializer
    {
        private static WindsorContainer _container;
        private static bool _initialized;

        public static IAdsmlAttribute Deserialize(XElement element)
        {
            if (!_initialized) {
                Initialize();
            }

            var deserializer =
                _container.Resolve<IAdsmlAttributeDeserializer>(element.Name.LocalName);

            return deserializer.Deserialize(element);
        }

        private static void Initialize() {
            _container = new WindsorContainer();

            _container.Install(new DeserializationInstaller());

            _initialized = true;
        }
    }
}