using System;
using PlantsLibraryVer2;

namespace PlantsLibraryVer2
{
    class Program
    {
        static MyObservableCollection<Plant> collection1;
        static MyObservableCollection<Plant> collection2;
        static Journal journal1;
        static Journal journal2;

        static void Main(string[] args)
        {
            InitializeCollectionsAndJournals();
            ShowMenu();
        }

        static void InitializeCollectionsAndJournals()
        {
            collection1 = new MyObservableCollection<Plant>("Коллекция 1");
            collection2 = new MyObservableCollection<Plant>("Коллекция 2");
            journal1 = new Journal();
            journal2 = new Journal();

            // Подписка журналов на события
            collection1.CollectionCountChanged += (source, e) =>
                journal1.Add(new JournalEntry(e.NameCollection, e.ChangeCollection, e.Obj.ToString()));
            collection1.CollectionReferenceChanged += (source, e) =>
                journal1.Add(new JournalEntry(e.NameCollection, e.ChangeCollection, e.Obj.ToString()));

            collection1.CollectionReferenceChanged += (source, e) =>
                journal2.Add(new JournalEntry(e.NameCollection, e.ChangeCollection, e.Obj.ToString()));
            collection2.CollectionReferenceChanged += (source, e) =>
                journal2.Add(new JournalEntry(e.NameCollection, e.ChangeCollection, e.Obj.ToString()));
        }

        static void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("\nМеню управления коллекциями");
                Console.WriteLine("1. Добавить случайные растения в коллекцию 1");
                Console.WriteLine("2. Добавить случайные растения в коллекцию 2");
                Console.WriteLine("3. Добавить конкретное растение в коллекцию 1");
                Console.WriteLine("4. Добавить конкретное растение в коллекцию 2");
                Console.WriteLine("5. Удалить элемент из коллекции 1");
                Console.WriteLine("6. Удалить элемент из коллекции 2");
                Console.WriteLine("7. Изменить элемент в коллекции 1");
                Console.WriteLine("8. Изменить элемент в коллекции 2");
                Console.WriteLine("9. Показать содержимое коллекции 1");
                Console.WriteLine("10. Показать содержимое коллекции 2");
                Console.WriteLine("11. Показать журнал 1 (все события коллекции 1)");
                Console.WriteLine("12. Показать журнал 2 (изменения ссылок обеих коллекций)");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Ошибка ввода! Попробуйте еще раз.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AddRandomPlantsToCollection(collection1);
                        break;
                    case 2:
                        AddRandomPlantsToCollection(collection2);
                        break;
                    case 3:
                        AddSpecificPlantToCollection(collection1);
                        break;
                    case 4:
                        AddSpecificPlantToCollection(collection2);
                        break;
                    case 5:
                        RemovePlantFromCollection(collection1);
                        break;
                    case 6:
                        RemovePlantFromCollection(collection2);
                        break;
                    case 7:
                        ChangePlantInCollection(collection1);
                        break;
                    case 8:
                        ChangePlantInCollection(collection2);
                        break;
                    case 9:
                        ShowCollection(collection1);
                        break;
                    case 10:
                        ShowCollection(collection2);
                        break;
                    case 11:
                        journal1.PrintJournal();
                        break;
                    case 12:
                        journal2.PrintJournal();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Неверный выбор! Попробуйте еще раз.");
                        break;
                }
            }
        }

        static void AddRandomPlantsToCollection(MyObservableCollection<Plant> collection)
        {
            Console.Write("Введите количество растений для добавления: ");
            if (int.TryParse(Console.ReadLine(), out int count) && count > 0)
            {
                for (int i = 0; i < count; i++)
                {
                    Plant plant = CreateRandomPlant();
                    collection.Add(plant);
                    Console.WriteLine($"Добавлено: {plant}");
                }
            }
            else
            {
                Console.WriteLine("Некорректное количество!");
            }
        }

        static Plant CreateRandomPlant()
        {
            Random rnd = new Random();
            int type = rnd.Next(4); // 0-3

            Plant plant = type switch
            {
                0 => new Plant(),
                1 => new Flower(),
                2 => new Rose(),
                _ => new Tree()
            };

            plant.RandomInit();
            return plant;
        }

        static void AddSpecificPlantToCollection(MyObservableCollection<Plant> collection)
        {
            Console.WriteLine("\nВыберите тип растения:");
            Console.WriteLine("1. Простое растение");
            Console.WriteLine("2. Цветок");
            Console.WriteLine("3. Роза");
            Console.WriteLine("4. Дерево");
            Console.Write("Ваш выбор: ");

            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 4)
            {
                Plant plant = choice switch
                {
                    1 => new Plant(),
                    2 => new Flower(),
                    3 => new Rose(),
                    _ => new Tree()
                };

                plant.Init();
                collection.Add(plant);
                Console.WriteLine("Растение добавлено в коллекцию!");
            }
            else
            {
                Console.WriteLine("Неверный выбор типа растения!");
            }
        }

        static void RemovePlantFromCollection(MyObservableCollection<Plant> collection)
        {
            if (collection.Count == 0)
            {
                Console.WriteLine("Коллекция пуста!");
                return;
            }

            Console.WriteLine("Текущая коллекция:");
            for (int i = 0; i < collection.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {collection[i]}");
            }

            Console.Write("Введите номер элемента для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= collection.Count)
            {
                var removed = collection[index - 1];
                collection.RemoveAt(index - 1);
                Console.WriteLine($"Удалено: {removed}");
            }
            else
            {
                Console.WriteLine("Некорректный индекс!");
            }
        }

        static void ChangePlantInCollection(MyObservableCollection<Plant> collection)
        {
            if (collection.Count == 0)
            {
                Console.WriteLine("Коллекция пуста!");
                return;
            }

            Console.WriteLine("Текущая коллекция:");
            for (int i = 0; i < collection.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {collection[i]}");
            }

            Console.Write("Введите номер элемента для изменения: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= collection.Count)
            {
                Console.WriteLine("\nВыберите тип нового растения:");
                Console.WriteLine("1. Простое растение");
                Console.WriteLine("2. Цветок");
                Console.WriteLine("3. Роза");
                Console.WriteLine("4. Дерево");
                Console.Write("Ваш выбор: ");

                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 4)
                {
                    Plant newPlant = choice switch
                    {
                        1 => new Plant(),
                        2 => new Flower(),
                        3 => new Rose(),
                        _ => new Tree()
                    };

                    newPlant.Init();
                    collection[index - 1] = newPlant;
                    Console.WriteLine("Элемент коллекции изменен!");
                }
                else
                {
                    Console.WriteLine("Неверный выбор типа растения!");
                }
            }
            else
            {
                Console.WriteLine("Некорректный индекс!");
            }
        }

        static void ShowCollection(MyObservableCollection<Plant> collection)
        {
            if (collection.Count == 0)
            {
                Console.WriteLine("Коллекция пуста!");
                return;
            }

            Console.WriteLine($"\nСодержимое коллекции {collection.Name}:");
            for (int i = 0; i < collection.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {collection[i]}");
            }
        }
    }
}