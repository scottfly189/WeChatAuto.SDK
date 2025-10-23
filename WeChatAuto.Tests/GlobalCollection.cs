using Xunit;
[CollectionDefinition("UiTestCollection", DisableParallelization = true)]
public class UiTestCollection : ICollectionFixture<UiTestFixture>
{

}