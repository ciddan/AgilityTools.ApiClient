using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public interface IResponseConverter<in TInput, out TOutput> where TInput : XObject
                                                                where TOutput : class
    {
        TOutput Convert(TInput source);
    }
}