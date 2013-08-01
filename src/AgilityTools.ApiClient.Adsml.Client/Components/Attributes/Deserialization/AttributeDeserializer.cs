using System;
using System.Xml.Linq;
using Castle.Windsor;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public static class AttributeDeserializer
    {
        private static WindsorContainer _container;
        private static bool _initialized;

        /// <summary>
        /// Deserializes an <see cref="XElement"/> into an appropriate implementation of <see cref="IAdsmlAttribute"/>.
        /// </summary>
        /// <param name="element">Required. The <see cref="XElement"/> to deserialize.</param>
        /// <returns>An <see cref="IAdsmlAttribute"/>.</returns>
        public static IAdsmlAttribute Deserialize(XElement element) {
            if (element == null) {
                throw new ArgumentNullException("element");
            }

            if (!_initialized) {
                Initialize();
            }

            var deserializer =
                _container.Resolve<IAdsmlAttributeDeserializer>(element.Name.LocalName);

            return deserializer.Deserialize(element);
        }

        /// <summary>
        /// Confugres the backing windsor container.
        /// </summary>
        private static void Initialize() {
            _container = new WindsorContainer();

            _container.Install(new DeserializationInstaller());

            _initialized = true;
        }
    }
}