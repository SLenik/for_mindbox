using Problem1.Application;
using Problem1.Domain;
using System;
using System.Collections.Generic;

namespace Problem1.Presentation
{
    public class TripCardConsole
    {
        private readonly ITripCardService _tripCardService;

        public TripCardConsole(ITripCardService tripCardService)
        {
            _tripCardService = tripCardService;
        }

        public void Run()
        {
            // Увы, если бы было больше времени - описал был ввод "по фэн-шую", через сервисы

            Console.WriteLine(
                "Введите карточки городов - по одной в строке. " +
                "Разделитель городов в карточке - точка с запятой.");
            Console.WriteLine("Признак окончания ввода - пустая строка");
            string line;
            var endInput = false;
            var tripCards = new List<TripCard>();
            do
            {
                Console.Write("> ");
                line = Console.ReadLine() ?? "";
                endInput = string.IsNullOrWhiteSpace(line);
                if (!endInput)
                {
                    var data = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (data.Length == 2)
                    {
                        // Явно используем интернирование строк чтобы стараться не тратить лишнюю память
                        var source = string.Intern(data[0].Trim());
                        var dest = string.Intern(data[1].Trim());
                        tripCards.Add(new TripCard(source, dest));
                    }
                    else
                    {
                        Console.WriteLine("Ошибка входных данных. Повторите ввод");
                    }
                }
            }
            while (!endInput);

            Console.WriteLine();
            Console.WriteLine($"Введено {tripCards.Count} строк.");

            var orderedTripCards = _tripCardService.OrderTripCards(tripCards);

            Console.WriteLine("Карточки в требуемом порядке: ");
            foreach (var tripCard in orderedTripCards)
                Console.WriteLine($"\t{tripCard.Source} - {tripCard.Destination}");

            Console.WriteLine("Спасибо за внимание!");
        }
    }
}
