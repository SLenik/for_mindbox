namespace Problem1.Domain
{
    /// <summary>
    /// Карточка путешествия.
    /// </summary>
    public class TripCard
    {
        /// <summary>
        /// Создание карточки путешествий.
        /// </summary>
        /// <param name="source">Город отправления.</param>
        /// <param name="destination">Город назначения.</param>
        public TripCard(string source, string destination)
        {
            Source = source;
            Destination = destination;
        }

        /// <summary>
        /// Город отправления.
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// Город назначения.
        /// </summary>
        public string Destination { get; }
    }
}
