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
	   .QueryType(QueryTypes.Below) // Enum of types
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
	   .AttributesToSet(
   		 StructureAttribute.CreateNew(215, 
	   		 new StructureValue(10, "foo"),
	   		 new StructureValue(11, "bar")),
	   	 StructureAttribute.CreateNew(391, 
	   		 new StructureValue(10, "foo"),
	   		 new StructureValue(11, "bar")))
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
	   .AddModification<SAttr>(Modifications.ReplaceAttribute, (attr) => attr = /*...*/)
	   .ConfigureLookupControls()
	   	.Foo();