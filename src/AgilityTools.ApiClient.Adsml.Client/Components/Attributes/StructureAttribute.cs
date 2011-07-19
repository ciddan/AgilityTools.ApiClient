using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Represents an Adsml StructureAttribute. Implements <see cref="IAdsmlAttribute"/>.
    /// </summary>
    public class StructureAttribute : IAdsmlAttribute
    {
        public int DefinitionId { get; set; }
        public string Name { get; set; }
        public IList<StructureValue> Values { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
	    public StructureAttribute() : this(new List<StructureValue>()) {
	    }

        /// <summary>
        /// Overload.
        /// </summary>
        /// <param name="values">Optional. The values of the attribute.</param>
	    public StructureAttribute(IList<StructureValue> values){
		    this.Values = values;
	    }

        /// <summary>
        /// Overload.
        /// </summary>
        /// <param name="definitionId">The Agility definition id of the attribute</param>
        /// <param name="values">Optional. The values of the attribute.</param>
        public StructureAttribute(int definitionId, IList<StructureValue> values) 
            : this (null, definitionId, values) { }

        /// <summary>
        /// Overload.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="values">Optional. The values of the attribute.</param>
        public StructureAttribute(string name, IList<StructureValue> values) 
            : this (name, 0, values) { }

        /// <summary>
        /// Overload.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="definitionId">The Agility definition id of the attribute</param>
        /// <param name="values">Optional. The values of the attribute.</param>
        public StructureAttribute(string name, int definitionId, IList<StructureValue> values) {
            this.Name = name;
            this.DefinitionId = definitionId;
            this.Values = values;
        }

        /// <summary>
        /// Gets the name of the attribute.
        /// </summary>
        /// <returns><see cref="string"/></returns>
        public string GetName() {
            return this.Name;
        }

        /// <summary>
        /// Gets the all the attribute values.
        /// </summary>
        /// <returns><see cref="IEnumerable{T}"/> of <see cref="AttributeValue"/>.</returns>
        public IEnumerable<AttributeValue> GetValues() {
            return this.Values.Select(val => new AttributeValue {LanguageId = val.LanguageId, Value = val.Value});
        }

        /// <summary>
        /// Returns a boolean indicating if the attribute holds any values.
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public bool HasValues() {
            return this.Values.Count > 0;
        }

        /// <summary>
        /// Converts the attribute to Admsl.
        /// </summary>
        /// <returns>An <see cref="XElement"/> containing the adsml representation of the attribute.</returns>
        public XElement ToAdsml() {
		    this.Validate();

	        var element = new XElement("StructureAttribute");

            if (this.DefinitionId != 0)
                element.Add(new XAttribute("id", this.DefinitionId));

            if (!string.IsNullOrEmpty(this.Name))
                element.Add(new XAttribute("name", this.Name));

            if (this.Values != null && this.Values.Count() >= 1)
                element.Add(this.Values.Select(val => val.ToAdsml()));

	        return element;
	    }

        /// <summary>
        /// Validates the attribute.
        /// </summary>
        /// <exception cref="ApiSerializationValidationException">Thrown if the validation fails.</exception>
        private void Validate() {
		    if (this.DefinitionId == 0 && string.IsNullOrEmpty(this.Name))
                throw new ApiSerializationValidationException("DefinitionId or Name has to be set.");
	    }

        /// <summary>
        /// Factory method for instantiating a new instance of a <see cref="StructureAttribute"/> with the provided parameters set.
        /// </summary>
        /// <param name="name">The name of the attribute</param>
        /// <param name="values">The values of the attribute.</param>
        /// <returns><see cref="StructureAttribute"/></returns>
        public static StructureAttribute New(string name, params StructureValue[] values) {
            return new StructureAttribute(name, values);
        }

        /// <summary>
        /// Factory method for instantiating a new instance of a <see cref="StructureAttribute"/> with the provided parameters set.
        /// </summary>
        /// <param name="definitionId">The definitionId of the attribute</param>
        /// <param name="values">The values of the attribute.</param>
        /// <returns><see cref="StructureAttribute"/></returns>
        public static StructureAttribute New(int definitionId, params StructureValue[] values) {
            return new StructureAttribute(definitionId, values);
        }

        /// <summary>
        /// Factory method for instantiating a new instance of a <see cref="StructureAttribute"/> with the provided parameters set.
        /// </summary>
        /// <param name="name">The name of the attribute</param>
        /// <param name="definitionId">The definitionId of the attribute</param>
        /// <param name="values">The values of the attribute.</param>
        /// <returns><see cref="StructureAttribute"/></returns>
        public static StructureAttribute New(string name, int definitionId, params StructureValue[] values) {
            return new StructureAttribute(name, definitionId, values);
        }
    }
}