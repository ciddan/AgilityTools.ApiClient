using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
  /// <summary>
  /// Allows the user to control the returned attributes.
  /// </summary>
  public class AttributeControl : ControlComponentBase<IAttributeControl>, ISearchControlComponent, ILookupControlComponent
  {
    internal AttributeControl(string nodeName) {
      this.OuterNodeAttributes = new List<XAttribute>();
      this.ContentNodes = new List<IAttributeControl>();
      this.NodeName = nodeName;
    }

    /// <summary>
    /// Constructor. Internal.
    /// </summary>
    /// <param name="attributesToReturn">A param array of <see cref="IAttributeControl"/> containing the attributes to return.</param>
    internal AttributeControl(params IAttributeControl[] attributesToReturn)
      : this("AttributesToReturn", attributesToReturn) {
    }

    internal AttributeControl(params int[] definitionIds)
      : this("AttributesToReturn", definitionIds) {
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

    internal AttributeControl(string nodeName = "AttributesToReturn", params int[] definitionIds) {
      if (definitionIds.Length == 0) {
        throw new ArgumentException("Parameter cannot be empty", "definitionIds");
      }
      string idList =
        definitionIds
          .Select(d => d.ToString(CultureInfo.InvariantCulture))
          .Aggregate((aggr, d) => aggr + ", " + d);

      this.OuterNodeAttributes = new List<XAttribute>() {
        new XAttribute("idlist", idList)
      };
      this.ContentNodes = new List<IAttributeControl>();
      this.NodeName = nodeName;
    }

    /// <summary>
    /// Adds attributes to the return list.
    /// </summary>
    /// <param name="attributesToReturn">A param array of <see cref="IAttributeControl"/> containing the attributes to return.</param>
    internal void AddAttributes(params IAttributeControl[] attributesToReturn) {
      if (this.ContentNodes == null) this.ContentNodes = new List<IAttributeControl>();
      foreach (var attributeControl in attributesToReturn) {
        this.ContentNodes.Add(attributeControl);
      }
    }
  }
}