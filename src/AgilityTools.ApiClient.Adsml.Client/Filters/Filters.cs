using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client
{
  /// <summary>
  /// A static class that containing static factory methods for creating various objects that implement any of the filter interfaces.
  /// </summary>
  public static class Filter
  {
    /// <summary>
    /// Used to exclude all results from a SearchRequest that are located in the bin.
    /// </summary>
    /// <param name="excludeBin">Optional. Defaults to true.</param>
    /// <returns><see cref="ExcludeBinFilter"/>.</returns>
    public static ExcludeBinFilter ExcludeBin(bool excludeBin = true) {
      return new ExcludeBinFilter(excludeBin);
    }

    /// <summary>
    /// Used to exclude all results from a SearchRequest that are located in the DOCUMENT-"folder" for the structure.
    /// </summary>
    /// <param name="excludeDoc">Optional. Defaults to true.</param>
    /// <returns><see cref="ExcludeDocumentFilter"/>.</returns>
    public static ExcludeDocumentFilter ExcludeDocument(bool excludeDoc = true) {
      return new ExcludeDocumentFilter(excludeDoc);
    }

    /// <summary>
    /// Configures the API to only return basic attributes (name, id) for any returned contexts.
    /// </summary>
    /// <param name="returnAllAttributes">Optional. Defaults to false.</param>
    /// <returns><see cref="ReturnAllAttributesFilter"/>.</returns>
    public static ReturnAllAttributesFilter ReturnAllAttributes(bool returnAllAttributes = false) {
      return new ReturnAllAttributesFilter(returnAllAttributes);
    }

    public static ResolveContextReferencesFilter ResolveContextReferences(bool resolveContextReferences = true) {
      return new ResolveContextReferencesFilter(resolveContextReferences);
    }

    public static ReturnAvailableAttributesFilter ReturnAvailableAttributes(bool returnAvailableAttribute = false) {
      return new ReturnAvailableAttributesFilter(returnAvailableAttribute);
    }

    public static ExpandLanguageIndependentAttributesFilter ExpandLanguageIndependentAttributes(bool expandLanguageIndependentAttributes = false) {
      return new ExpandLanguageIndependentAttributesFilter(expandLanguageIndependentAttributes);
    }

    /// <summary>
    /// Configures the API to only return basic attributes (name, id) for any returned contexts.
    /// </summary>
    /// <param name="returnNoAttributes">Optional. Defaults to true.</param>
    /// <returns><see cref="ReturnAllAttributesFilter"/>.</returns>
    public static ReturnNoAttributesFilter ReturnNoAttributes(bool returnNoAttributes = true) {
      return new ReturnNoAttributesFilter(returnNoAttributes);
    }

    /// <summary>
    /// Configures the API to page its results. Should be used in conjunction with the <see cref="PageSizeFilter"/>.
    /// </summary>
    /// <param name="allowPaging">Optional. Defaults to true.</param>
    /// <returns><see cref="AllowPagingFilter"/>.</returns>
    public static AllowPagingFilter AllowPaging(bool allowPaging = true) {
      return new AllowPagingFilter(allowPaging);
    }

    /// <summary>
    /// Tells the API how many results should be included per page in a paged query.
    /// </summary>
    /// <param name="pageSize">The number of results per "page".</param>
    /// <returns><see cref="PageSizeFilter"/>.</returns>
    public static PageSizeFilter PageSize(int pageSize) {
      return new PageSizeFilter(pageSize);
    }

    /// <summary>
    /// Caps the number of results from the query to the provided number.
    /// </summary>
    /// <param name="countLimit">The result count limit.</param>
    /// <returns><see cref="CountLimitFilter"/>.</returns>
    public static CountLimitFilter CountLimit(int countLimit) {
      return new CountLimitFilter(countLimit);
    }

    /// <summary>
    /// Configures the API to return an error if a ModificationOperation fails.
    /// </summary>
    /// <param name="failOnError">Optional. Defaults to true.</param>
    /// <returns><see cref="FailOnErrorFilter"/>.</returns>
    public static FailOnErrorFilter FailOnError(bool failOnError = true) {
      return new FailOnErrorFilter(failOnError);
    }

    /// <summary>
    /// Configures the API to return relations as attributes. That representation of the relation is closer to how relations are persisted in the Agility database.
    /// </summary>
    /// <param name="returnAsAttributes">Optional. Defaults to true.</param>
    /// <returns><see cref="ReturnRelationsAsAttributesFilter"/>.</returns>
    public static ReturnRelationsAsAttributesFilter ReturnRelationsAsAttributes(bool returnAsAttributes = true) {
      return new ReturnRelationsAsAttributesFilter(returnAsAttributes);
    }

    /// <summary>
    /// Tells the API whether to use data from the local or global attribute instances when copying a context.
    /// </summary>
    /// <param name="copyLocalFromGlobal">Optional. Defaults to true.</param>
    /// <returns><see cref="CopyLocalAttributesFromSourceFilter"/>.</returns>
    public static CopyLocalAttributesFromSourceFilter CopyLocalAttributesFromSource(bool copyLocalFromGlobal = true) {
      return new CopyLocalAttributesFromSourceFilter(copyLocalFromGlobal);
    }
  }

  public class ReturnNoAttributesFilter : ISearchRequestFilter, ICreateRequestFilter, IModifyRequestFilter, ILinkRequestFilter
  {
    private readonly bool _returnNoAttributes;

    public ReturnNoAttributesFilter(bool returnNoAttributes)
    {
      _returnNoAttributes = returnNoAttributes;
    }

    public XAttribute ToAdsml()
    {
      return new XAttribute("returnNoAttributes", _returnNoAttributes);
    }
  }

  public class ReturnAllAttributesFilter : ISearchControlFilter
  {
    private readonly bool _returnAllAttributes;

    public ReturnAllAttributesFilter(bool returnAllAttributes)
    {
      _returnAllAttributes = returnAllAttributes;
    }

    public XAttribute ToAdsml()
    {
      return new XAttribute("returnAllAttributes", _returnAllAttributes);
    }
  }

  public class FailOnErrorFilter : ICreateRequestFilter, IModifyRequestFilter
  {
    private readonly bool _failOnError;

    public FailOnErrorFilter(bool failOnError) {
      _failOnError = failOnError;
    }

    public XAttribute ToAdsml() {
      return new XAttribute("failOnError", _failOnError);
    }
  }

  public class AllowPagingFilter : ISearchControlFilter
  {
    private readonly bool _allowPaging;

    public AllowPagingFilter(bool allowPaging) {
      _allowPaging = allowPaging;
    }

    public XAttribute ToAdsml() {
      return new XAttribute("allowPaging", _allowPaging);
    }
  }

  public class PageSizeFilter : ISearchControlFilter
  {
    private readonly int _pageSize;

    public PageSizeFilter(int pageSize) {
      _pageSize = pageSize;
    }

    public XAttribute ToAdsml() {
      this.Validate();
      return new XAttribute("pageSize", _pageSize);
    }

    private void Validate() {
      if (this._pageSize <= 0)
        throw new ApiSerializationValidationException("PageSize must be larger than 0.");
    }
  }

  public class CountLimitFilter : ISearchControlFilter
  {
    private readonly int _countLimit;

    public CountLimitFilter(int countLimit) {
      _countLimit = countLimit;
    }

    public XAttribute ToAdsml() {
      this.Validate();
      return new XAttribute("countLimit", _countLimit);
    }

    private void Validate() {
      if (this._countLimit <= 0)
        throw new ApiSerializationValidationException("countLimit must be larger than 0.");
    }
  }

  public class ExcludeBinFilter : ISearchControlFilter
  {
    private readonly bool _excludeBin;

    public ExcludeBinFilter(bool excludeBin) {
      _excludeBin = excludeBin;
    }

    public XAttribute ToAdsml() {
      return new XAttribute("excludeBin", _excludeBin);
    }
  }

  public class ExcludeDocumentFilter : ISearchControlFilter
  {
    private readonly bool _excludeDocument;

    public ExcludeDocumentFilter(bool excludeDocument) {
      _excludeDocument = excludeDocument;
    }

    public XAttribute ToAdsml() {
      return new XAttribute("excludeDocument", _excludeDocument);
    }
  }

  public class ReturnRelationsAsAttributesFilter : ILookupControlFilter
  {
    private readonly bool _returnAsAttributes;

    public ReturnRelationsAsAttributesFilter(bool returnAsAttributes) {
      _returnAsAttributes = returnAsAttributes;
    }

    public XAttribute ToAdsml() {
      return new XAttribute("returnRelationsAsAttributes", _returnAsAttributes);
    }
  }

  public class ResolveContextReferencesFilter : ILookupControlFilter
  {
    private readonly bool _resolveContextReferences;

    public ResolveContextReferencesFilter(bool returnAsAtresolveContextReferencestributes) {
      _resolveContextReferences = returnAsAtresolveContextReferencestributes;
    }

    public XAttribute ToAdsml() {
      return new XAttribute("returnAsAtresolveContextReferencestributes", _resolveContextReferences);
    }
  }

  public class ReturnAvailableAttributesFilter : ILookupControlFilter
  {
    private readonly bool _returnAvailableAttributes;

    public ReturnAvailableAttributesFilter(bool returnAvailableAttributes)
    {
      _returnAvailableAttributes = returnAvailableAttributes;
    }

    public XAttribute ToAdsml()
    {
      return new XAttribute("returnAvailableAttributes", _returnAvailableAttributes);
    }
  }

  public class ExpandLanguageIndependentAttributesFilter : ILookupControlFilter
  {
    private readonly bool _expandLanguageIndependentAttributes;

    public ExpandLanguageIndependentAttributesFilter(bool expandLanguageIndependentAttributes) {
      _expandLanguageIndependentAttributes = expandLanguageIndependentAttributes;
    }

    public XAttribute ToAdsml() {
      return new XAttribute("expandLanguageIndependentAttributes", _expandLanguageIndependentAttributes);
    }
  }

  public class CopyLocalAttributesFromSourceFilter : ICopyControlFilter
  {
    private readonly bool _copyFromSource;

    public CopyLocalAttributesFromSourceFilter(bool copyFromSource) {
      _copyFromSource = copyFromSource;
    }

    public XAttribute ToAdsml() {
      return new XAttribute("copyLocalAttributes", _copyFromSource);
    }
  }
}