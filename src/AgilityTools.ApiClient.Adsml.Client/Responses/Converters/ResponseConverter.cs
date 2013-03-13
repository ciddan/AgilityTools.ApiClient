﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
  public abstract class ResponseConverter<TOutput> : IResponseConverter<XElement, TOutput> where TOutput : class, new()
  {
    protected readonly string _validationDocument;
    protected readonly string _responseNodeName;

    protected ResponseConverter(string validationDocument, string responseNodeName) {
      _validationDocument = validationDocument;
      _responseNodeName = responseNodeName;
    }

    /// <summary>
    /// Converts the input of XElement to <typeparam name="TOutput"></typeparam>.
    /// </summary>
    /// <param name="source">Required. The data to convert.</param>
    /// <returns>An <see cref="IEnumerable{TOutput}"/> containing the converted results.</returns>
    public virtual IEnumerable<TOutput> Convert(XElement source) {
      CheckResponse(source);
      
      var results = source.Descendants().Where(d => d.Name.LocalName == _responseNodeName);
      return results.Select(ConvertSingle);
    }

    /// <summary>
    /// Used to convert an <see cref="XElement"/> source into a single <typeparamref name="TOutput"/>.
    /// </summary>
    /// <param name="source">Required. The data to convert.</param>
    /// <returns>The result of the conversion of type <typeparamref name="TOutput"/>.</returns>
    protected abstract TOutput ConvertSingle(XElement source);

    /// <summary>
    /// Checks to see whether the response is of the correct type for the converter.
    /// </summary>
    /// <param name="source">Required. The reponse to check for syntax validity.</param>
    /// <exception cref="ArgumentNullException">Thrown if <param name="source"></param> is null.</exception>
    protected void CheckResponse(XElement source) {
      if (source == null) throw new ArgumentNullException("source");

      if (source.Elements().Any(e => e.Name.LocalName.ToString(CultureInfo.InvariantCulture) != _responseNodeName))
        throw new InvalidOperationException(string.Format("Not a valid {0}.\r\nResponse:\r\n{1}", _responseNodeName, source));
    }
  }
}