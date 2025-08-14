using Xunit;

[CollectionDefinition("GlobalCollection")]
public class GlobalCollection : ICollectionFixture<GlobalFixture>
{

}