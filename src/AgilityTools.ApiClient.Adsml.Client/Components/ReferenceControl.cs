using System;
using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class ReferenceControl : SearchControlComponentBase<IReferenceOptions>
    {
        internal ReferenceControl(params IReferenceOptions[] referenceOptions) {
            if (referenceOptions == null)
                throw new ArgumentNullException("referenceOptions");

            this.ContentNodes = new List<IReferenceOptions>(referenceOptions);
            this.NodeName = "ReferenceControls";
        }
    }
}