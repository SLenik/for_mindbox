using Problem1.Application;
using Problem1.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Problem1.Test
{
    [Collection(TripCardTestCollection.COLLECTION_NAME)]
    public class TripCardTest
    {
        // Чтобы запустить тесты через VS, см.
        // https://xunit.github.io/docs/getting-started-desktop.html#run-tests-visualstudio

        private const string TEST_NAME_PREFIX = "TripCardService.";
        private readonly ITripCardService _tripCardService;
        private readonly TripCardComparer _tripCardComparer;

        public TripCardTest(TripCardFixture fixture)
        {
            _tripCardService = fixture.TripCardService;
            _tripCardComparer = fixture.TripCardComparer;
        }

        //
        // Если по фэн-шую, то в качестве теста надо задавать исходные данные + правильную перестановку - 
        // а потом сверять эту пару с результатом. 
        // Но конкретно сейчас мне выгоднее написать как сделать правильно + накопипастить =)
        //

        [Fact(DisplayName = TEST_NAME_PREFIX + nameof(ZeroTest))]
        public void ZeroTest()
        {
            var testTripCards = new TripCard[0];
            var orderedTripCards = _tripCardService.OrderTripCards(testTripCards);

            Assert.NotEqual(null, orderedTripCards);
            Assert.Empty(orderedTripCards);
        }

        [Fact(DisplayName = TEST_NAME_PREFIX + nameof(OneTest))]
        public void OneTest()
        {
            IReadOnlyList<TripCard> testTripCards = new[]
            {
                new TripCard("Москва", "Воркута")
            };
            var orderedTripCards = _tripCardService.OrderTripCards(testTripCards);

            Assert.NotEqual(null, orderedTripCards);
            Assert.Equal(testTripCards.Count, orderedTripCards.Count);
            Assert.Equal(testTripCards[0], orderedTripCards[0], _tripCardComparer);
        }

        [Fact(DisplayName = TEST_NAME_PREFIX + nameof(TwoTest))]
        public void TwoTest()
        {
            var testTripCards = new List<TripCard>
            {
                new TripCard("Воркута", "Сыктывкар"),
                new TripCard("Москва", "Воркута")
            };
            var orderedTripCards = _tripCardService.OrderTripCards(testTripCards);

            Assert.NotEqual(null, orderedTripCards);
            Assert.Equal(testTripCards.Count, orderedTripCards.Count);
            Assert.Equal(orderedTripCards[0], testTripCards[1], _tripCardComparer);
            Assert.Equal(orderedTripCards[1], testTripCards[0], _tripCardComparer);
        }

        [Fact(DisplayName = TEST_NAME_PREFIX + nameof(ThreeTest))]
        public void ThreeTest()
        {
            var testTripCards = new List<TripCard>
            {
                new TripCard("Мельбурн", "Кельн"),
                new TripCard("Москва", "Париж"),
                new TripCard("Кельн", "Москва")
            };
            var orderedTripCards = _tripCardService.OrderTripCards(testTripCards);

            Assert.NotEqual(null, orderedTripCards);
            Assert.Equal(testTripCards.Count, orderedTripCards.Count);
            Assert.Equal(orderedTripCards[0], testTripCards[0], _tripCardComparer);
            Assert.Equal(orderedTripCards[1], testTripCards[2], _tripCardComparer);
            Assert.Equal(orderedTripCards[2], testTripCards[1], _tripCardComparer);
        }

        [Fact(DisplayName = TEST_NAME_PREFIX + nameof(FourTest))]
        public void FourTest()
        {
            var testTripCards = new List<TripCard>
            {
                new TripCard("Сергиев-Посад", "Мытищи"),
                new TripCard("Москва", "Ярославль"),
                new TripCard("Мытищи", "Щёлково"),
                new TripCard("Ярославль", "Сергиев-Посад")
            };
            var orderedTripCards = _tripCardService.OrderTripCards(testTripCards);

            Assert.NotEqual(null, orderedTripCards);
            Assert.Equal(testTripCards.Count, orderedTripCards.Count);
            Assert.Equal(orderedTripCards[0], testTripCards[1], _tripCardComparer);
            Assert.Equal(orderedTripCards[1], testTripCards[3], _tripCardComparer);
            Assert.Equal(orderedTripCards[2], testTripCards[0], _tripCardComparer);
            Assert.Equal(orderedTripCards[3], testTripCards[2], _tripCardComparer);
        }

        [Fact(DisplayName = TEST_NAME_PREFIX + nameof(RandomTest))]
        public void RandomTest()
        {
            const int TEST_LENGTH = 100000;
            const int TOWN_NAME_LENGTH_MIN = 5;
            const int TOWN_NAME_LENGTH_MAX = 10;
            const string TOWN_NAME_SYMBOLS = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var r = new Random();

            var towns = new HashSet<string>(_tripCardComparer.StringComparer);
            var sb = new StringBuilder(TOWN_NAME_LENGTH_MAX);
            while (towns.Count <= TEST_LENGTH)
            {
                sb.Clear();
                var newTownLength = r.Next(TOWN_NAME_LENGTH_MIN, TOWN_NAME_LENGTH_MAX);
                for (int i = 0; i < newTownLength; i++)
                    sb.Append(TOWN_NAME_SYMBOLS[r.Next(TOWN_NAME_SYMBOLS.Length)]);

                towns.Add(sb.ToString());
            }

            var sourceTripCards = new List<TripCard>(TEST_LENGTH);
            string previousTown = null;
            foreach(var town in towns)
            {
                if (previousTown != null)
                    sourceTripCards.Add(new TripCard(previousTown, town));
                previousTown = town;
            }

            var testTripCards = new List<TripCard>(sourceTripCards);
            for(var shuffleRounds = r.Next(TEST_LENGTH); shuffleRounds >= 0; shuffleRounds--)
            {
                int i = r.Next(TEST_LENGTH), j = r.Next(TEST_LENGTH);
                if (i != j)
                {
                    var t = testTripCards[i];
                    testTripCards[i] = testTripCards[j];
                    testTripCards[j] = t;
                }
            }

            var orderedTripCards = _tripCardService.OrderTripCards(testTripCards);

            Assert.NotEqual(null, orderedTripCards);
            Assert.Equal(testTripCards.Count, orderedTripCards.Count);
            Assert.Equal(sourceTripCards, orderedTripCards, _tripCardComparer);
        }
    }
}
