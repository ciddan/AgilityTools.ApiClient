using System.Collections.Generic;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Responses
{
    public abstract class ResponseConverter<TInput, TOutput>  : IResponseConverter<TInput, TOutput> where TOutput : class where TInput : XObject
    {
        protected readonly string _validationDocument;

        protected ResponseConverter(string validationDocument) {
            _validationDocument = validationDocument;
        }

        /// <summary>
        /// Converts the input of {TInput} to {TOutput}.
        /// </summary>
        /// <param name="source">Required. The data to convert.</param>
        /// <returns>An <see cref="IEnumerable{TOutput}"/> containing the converted results.</returns>
        public abstract IEnumerable<TOutput> Convert(TInput source);
    }
}