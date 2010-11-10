using System;
using System.Collections.Generic;

namespace AgilityTools.ApiClient.Adsml.Client
{
    public class LanguageControl : SearchControlComponentBase<IReturnedLanguageControl>
    {
        internal LanguageControl(params IReturnedLanguageControl[] languagesToReturn) {
            if (languagesToReturn == null)
                throw new ArgumentNullException("languagesToReturn");

            this.ContentNodes = new List<IReturnedLanguageControl>(languagesToReturn);
            this.NodeName = "LanguagesToReturn";
        }
    }
}