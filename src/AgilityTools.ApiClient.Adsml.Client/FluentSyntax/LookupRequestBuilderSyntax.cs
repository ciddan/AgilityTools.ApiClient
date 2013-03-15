using System.ComponentModel;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using Autofac.Registrars;

namespace AgilityTools.ApiClient.Adsml.Client
{
    /// <summary>
    /// Defines available commands and command order for the <see cref="ILookupRequestBuilder"/>. 
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface ILookupRequestBuilder : IFluentInterface, IRequestBuilder<LookupRequest>, IName, IConfigLookupControlsLookup
    {
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