using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public abstract class ControlBase : IControlComponent
    {
        private XElement Request { get; set; }
        
        protected string NodeName { get; set; }
        protected IList<IRequestFilter> RequestFilters { get; set; }
        protected IList<IControlComponent> Components { get; set; }

        public IList<XAttribute> OuterNodeAttributes { get; set; }

        public XElement ToAdsml() {
            this.Request = new XElement(this.NodeName);

            this.ApplyFilters();

            if (this.Components != null) {
                foreach (var controlComponent in this.Components) {
                    this.Request.Add(controlComponent.ToAdsml());
                }
            }

            return this.Request;
        }

        private void ApplyFilters() {
            if (this.RequestFilters != null) {
                this.Request.Add(this.RequestFilters.Select(filter => filter.ToAdsml()));
            }
        }
    }
}