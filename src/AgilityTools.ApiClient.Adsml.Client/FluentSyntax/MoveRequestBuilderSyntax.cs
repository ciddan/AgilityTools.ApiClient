using Autofac.Registrars;
using AgilityTools.ApiClient.Adsml.Client.Requests;

namespace AgilityTools.ApiClient.Adsml.Client
{
  /// <summary>
  /// Defines available commands and command order for the <see cref="IMoveRequestBuilder"/>. 
  /// </summary>
  public interface IMoveRequestBuilder : IFluentInterface,
  IRequestBuilder<MoveRequest>,
  IMoveRequestTargetPathReturnNoAttributesConfigureCopyControls,
  IMoveRequestReturnNoAttributesConfigureCopyControls,
  IMoveRequestConfigureCopyControls { }

  public interface IMoveRequestTargetPathReturnNoAttributesConfigureCopyControls : IFluentInterface,
  IMoveRequestTargetPath,
  IMoveRequestReturnNoAttributes,
  IMoveRequestConfigureCopyControls { }

  public interface IMoveRequestReturnNoAttributesConfigureCopyControls : IFluentInterface,
  IMoveRequestReturnNoAttributes,
  IMoveRequestConfigureCopyControls { }

  public interface IMoveRequestSourceContext : IFluentInterface
  {
    IMoveRequestTargetPathReturnNoAttributesConfigureCopyControls SourceContext(string sourcePath);
  }

  public interface IMoveRequestTargetPath : IFluentInterface
  {
    IMoveRequestReturnNoAttributesConfigureCopyControls TargetPath(string targetPath);
  }

  public interface IMoveRequestReturnNoAttributes : IFluentInterface
  {
    IMoveRequestConfigureCopyControls ReturnNoAttributes();
  }

  public interface IMoveRequestConfigureCopyControls
  {
    ICopyControlBuilder ConfigureCopyControls();
  }
}