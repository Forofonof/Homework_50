using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Database database = new Database();
        Menu menu = new Menu(database);

        menu.work();
    }
}

class Menu
{
    private const string CommandSearch = "1";
    private const string CommandExit = "2";

    private bool _isWork = true;

    private Database _database;

    public Menu(Database database)
    {
        _database = database;
    }

    public void work()
    {
        Console.WriteLine("База данных к вашим услугам.");

        while (_isWork)
        {
            Console.WriteLine($"{CommandSearch} - Начать поиск.\n{CommandExit} - Выход из программы.");
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case CommandSearch:
                    _database.FindPerpetrators();
                    break;

                case CommandExit:
                    _isWork = false;
                    break;

                default:
                    Console.WriteLine("Ошибка!");
                    break;
            }
        }
    }
}

class Search
{
    private List<Perpetrator> _perpetrators = new List<Perpetrator>();
    private Database _database;

    public Search(List<Perpetrator> perpetrators, Database database)
    {
        _perpetrators = perpetrators;
        _database = database;
    }

    public void FindPerpetrators()
    {
        Console.WriteLine("\nВведите параметры преступника\n");

        Console.WriteLine("Введите национальность: ");
        string nationality = Console.ReadLine();

        Console.Write($"Введите рост: ");
        int height = GetNumberInput();

        Console.Write("Введите вес: ");
        int weight = GetNumberInput();

        var filteredPerpetrators = _perpetrators.Where(perpetrator => perpetrator.Height == height
                                                                   && perpetrator.Weight == weight
                                                                   && perpetrator.Nationality == nationality
                                                                   && perpetrator.IsPrisoned == false);

        if (filteredPerpetrators.Any())
        {
            Console.WriteLine("Результат поиска:\n");

            foreach (var perpetrator in filteredPerpetrators)
            {
                perpetrator.ShowInfo();
            }
        }
        else
        {
            Console.WriteLine("Совпадений не найдено.");
        }
    }

    private int GetNumberInput()
    {
        int number = 0;
        bool isWork = true;

        while (isWork)
        {
            if (int.TryParse(Console.ReadLine(), out number))
            {
                isWork = false;
            }
            else
            {
                Console.WriteLine("Ошибка! Введите целое число:");
            }
        }

        return number;
    }
}

class Database
{
    private List<Perpetrator> _perpetrator = new List<Perpetrator>();
    private Search _search;

    public Database()
    {
        _perpetrator.Add(new Perpetrator("Зубенко Михаил Петрович", "Молдован", "Мафиози", 192, 90, false));
        _perpetrator.Add(new Perpetrator("Иванов Артём Алексеевич", "Якут", "Мафиози", 180, 71, false));
        _perpetrator.Add(new Perpetrator("Поляков Глеб Ярославович", "Татар", "Авторитеты", 179, 83, true));
        _perpetrator.Add(new Perpetrator("Сергеев Михаил Тимофеевич", "Чуваш", "Авторитеты", 167, 77, true));
        _perpetrator.Add(new Perpetrator("Дорофеев Денис Леонидович", "Ингуш", "Тамбовская", 191, 95, false));
        _perpetrator.Add(new Perpetrator("Кузнецов Денис Иванович", "Бурят", "Уралсуб", 205, 110, false));

        _search = new Search(_perpetrator, this);
    }

    public void FindPerpetrators()
    {
        _search.FindPerpetrators();
    }
}

class Perpetrator
{
    public Perpetrator(string fullName, string nationality, string gang, int height, int weigth, bool isPrisoned) 
    {
        FullName = fullName;
        Nationality = nationality;
        Gang = gang;
        Height = height;
        Weight = weigth;
        IsPrisoned = isPrisoned;
    }

    public string FullName { get; private set; }

    public string Nationality { get; private set; }

    public string Gang { get; private set; }

    public int Height { get; private set; }

    public int Weight { get; private set; }

    public bool IsPrisoned { get; private set; }

    public void ShowInfo()
    {
        Console.WriteLine($"ФИО - {FullName}.\nНациональность - {Nationality}.");
        Console.WriteLine($"Группировка - {Gang}.\nРост - {Height}.\nВес - {Weight}.");
    }
}