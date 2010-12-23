var builder = new SearchControlBuilder();

builder.Configure()
	   .AddRequestFilters(
	   	   	Filter.ExcludeBin(),
	   	   	Filter.ExcludeDocument(),
	   	   	Filter.OmitStructureAttributes(),
	   	   	Filter.AllowPaging(),
	   	   	Filter.PageSize(2))
	   .SpecifyReturnedAttributes(
	   		Attribute.WithDefinitionId(31),
	   		Attribute.WithName("foo"))
	   .ConfigureReferenceHandling(
	   		Options.UseChannel(3),
	   		Options.ResolveAttributes(),
	   		Options.ResolveSpecialCharacters()
	   		Options.ReturnValueOnly());

var builder = new AqlQueryBuilder();

builder.BasePath("/foo/bar")
	   .QueryType(AqlQueryTypes.Below) // Enum of types
	   .ObjectTypeToFind(12)		// Optional, can be string
	   .QueryString("\"objectId\" <= \"10\"")
	   .ConfigureSearchControls()
		   	.AddRequestFilters(
		   	   	Filter.ExcludeBin(),
		   	   	Filter.ExcludeDocument(),
		   	   	Filter.OmitStructureAttributes(),
		   	   	Filter.AllowPaging(),
		   	   	Filter.PageSize(2))
		   .ReturnAttributes(
		   		Attribute.WithDefinitionId(31),
		   		Attribute.WithName("foo"))
		   .ReturnLanguages(
			    Language.WithId(10),
			    Language.WithId(11))
		   .ConfigureReferenceHandling(
		   		RefOptions.UseChannel(3),
		   		RefOptions.ResolveAttributes(),
		   		RefOptions.ResolveSpecialCharacters()
		   		RefOptions.ReturnValueOnly());

var query = builder.Build();

var builder = new CreateRequestBuilder();

builder.NewContextName("/foo/bar")
	   .ObjectTypeToCreate("Produkt")
	   .FailOnError()
	   .AttributesToSet(() => new List<IAdsmlAttribute> {
		   	StructureAttribute.New(215, new StructureValue(10, "foo")),
		   	RelationAttribute.New(31, 1779))
	   	})
	   .ConfigureLookupControls()
	   	   .AddRequestFilters(
		   		Filter.ReturnRelationsAsAttributes(),
		   		Filter.ResolveContextReferences())
		   .ReturnAttributes(
		   		Attribute.WithDefinitionId(31),
		   		Attribute.WithName("foo"))
		   .ReturnLanguages(
			    Language.WithId(10),
			    Language.WithId(11))
		   .ConfigureReferenceHandling(
		   		RefOptions.UseChannel(3),
		   		RefOptions.ResolveAttributes(),
		   		RefOptions.ResolveSpecialCharacters()
		   		RefOptions.ReturnValueOnly());

// OR

IList<StructureAttribute> attributes = new List<StructureAttribute>();
// Fill collection

builder.NewContextName("/foo/bar")
	   .ObjectTypeToCreate("Produkt")
	   .FailOnError()
	   .AttributesToSet(attributes)
	   .ConfigureLookupControls()


/*----------------------------------------------------------------------*/
/*                    MODIFY OPERATION                                  */
/*----------------------------------------------------------------------*/

var builder = new ModifyRequestBuilder();

builder.Context("/foo/bar")
	   .ReturnNoAttributes()
	   .FailOnError()
	   .AddModification<SAttr>(
		   Modifications.ReplaceAttribute, 
		   () => /*...*/)
	   .ConfigureLookupControls()
	   	.Foo();

/*----------------------------------------------------------------------*/
/*                      LINK OPERATION                                  */
/*----------------------------------------------------------------------*/

var builder = new LinkRequestBuilder();

builder.Context("foo")
	   .Target("bar")
	   .ReturnNoAttributes()
	   .CopyControls()
	   	.CopyLocalAttributesFromSource()
	   	.LookupControls()
	   		.Foo();



public interface IResponseConverter<TOutput> : Converter<XElement, out TOutput> where TOutput : class
{
	TOutput GetResponse(XElement source);
}

public class DeleteResponse : AdsmlResult { }

public class AdsmlResult
{
	public IEnumerable<string> Messages { get; set; }
	public string Code { get; set; }
	public string Description { get; set; }
}

public class AdsmlResponseConverter : IResponseConverter
{
	TOutput GetResponse<TOutput>(XElement source) where TOutput : class, AdsmlResult {
		var result = new AdsmlResult();
		// fill properties
		return (TOutput) result;
	}
}

/*
	<xsd:complexType name="ADSMLResult">
		<xsd:sequence>
			<xsd:element name="Message" type="xsd:string" minOccurs="0" maxOccurs="unbounded"/>
		</xsd:sequence>
		<xsd:attribute name="code" type="xsd:string" use="required"/>
		<xsd:attribute name="description" type="xsd:string"/>
	</xsd:complexType>
*/