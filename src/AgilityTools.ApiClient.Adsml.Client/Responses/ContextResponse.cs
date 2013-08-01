using System.Collections.Generic;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    /// <summary>
    /// Used to represent an ContextResponse. Extends <see cref="AdsmlResult"/>.
    /// </summary>
    public class ContextResponse 
    {
        public string Name { get; set; }
        public string IdPath { get; set; }
        public string SortPath { get; set; }
        public IList<IAdsmlAttribute> Attributes { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public ContextResponse() {
            this.Attributes = new List<IAdsmlAttribute>();
        }

        /// <summary>
        /// Overload.
        /// </summary>
        /// <param name="name">Optional. The full path to and name of the context.<example>/Structures/Classification/Foo/000111 Product</example></param>
        /// <param name="idPath">Optional. The id path to the context.</param>
        /// <param name="sortPath">Optional.</param>
        /// <param name="attributes">Optional. An <see cref="IList{T}"/> of <see cref="IAdsmlAttribute"/> containing the attributes associated with the context.</param>
        public ContextResponse(string name, string idPath, string sortPath, IList<IAdsmlAttribute> attributes = null) {
            this.Name = name;
            this.IdPath = idPath;
            this.SortPath = sortPath;
            
            this.Attributes = attributes ?? new List<IAdsmlAttribute>();
        }
    }
}