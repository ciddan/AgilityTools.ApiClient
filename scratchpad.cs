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

builder.ConfigureQuery()
	   .BasePath("/foo/bar")
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

var result = apiClient.SendQuery(builder.BuildQuery());