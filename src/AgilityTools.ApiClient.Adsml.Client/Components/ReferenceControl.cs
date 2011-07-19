using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Represents a ReferenceControl ADSML xml block. Used to configure how the api handles attribute- and special character references.
    /// </summary>
    public class ReferenceControl : ControlComponentBase<IReferenceOptions>, ISearchControlComponent, ILookupControlComponent 
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="referenceOptions">A params array of <see cref="IReferenceOptions"/> configuring the ReferenceControl.</param>
        internal ReferenceControl(params IReferenceOptions[] referenceOptions) {
            if (referenceOptions == null)
                throw new ArgumentNullException("referenceOptions");

            this.OuterNodeAttributes = new List<XAttribute>();
            this.ContentNodes = new List<IReferenceOptions>(referenceOptions);
            this.NodeName = "ReferenceControls";
        }
    }
}