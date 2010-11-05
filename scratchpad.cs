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
	   		Options.ReturnValueOnly()
		   );

var builder = new AqlQueryBuilder();

builder.ConfigureQuery()
	   .BasePath("/foo/bar")
	   .QueryType(QueryTypes.Below) // Enum of types
	   .ObjectTypeToFind(12)		// .Find is optional, can be string
	   .AttributeToMatch(10) 		// .AttributeToMatch("foo")
	   .SearchTerm("foo")
 	   .ConfigureSearchControls()
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

// OR

builder.ConfigureQuery()
	   .BasePath("/foo/bar")
	   .QueryType(QueryTypes.Below) // Enum of types
	   .ObjectTypeToFind(12)		// .Find is optional, can be string
	   .ManualQuery("\"objectId\" <= \"10\"")
	   .ConfigureSearchControls()
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

var result = apiClient.SendQuery(builder.BuildQuery());