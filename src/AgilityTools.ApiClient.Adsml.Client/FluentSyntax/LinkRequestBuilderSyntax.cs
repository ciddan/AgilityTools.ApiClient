using Autofac.Registrars;
using AgilityTools.ApiClient.Adsml.Client.Requests;

namespace AgilityTools.ApiClient.Adsml.Client
{
  /// <summary>
  /// Defines available commands and command order for the <see cref="ILinkRequestBuilder"/>. 
  /// </summary>
  public interface ILinkRequestBuilder : IFluentInterface,
                                         IRequestBuilder<LinkRequest>,
                                         ILinkRequestTargetPathReturnNoAttributesConfigureCopyControls,
                                         ILinkRequestReturnNoAttributesConfigureCopyControls,
                                         ILinkRequestConfigureCopyControls { }
  
  public interface ILinkRequestTargetPathReturnNoAttributesConfigureCopyControls : IFluentInterface,
                                                                                   ILinkRequestTargetPath,
                                                                                   ILinkRequestReturnNoAttributes,
                                                                                   ILinkRequestConfigureCopyControls { }
  
  public interface ILinkRequestReturnNoAttributesConfigureCopyControls : IFluentInterface,
                                                                         ILinkRequestReturnNoAttributes,
                                                                         ILinkRequestConfigureCopyControls { }
  
  public interface ILinkRequestSourceContext : IFluentInterface
  {
    ILinkRequestTargetPathReturnNoAttributesConfigureCopyControls SourceContext(string sourcePath);
  }
  
  public interface ILinkRequestTargetPath : IFluentInterface
  {
    ILinkRequestReturnNoAttributesConfigureCopyControls TargetPath(string targetPath);
  }
  
  public interface ILinkRequestReturnNoAttributes : IFluentInterface
  {
    ILinkRequestConfigureCopyControls ReturnNoAttributes();
  }
  
  public interface ILinkRequestConfigureCopyControls
  {
    ICopyControlBuilder ConfigureCopyControls();
  }
}