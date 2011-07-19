using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Allows the user to control the returned attributes.
    /// </summary>
    public class AttributeControl : ControlComponentBase<IAttributeControl>, ISearchControlComponent, ILookupControlComponent
    {
        /// <summary>
        /// Constructor. Internal.
        /// </summary>
        /// <param name="attributesToReturn">A param array of <see cref="IAttributeControl"/> containing the attributes to return.</param>
        internal AttributeControl(params IAttributeControl[] attributesToReturn)
            : this("AttributesToReturn", attributesToReturn) {
        }

        /// <summary>
        /// Constructor. Internal.
        /// </summary>
        /// <param name="nodeName">Optional. The name of the wrapping xml tag. Defaults to AttributesToReturn.</param>
        /// <param name="attributesToReturn">A param array of <see cref="IAttributeControl"/> containing the attributes to return.</param>
        internal AttributeControl(string nodeName = "AttributesToReturn", params IAttributeControl[] attributesToReturn) {
            if (attributesToReturn == null)
                throw new ArgumentNullException("attributesToReturn");

            this.OuterNodeAttributes = new List<XAttribute>();
            this.ContentNodes = new List<IAttributeControl>(attributesToReturn);
            this.NodeName = nodeName;
        }

        /// <summary>
        /// Adds attributes to the return list.
        /// </summary>
        /// <param name="attributesToReturn">A param array of <see cref="IAttributeControl"/> containing the attributes to return.</param>
        internal void AddAttributes(params IAttributeControl[] attributesToReturn) {
            this.ContentNodes = new List<IAttributeControl>(attributesToReturn);
        }
    }
}