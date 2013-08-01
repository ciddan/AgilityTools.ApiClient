using System;
using System.Xml.Linq;
using AgilityTools.ApiClient.Adsml.Client.Components;
using AgilityTools.ApiClient.Adsml.Client.Requests;
using NUnit.Framework;
using System.Linq;

namespace AgilityTools.ApiClient.Adsml.Client.Tests.Requests.Builders
{
  [TestFixture]
  public class AqlQueryBuilderFixture
  {
    [Test]
    public void Can_Instantiate_New_AqlQueryBuilder() {
      //Act
      var aql = new AqlQueryBuilder();

      //Assert
      Assert.That(aql, Is.Not.Null);
    }

    [Test]
    public void Can_Specify_Base_Path() {
      //Arrange
      var aql = new AqlQueryBuilder();

      //Act
      aql.BasePath("foo");

      //Assert
      Assert.That(aql.Path, Is.EqualTo("foo"));
    }

    [Test]
    public void Can_Specify_QueryType() {
      //Arrange
      var aql = new AqlQueryBuilder();

      //Act
      aql.QueryType(AqlQueryTypes.Below);

      //Assert
      Assert.That(aql.SelectedAqlQueryType.ToString(), Is.EqualTo("Below"));
    }

    [Test]
    public void Can_Specify_ObjectTypeToFind_By_TypeId() {
      //Arrange
      var aql = new AqlQueryBuilder();

      //Act
      aql.ObjectTypeToFind(10);

      //Assert
      Assert.That(aql.ObjectTypeIds.Count, Is.EqualTo(1));
      Assert.That(aql.ObjectTypeIds.Single(), Is.EqualTo(10));
    }

    [Test]
    public void Can_Specify_ObjectTypeToFind_By_TypeName()
    {
      //Arrange
      var aql = new AqlQueryBuilder();

      //Act
      aql.ObjectTypeToFind("foo");

      //Assert
      Assert.That(aql.ObjectTypeName, Is.EqualTo("foo"));
    }

    [Test]
    public void Can_Provide_QueryString() {
      //Arrange
      var aql = new AqlQueryBuilder();

      //Act
      aql.QueryString("bar");

      //Assert
      Assert.That(aql.Query, Is.EqualTo("bar"));
    }

    [Test]
    public void Can_Configure_SearchControls() {
      //Arrange
      var aql = new AqlQueryBuilder();

      //Act
      aql.ConfigureSearchControls().AddRequestFilters(Filter.CountLimit(1));

      //Assert
      Assert.That(
        aql.SearchControlBuilder.Build().ToAdsml().ToString(),
        Is.EqualTo(new XElement("SearchControls", new XAttribute("countLimit", "1")).ToString())
      );
    }

    [Test]
    public void Can_Combine_All_Api_Commands() {
      //Arrange
      var aqlBuilder = new AqlQueryBuilder();

      //Act
      aqlBuilder.BasePath("/foo/bar")
                .SearchRequestFilters(Filter.ReturnNoAttributes())
                .QueryType(AqlQueryTypes.Below)
                .ObjectTypeToFind(12)
                .QueryString("#215 = \"foo\"")
                .ConfigureSearchControls()
                  .AddRequestFilters(
                      Filter.ExcludeBin(),
                      Filter.ExcludeDocument(),
                      Filter.CountLimit(1))
                  .ReturnAttributes(AttributeToReturn.WithName("Artikelnummer"))
                  .ReturnLanguages(LanguageToReturn.WithLanguageId(10))
                  .ConfigureReferenceHandling(
                      ReferenceOptions.ResolveAttributes(),
                      ReferenceOptions.ResolveSpecialCharacters(),
                      ReferenceOptions.UseChannel(3),
                      ReferenceOptions.ReturnValuesOnly());

      Console.WriteLine(aqlBuilder.Build().ToAdsml().ToString());
      var request = new BatchRequest(aqlBuilder.Build());

      //Assert
      Assert.DoesNotThrow(() => aqlBuilder.Build());
      Assert.DoesNotThrow(() => request.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }

    [Test]
    public void Can_Build_AqlSearchRequest() {
      //Arrange
      var builder = new AqlQueryBuilder();

      //Act
      builder.BasePath("/foo/bar")
             .ObjectTypeToFind("baz")
             .QueryString("foo");

      var aql = builder.Build();
      var request = new BatchRequest(aql);

      //Assert
      Assert.That(aql, Is.Not.Null);
      Assert.That(aql, Is.InstanceOf<AqlSearchRequest>());
      
      Assert.DoesNotThrow(() => request.ToAdsml().ValidateAdsmlDocument("adsml.xsd"));
    }
  }
}