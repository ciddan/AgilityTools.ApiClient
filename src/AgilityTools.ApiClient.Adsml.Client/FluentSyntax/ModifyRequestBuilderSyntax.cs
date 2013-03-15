using System;
using System.Collections.Generic;
using System.ComponentModel;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using Autofac.Registrars;

namespace AgilityTools.ApiClient.Adsml.Client
{
    /// <summary>
    /// Defines available commands and command order for the <see cref="IModifyRequestBuilder"/>. 
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IModifyRequestBuilder : IFluentInterface,
                                             IRequestBuilder<ModifyRequest>,
                                             IReturnNoAttributesFailOnErrorAddModificationsConfigLookupControls,
                                             IFailOnErrorAddModificationsConfigLookupControls,
                                             IAddModificationsConfigLookupControls,
                                             IModifyContext,
                                             IConfigLookupControlsModify,
                                             IFailOnErrorModify,
                                             IReturnNoAttributesModify
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReturnNoAttributesFailOnErrorAddModificationsConfigLookupControls : IFluentInterface,
                                                                                          IReturnNoAttributesModify,
                                                                                          IFailOnErrorModify,
                                                                                          IAddModification,
                                                                                          IConfigLookupControlsModify
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFailOnErrorAddModificationsConfigLookupControls : IFluentInterface,
                                                                        IReturnNoAttributesModify,
                                                                        IFailOnErrorModify,
                                                                        IAddModification,
                                                                        IConfigLookupControlsModify
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IAddModificationsConfigLookupControls : IFluentInterface,
                                                             IReturnNoAttributesModify,
                                                             IFailOnErrorModify,
                                                             IAddModification,
                                                             IConfigLookupControlsModify
    {
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IModifyContext : IFluentInterface
    {
        IReturnNoAttributesFailOnErrorAddModificationsConfigLookupControls Context(string contextToModify);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IReturnNoAttributesModify : IFluentInterface
    {
        IFailOnErrorAddModificationsConfigLookupControls ReturnNoAttributes();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IFailOnErrorModify
    {
        IAddModificationsConfigLookupControls FailOnError();
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IAddModification : IFluentInterface
    {
        IAddModificationsConfigLookupControls AddModification<TAttribute>(Modifications modificationType, TAttribute attribute)
            where TAttribute : class, IAdsmlAttribute;

        IAddModificationsConfigLookupControls AddModifications(Func<IList<ModificationItem>> modificationsFactory);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    public interface IConfigLookupControlsModify
    {
        ILookupControlBuilder ConfigureLookupControls();
    }
}