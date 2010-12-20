using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;
using AgilityTools.ApiClient.Adsml.Client.Helpers;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public class ModificationItem : IAdsmlSerializable<XElement>
    {
        internal Modifications ModificationType { get; private set; }
        internal IAdsmlAttribute AttributeToModify { get; private set; }

        public static ModificationItem New<TAttribute>(Modifications operation, TAttribute attribute)
            where TAttribute : class, IAdsmlAttribute {

            return new ModificationItem {ModificationType = operation, AttributeToModify = attribute};
        }

        public XElement ToAdsml() {
            this.Validate();

            var xml = new XElement("ModificationItem",
                        new XAttribute("operation", ModificationType.GetStringValue()),
                        new XElement("AttributeDetails", AttributeToModify.ToAdsml()));

            return xml;
        }

        private void Validate() {
            if (this.AttributeToModify == null)
                throw new ApiSerializationValidationException("You must specify an attribute to modify.");
        }
    }

    public enum Modifications
    {
        [StringValue("addAttribute")] 
        AddAttribute = 1,
        [StringValue("replaceAttribute")] 
        ReplaceAttribute = 2,
        [StringValue("removeAttribute")] 
        RemoveAttribute = 3,
        [StringValue("addValue")] 
        AddValue = 4,
        [StringValue("replaceValue")] 
        ReplaceValue = 5,
        [StringValue("removeValue")] 
        RemoveValue = 6,
    }
}