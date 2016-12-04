using Xunit;

namespace Problem1.Test
{
    [CollectionDefinition(COLLECTION_NAME)]
    public class TripCardTestCollection : ICollectionFixture<TripCardFixture>
    {
        internal const string COLLECTION_NAME = "Тесты упорядочения карточек путешествий";

        // This class has no code, and is never created. Its purpose is simply
        // to be the place to apply [CollectionDefinition] and all the
        // ICollectionFixture<> interfaces.
        // See https://xunit.github.io/docs/shared-context.html for details.
    }
}
