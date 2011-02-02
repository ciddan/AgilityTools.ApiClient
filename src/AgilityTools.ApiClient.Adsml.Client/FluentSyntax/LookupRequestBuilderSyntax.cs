using System.ComponentModel;
using AgilityTools.ApiClient.Adsml.Client.Requests;

namespace AgilityTools.ApiClient.Adsml.Client
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ILookupRequestBuilder : IFluentInterface, IName, IConfigLookupControlsLookup
    {
        LookupRequest Build();
    }

    public interface IName
    {
        IConfigLookupControlsLookup ContextName(string name);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IConfigLookupControlsLookup
    {
        ILookupControlBuilder ConfigureLookupControls();
    }
}