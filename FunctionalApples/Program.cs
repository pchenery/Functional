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
            Apple apple2 = new Apple();
            
            var Apples = apple.PickApples().Take(10000).
                Where(a => a.Colour == "Red").
                Where(a => a.Poisoned == true).
                Zip(apple.PickApples().Take(10000).Skip(1).
                Where(a => a.Colour == "Red").
                Where(a => a.Poisoned == true)
                , (curr, prev) => curr.Colour == prev.Colour).
                Max();


            Console.WriteLine(apple.HowManyArePoisoned());
            Console.WriteLine(apple.SecondMostPoisonousColour());
            Console.WriteLine(Apples);
            //Console.WriteLine(test);

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
                return PickApples().
                       Take(10000).
                       Count(a => a.Poisoned == true);
            }

            public string SecondMostPoisonousColour()
            {
                return PickApples().Take(10000).
                       Where(a => a.Poisoned == true).
                       GroupBy(a => a.Colour).
                       OrderByDescending(g => g.Count()).
                       ElementAt(1).Key;
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
