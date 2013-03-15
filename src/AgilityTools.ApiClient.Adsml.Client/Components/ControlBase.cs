using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Abstract base implementation of <see cref="IControlComponent"/>. Used for simpler container types.
    /// </summary>
    public abstract class ControlBase : IControlComponent
    {
        private XElement Request { get; set; }
        
        protected string NodeName { get; set; }
        protected IList<IRequestFilter> RequestFilters { get; set; }
        protected IList<IControlComponent> Components { get; set; }

        /// <summary>
        /// Contains any optional extra xml attributes that should be applied to the outer node.
        /// </summary>
        public IList<XAttribute> OuterNodeAttributes { get; set; }

        /// <summary>
        /// Serializes the object to ADSML xml form.
        /// </summary>
        /// <returns><see cref="XElement"/></returns>
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

        /// <summary>
        /// Adds the specified control filters to the resulting request.
        /// </summary>
        private void ApplyFilters() {
            if (this.RequestFilters != null) {
                this.Request.Add(this.RequestFilters.Select(filter => filter.ToAdsml()));
            }
        }
    }
}