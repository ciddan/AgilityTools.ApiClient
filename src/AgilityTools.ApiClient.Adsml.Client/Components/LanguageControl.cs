using System;
using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    /// <summary>
    /// Represents a LanguagesToReturn ADSML xml block. Used to restrict the number of returned languages. Decorated with <see cref="ISearchControlComponent"/> and <see cref="ILookupControlComponent"/> to make it available in the right context.
    /// </summary>
    public class LanguageControl : ControlComponentBase<IReturnedLanguageControl>, ISearchControlComponent, ILookupControlComponent
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="languagesToReturn">A params array of <see cref="IReturnedLanguageControl"/>. The specified languages get added to the control.</param>
        internal LanguageControl(params IReturnedLanguageControl[] languagesToReturn) {
            if (languagesToReturn == null)
                throw new ArgumentNullException("languagesToReturn");

            this.ContentNodes = new List<IReturnedLanguageControl>(languagesToReturn);
            this.NodeName = "LanguagesToReturn";
        }
    }
}