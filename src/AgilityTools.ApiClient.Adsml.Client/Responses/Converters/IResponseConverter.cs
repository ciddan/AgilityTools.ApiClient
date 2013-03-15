using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
  public interface IResponseConverter<in TInput, out TOutput>
    where TInput : XObject
    where TOutput : class, new() 
  {
    /// <summary>
    /// Converts the input of {TInput} to {TOutput}.
    /// </summary>
    /// <param name="source">Required. The data to convert.</param>
    /// <returns>An <see cref="IEnumerable{TOutput}"/> containing the converted results.</returns>
    IEnumerable<TOutput> Convert(TInput source);
  }
}