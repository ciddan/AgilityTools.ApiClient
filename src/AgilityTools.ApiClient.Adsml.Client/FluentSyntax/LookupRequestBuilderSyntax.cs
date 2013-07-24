using System.ComponentModel;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using Autofac.Registrars;

namespace AgilityTools.ApiClient.Adsml.Client
{
  /// <summary>
  /// Defines available commands and command order for the <see cref="ILookupRequestBuilder"/>. 
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface ILookupRequestBuilder : IFluentInterface, IRequestBuilder<LookupRequest>, IContextLookup, IConfigLookupControlsLookupReturnNoAttributesLookup, IConfigLookupControlsLookup, IReturnNoAttributesLookup
  {
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IConfigLookupControlsLookupReturnNoAttributesLookup : IFluentInterface, IConfigLookupControlsLookup, IReturnNoAttributesLookup
  {
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IContextLookup
  {
    IConfigLookupControlsLookupReturnNoAttributesLookup Context(string name);
  }

  public interface IReturnNoAttributesLookup : IFluentInterface
  {
    IConfigLookupControlsLookup ReturnNoAttributes(bool returnNoAttributes = true);
  }

  [EditorBrowsable(EditorBrowsableState.Never)]
  public interface IConfigLookupControlsLookup
  {
    ILookupControlBuilder ConfigureLookupControls();
  }
}