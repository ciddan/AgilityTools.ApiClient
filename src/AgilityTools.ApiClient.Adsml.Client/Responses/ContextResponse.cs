using System.Collections.Generic;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components.Attributes;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public class ContextResponse 
    {
        public string Name { get; set; }
        public string IdPath { get; set; }
        public string SortPath { get; set; }
        public IList<IAdsmlAttribute> Attributes { get; set; }

        public ContextResponse() {
            this.Attributes = new List<IAdsmlAttribute>();
        }

        public ContextResponse(string name, string idPath, string sortPath, IList<IAdsmlAttribute> attributes = null) {
            this.Name = name;
            this.IdPath = idPath;
            this.SortPath = sortPath;
            
            this.Attributes = attributes ?? new List<IAdsmlAttribute>();
        }
    }
}