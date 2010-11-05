using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class ReferenceControlComponent : ISearchControlComponent
    {
        public IList<XAttribute> OuterNodeAttributes { get; set; }

        public XElement ToApiXml() {
            throw new NotImplementedException();
        }
    }
}