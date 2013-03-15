using System;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
  public interface IRequestBuilder<TRequest> where TRequest : IAdsmlSerializable<XElement>
  {
    TRequest Build();
  }
}
