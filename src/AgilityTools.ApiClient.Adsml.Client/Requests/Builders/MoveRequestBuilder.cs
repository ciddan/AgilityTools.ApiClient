using System.Collections.Generic;
using AgilityTools.ApiClient.Adsml.Client.Components;

namespace AgilityTools.ApiClient.Adsml.Client.Requests
{
  public class MoveRequestBuilder : IMoveRequestBuilder
  {
    internal IList<IMoveRequestFilter> RequestFilters { get; private set; }
    internal CopyControlBuilder CopyControlBuilder { get; private set; }
    internal string Source { get; private set; }
    internal string Target { get; private set; }

    public MoveRequestBuilder() {
      this.RequestFilters = new List<IMoveRequestFilter>();
    }

    public IMoveRequestTargetPathReturnNoAttributesConfigureCopyControls SourceContext(string sourcePath) {
      this.Source = sourcePath;

      return this;
    }

    public IMoveRequestReturnNoAttributesConfigureCopyControls TargetPath(string targetPath) {
      this.Target = targetPath;

      return this;
    }

    public IMoveRequestConfigureCopyControls ReturnNoAttributes() {
      this.RequestFilters.Add(Filter.ReturnNoAttributes());

      return this;
    }

    public ICopyControlBuilder ConfigureCopyControls() {
      this.CopyControlBuilder = new CopyControlBuilder();

      return this.CopyControlBuilder;
    }

    public MoveRequest Build() {
      this.Validate();

      var moveRequest = new MoveRequest(this.Source, this.Target) {
        RequestFilters = new List<IMoveRequestFilter>(this.RequestFilters)
      };

      if (this.CopyControlBuilder != null)
        moveRequest.CopyControl = this.CopyControlBuilder.Build();

      return moveRequest;
    }

    private void Validate() {
      if (Source.IsNullOrEmpty()) {
        throw new ApiSerializationValidationException("A Source context must be specified.");
      }

      if (Target.IsNullOrEmpty()) {
        throw new ApiSerializationValidationException("A Target path must be specified.");
      }
    }
  }
}