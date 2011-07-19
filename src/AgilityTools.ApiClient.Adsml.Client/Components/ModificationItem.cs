using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Represents an ADSML ModificationItem block. Used in ModifyRequests.
    /// </summary>
    public class ModificationItem : IAdsmlSerializable<XElement>
    {
        internal Modifications ModificationType { get; private set; }
        internal IAdsmlAttribute AttributeToModify { get; private set; }

        /// <summary>
        /// Factory method for instantiating a new <see cref="ModificationItem"/> with the specified parameters.
        /// </summary>
        /// <typeparam name="TAttribute">Required. The type of the attribute that is the target of the modification.</typeparam>
        /// <param name="operation">Required. A <see cref="Modifications"/>.</param>
        /// <param name="attribute">Required. The attribute instance that represents the final state of the attribute post modification.</param>
        /// <returns><see cref="ModificationItem"/>.</returns>
        public static ModificationItem New<TAttribute>(Modifications operation, TAttribute attribute)
            where TAttribute : class, IAdsmlAttribute {

            return new ModificationItem {ModificationType = operation, AttributeToModify = attribute};
        }

        /// <summary>
        /// Serializes the object into ADSML xml.
        /// </summary>
        /// <returns><see cref="XElement"/></returns>
        public XElement ToAdsml() {
            this.Validate();

            var xml = new XElement("ModificationItem",
                        new XAttribute("operation", ModificationType.GetStringValue()),
                        new XElement("AttributeDetails", AttributeToModify.ToAdsml()));

            return xml;
        }

        /// <summary>
        /// Validated the object state.
        /// </summary>
        /// <exception cref="ApiSerializationValidationException">Thrown if AttributeToModify has not been set.</exception>
        private void Validate() {
            if (this.AttributeToModify == null)
                throw new ApiSerializationValidationException("You must specify an attribute to modify.");
        }
    }

    /// <summary>
    /// Enum containing the various operation modes for attribute modification. The string value contains the adsml.xsd mandated name of the operation.
    /// </summary>
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