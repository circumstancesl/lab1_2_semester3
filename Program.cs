using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Animal
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public double DailyFood { get; set; }
    public string FoodType { get; set; }
}

class Program
{
    static void Main(string[] args)
    {
        GenerateAndSaveData();
        ReadAndProcessData();
    }

    static void GenerateAndSaveData()
    {
        List<Animal> animals = new List<Animal>
        {
            new Animal { Name = "Лев", Weight = 200, DailyFood = 7, FoodType = "мясо" },
            new Animal { Name = "Слон", Weight = 4000, DailyFood = 300, FoodType = "трава" },
            new Animal { Name = "Тигр", Weight = 190, DailyFood = 8, FoodType = "мясо" },
            new Animal { Name = "Жираф", Weight = 800, DailyFood = 70, FoodType = "трава" },
            new Animal { Name = "Пингвин", Weight = 25, DailyFood = 2, FoodType = "мясо" }
        };

        using (StreamWriter writer = new StreamWriter("animals.csv"))
        {
            writer.WriteLine("Наименование,Вес,Вес потребляемой пищи за сутки,Тип пищи");
            foreach (var animal in animals)
            {
                writer.WriteLine($"{animal.Name},{animal.Weight},{animal.DailyFood},{animal.FoodType}");
            }
        }

        Console.WriteLine("Данные успешно сохранены в файл animals.csv");
    }

    static void ReadAndProcessData()
    {
        List<Animal> animals = new List<Animal>();

        using (StreamReader reader = new StreamReader("animals.csv"))
        {
            // Пропускаем заголовок
            reader.ReadLine();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] values = line.Split(',');

                animals.Add(new Animal
                {
                    Name = values[0],
                    Weight = double.Parse(values[1]),
                    DailyFood = double.Parse(values[2]),
                    FoodType = values[3]
                });
            }
        }

        var meatEaters = animals.Where(a => a.FoodType == "мясо");
        Console.WriteLine("Животные, поедающие мясо:");
        foreach (var animal in meatEaters)
        {
            Console.WriteLine($"{animal.Name} - Вес: {animal.Weight} кг, Потребляемая пища: {animal.DailyFood} кг");
        }

        var maxFoodPerKg = animals.Max(a => a.DailyFood / a.Weight);
        var animalWithMaxFoodPerKg = animals.First(a => a.DailyFood / a.Weight == maxFoodPerKg);

        Console.WriteLine($"Животное, поедающее максимальное количество пищи на 1 кг собственного веса: {animalWithMaxFoodPerKg.Name}");
    }
}
