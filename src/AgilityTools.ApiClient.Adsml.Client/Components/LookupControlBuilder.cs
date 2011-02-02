using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Components
{
    public class LookupControlBuilder : ILookupControlBuilder
    {
        private IList<ILookupControlFilter> RequestFilterList { get; set; }
        private IList<ILookupControlComponent> ControlComponents { get; set; }

        public LookupControlBuilder() {
            ControlComponents = new List<ILookupControlComponent>();
        }

        public IReturnedAttributesReturnedLanguagesConfigureReferences AddRequestFilters(params ILookupControlFilter[] filters) {
            if (filters != null) {
                this.RequestFilterList =
                    new List<ILookupControlFilter>(filters);
            }

            return this;
        }

        public IReturnedAttributesReturnedLanguagesConfigureReferences AttributeNamelist(string attributeNames) {
            if (!string.IsNullOrEmpty(attributeNames)) {
                var aControl = new AttributeControl {
                                   OuterNodeAttributes = new List<XAttribute> {
                                                             new XAttribute("namelist", attributeNames)
                                                         }
                               };
                
                this.ControlComponents.Add(aControl);
            }

            return this;
        }

        public IReturnedLanguagesConfigureReferences ReturnAttributes(params IAttributeControl[] attributesToReturn) {
            if (attributesToReturn != null) {

                if (!this.ControlComponents.Any(cc => cc is AttributeControl)) {
                    this.ControlComponents.Add(new AttributeControl(attributesToReturn));
                } else {
                    AttributeControl control =
                        this.ControlComponents.Where(cc => cc is AttributeControl).Cast<AttributeControl>().Single();

                    control.AddAttributes(attributesToReturn);
                }
            }

            return this;
        }

        public IConfigureReferences ReturnLanguages(params IReturnedLanguageControl[] languagesToReturn) {
            if (languagesToReturn != null) {
                this.ControlComponents.Add(new LanguageControl(languagesToReturn));
            }

            return this;
        }

        public void ConfigureReferenceHandling(params IReferenceOptions[] referenceOptions) {
            if (referenceOptions != null) {
                this.ControlComponents.Add(new ReferenceControl(referenceOptions));
            }
        }

        public LookupControl Build() {
            return new LookupControl(this.RequestFilterList, this.ControlComponents);
        }
    }
}