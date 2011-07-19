using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Specifies the minimal functionality required for attribute types. Implements <see cref="IAdsmlSerializable{XElement}"/>.
    /// </summary>
    public interface IAdsmlAttribute : IAdsmlSerializable<XElement>
    {
        string GetName();
        IEnumerable<AttributeValue> GetValues();
        bool HasValues();
    }
}