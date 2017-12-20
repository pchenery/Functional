using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalApples
{
    class Program
    {
        static void Main(string[] args)
        {
            Apple apple = new Apple();

            Console.WriteLine(apple.HowManyArePoisoned());
            Console.WriteLine(apple.SecondMostPoisonousColour());
            Console.WriteLine(apple.MaxRunRedNonPoisoned());
            Console.WriteLine(apple.GreenFollowedbyGreen());

            Console.ReadLine();
        }
        class Apple
        {
            public string Colour { get; set; }
            public bool Poisoned { get; set; }

            public override string ToString()
            {
                return $"{Colour} apple{(Poisoned ? " (poisoned!)" : "")}";
            }

            public int HowManyArePoisoned()
            {
                return PickApples()
                       .Take(10000)
                       .Count(a => a.Poisoned);
            }

            public string SecondMostPoisonousColour()
            {
                return PickApples().Take(10000)
                       .Where(a => a.Poisoned)
                       .GroupBy(a => a.Colour)
                       .OrderByDescending(g => g.Count())
                       .ElementAt(1).Key;
            }

            public int MaxRunRedNonPoisoned()
            {
                return PickApples()
                        .Take(10000)
                        .Aggregate(Tuple.Create(0, 0),
                            (acc, app) =>
                            (app.Colour == "Red" && !app.Poisoned)
                            ? Tuple.Create(acc.Item1 + 1, Math.Max(acc.Item1 + 1, acc.Item2))
                            : Tuple.Create(0, acc.Item2))
                        .Item2;
            }

            public int GreenFollowedbyGreen()
            {
                return PickApples().Take(10000)
                        .Zip(PickApples()
                        .Skip(1), Tuple.Create)
                        .Count(pair => pair.Item1.Colour == "Green" && pair.Item2.Colour == "Green");
            }

            public IEnumerable<Apple> PickApples()
            {
                int colourIndex = 1;
                int poisonIndex = 7;

                while (true)
                {
                    yield return new Apple
                    {
                        Colour = GetColour(colourIndex),
                        Poisoned = poisonIndex % 41 == 0
                    };

                    colourIndex += 5;
                    poisonIndex += 37;
                }
            }

            private string GetColour(int colourIndex)
            {
                if (colourIndex % 13 == 0 || colourIndex % 29 == 0)
                {
                    return "Green";
                }

                if (colourIndex % 11 == 0 || colourIndex % 19 == 0)
                {
                    return "Yellow";
                }

                return "Red";
            }
        }
    }
}
