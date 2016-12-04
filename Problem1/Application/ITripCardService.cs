using Problem1.Domain;
using System.Collections.Generic;

namespace Problem1.Application
{
    /// <summary>
    /// Сервис работы с карточками путешествий.
    /// </summary>
    public interface ITripCardService
    {
        /// <summary>
        /// Упорядочить карточки путешествий 
        /// </summary>
        /// <param name="tripCards"></param>
        IReadOnlyList<TripCard> OrderTripCards(IReadOnlyList<TripCard> tripCards);
    }
}
