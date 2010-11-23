using System;
using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public class LanguageControl : ControlComponentBase<IReturnedLanguageControl>, ISearchControlComponent, ILookupControlComponent
    {
        internal LanguageControl(params IReturnedLanguageControl[] languagesToReturn) {
            if (languagesToReturn == null)
                throw new ArgumentNullException("languagesToReturn");

            this.ContentNodes = new List<IReturnedLanguageControl>(languagesToReturn);
            this.NodeName = "LanguagesToReturn";
        }
    }
}