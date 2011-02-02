using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public interface IResponseConverter<in TInput, out TOutput> where TInput : XObject
                                                                where TOutput : class
    {
        IEnumerable<TOutput> Convert(TInput source);
    }
}