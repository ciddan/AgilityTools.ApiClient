using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public class ModificationItem<TAttribute> where TAttribute : IAdsmlAttribute<XElement>
    {
        internal Modifications ModificationType { get; private set; }
        internal TAttribute AttributeToSet { get; private set; }

        public void AddMod(Modifications operation, Func<TAttribute> attributeFactory) {
            this.ModificationType = operation;

            AttributeToSet = attributeFactory.Invoke();
        }
    }

    public enum Modifications
    {
        [StringValue("replaceAttribute")]
        ReplaceAttribute = 1
    }
}