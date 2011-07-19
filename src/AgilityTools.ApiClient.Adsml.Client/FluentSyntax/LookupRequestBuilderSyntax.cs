using System.ComponentModel;
using AgilityTools.ApiClient.Adsml.Client.Requests;

namespace AgilityTools.ApiClient.Adsml.Client
{
    /// <summary>
    /// Defines available commands and command order for the <see cref="ILookupRequestBuilder"/>. 
    /// </summary>
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