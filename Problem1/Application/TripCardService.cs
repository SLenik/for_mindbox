using System.Collections.Generic;
using Problem1.Domain;

namespace Problem1.Application
{
    public class TripCardService : ITripCardService
    {
        private readonly IEqualityComparer<string> _tripCardStringComparer;

        public TripCardService(IEqualityComparer<string> tripCardStringComparer)
        {
            _tripCardStringComparer = tripCardStringComparer;
        }

        private class TripCardElement
        {
            public TripCardElement(TripCard tripCard, int sourceIndex)
            {
                TripCard = tripCard;
                SourceIndex = sourceIndex;
            }

            public TripCard TripCard { get; }

            public int SourceIndex { get; }
        }

        public IReadOnlyList<TripCard> OrderTripCards(IReadOnlyList<TripCard> tripCards)
        {
            var destinationToTripCard = new Dictionary<string, TripCardElement>(
                tripCards.Count, _tripCardStringComparer);

            // ToDo: обработка InvalidOperationException => набор не соответствует условиям задачи
            // (имеется две карты с одним и тем же городом назначения)
            for (var i = 0; i < tripCards.Count; i++)
                destinationToTripCard.Add(tripCards[i].Destination, new TripCardElement(tripCards[i], i));

            // определяем для текущей карты индекс следующего элемента в исходной последовательности
            var nextTripCardIndex = new int[tripCards.Count];
            var firstTripCardIndex = -1;
            for (var i = 0; i < tripCards.Count; i++)
            {
                TripCardElement previousTripCard;
                if (destinationToTripCard.TryGetValue(tripCards[i].Source, out previousTripCard))
                    // для предыдущего города текущий элемент - это следующий 
                    nextTripCardIndex[previousTripCard.SourceIndex] = i;
                // Если карточка с указанным городом назначения отсутствует, то текущая карточка - последняя
                else firstTripCardIndex = i;
            }

            // Используя полученную перестановку, строим по исходному 
            // списку элементов новый, но уже упорядоченный в указанной последовательности
            var sortedTripCards = new List<TripCard>(tripCards.Count);
            for(int i = 0; i < tripCards.Count; i++)
            {
                var currentTripCard = tripCards[firstTripCardIndex];
                sortedTripCards.Add(currentTripCard);
                firstTripCardIndex = nextTripCardIndex[firstTripCardIndex];
            }

            return sortedTripCards;

            // Послесловие
            // --------------------------------------------------
            // Разумеется, для реализованного алгоритма можно значительно улучшить оценку в худшем, 
            // заменив хэширование на структуру данных, специально предназначенную для работы со строками, - префиксное дерево (Trie)
            // В этом случае названная оценка O(N * Avg(L)) в среднем превратится в O(N * Max(L)) в худшем.
            //
            // НО без реальной необходимости (кому это надо в тестовом задании?!) реализовывать такое решение мне было банально лень =))
        }
    }
}
