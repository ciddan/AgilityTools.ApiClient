namespace AgilityTools.ApiClient.Adsml.Client.FluentSyntax
{
    public interface ILinkRequestBuilder : IFluentInterface,
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