using CarsLibrary;
using MyCollectionLibrary;
using System.Threading.Channels;
namespace L14_2
{
    internal class Program
    {
        public static bool ProcessCarQueries<T>(
        MyCollection<Car> col,
        Func<MyCollection<Car>, T> extensionMethodQuery,
        Func<MyCollection<Car>, T> linqQuery,
        Action<T> displayResult,
        string message,
        Predicate<T> isEmpty,
        string errorMessage)
        {
            if (col.Count > 0)
            {
                T EM = extensionMethodQuery(col);
                T LINQ = linqQuery(col);

                if (isEmpty(EM) || isEmpty(LINQ))
                {
                    Console.WriteLine(errorMessage);
                    return false;
                }

                Console.WriteLine($"{message} методами расширения:");
                displayResult(EM);
                Console.WriteLine($"\n{message} LINQ запросами:");
                displayResult(LINQ);
                return true;
            }
            else
            {
                Console.WriteLine("Коллекция не создана!");
                return false;
            }
        }
        static bool fillCollection(ref MyCollection<Car> col, int length)
        {
            col = new MyCollection<Car>();
            Random random = new Random();
            var carTypes = new List<Type> { typeof(Car), typeof(PassengerCar), typeof(SUV), typeof(Truck) };
            for (int i = 0; i < length; i++)
            {
                Type carType = carTypes[random.Next(carTypes.Count)];
                Car car = (Car)Activator.CreateInstance(carType);
                car.RandomInit();
                col.Add(car);
            }
            Console.WriteLine("\nКоллекция была успешно создана!");
            return true;
        }
        static List<Car> ChooseAllAutosReleasedAfter2000_EM(MyCollection<Car> col)
        {
            return col.Select(x => x).Where(y => y.Year > 2000).ToList();
        }
        static List<Car> ChooseAllAutosReleasedAfter2000_LINQ(MyCollection<Car> col)
        {
            return (from car in col
                    where car.Year > 2000
                    select car).ToList();
        }
        static int CountAllTrucks_EM(MyCollection<Car> col)
        {
            return col.Where(x => x is Truck).Count();
        }
        static int CountAllTrucks_LINQ(MyCollection<Car> col)
        {
            return (from car in col
                    where car is Truck
                    select car).Count();
        }
        static int PriceOfAllCars_EM(MyCollection<Car> col)
        {
            return col.Select(x => x.Price).Sum();
        }
        static int PriceOfAllCars_LINQ(MyCollection<Car> col)
        {
            return (from car in col
                    select car.Price).Sum();
        }
        static List<Car> NewestCars_EM(MyCollection<Car> col)
        {
            return col.Where(x => x.Year == col.Max(y => y.Year)).ToList();
        }
        static List<Car> NewestCars_LINQ(MyCollection<Car> col)
        {
            return (from car in col
                    where car.Year == col.Max(c => c.Year)
                    select car).ToList();
        }
        static List<Car> OldestCars_EM(MyCollection<Car> col)
        {
            return col.Where(x => x.Year == col.Min(y => y.Year)).ToList();
        }
        static List<Car> OldestCars_LINQ(MyCollection<Car> col)
        {
            return (from car in col
                    where car.Year == col.Min(c => c.Year)
                    select car).ToList();
        }
        static double AveragePriceOfPassengerCars_EM(MyCollection<Car> col)
        {
            var prices = col.Where(x => x is PassengerCar).Select(y => y.Price);
            return prices.Any() ? prices.Average(c => c) : 0;
        }
        static double AveragePriceOfPassengerCars_LINQ(MyCollection<Car> col)
        {
            var prices = from car in col
                         where car is PassengerCar
                         select car.Price;
            return prices.Any() ? prices.Average(c => c) : 0;
        }
        static List<IGrouping<int, Car>> GroupByYear_EM(MyCollection<Car> col)
        {
            return col.GroupBy(car => car.Year).ToList();
        }
        static List<IGrouping<int, Car>> GroupByYear_LINQ(MyCollection<Car> col)
        {
            return (from car in col
                    group car by car.Year).ToList();
        }
        static void Main(string[] args)
        {
            string Menu = "\nВыберите действие с хеш-таблицей:\n" +
                         "1. Создать коллекцию машин.\n" +
                         "2. Распечатать коллекцию.\n" +
                         "3. Выбрать все автомобили, выпущенные после 2000 года (Where).\n" +
                         "4. Посчитать кол-во грузовиков (Count).\n" +
                         "5. Посчитать суммарную стоимость всех машин (Sum).\n" +
                         "6. Найти самые новые автомобили (Max).\n" +
                         "7. Найти самый старый автомобиль (Min).\n" +
                         "8. Найти среднюю цену среди всех легковых автомобилей (Average).\n" +
                         "9. Сгруппировать автомобили по году выпуска (Group By).\n" +
                         "10. Выход.\n";
            MyCollection<Car> collection = new MyCollection<Car>();
            int response;
            do
            {
                Console.WriteLine(Menu);
                response = VHS.Input("Ошибка! Введите целое число от 1 до 7!", x => 1 <= x && x <= 10);
                Console.WriteLine();
                try
                {
                    switch (response)
                    {
                        case 1:
                            Console.WriteLine("Введите кол-во элементов коллекции:");
                            fillCollection(
                                ref collection,
                                VHS.Input("Ошибка! Введите натуральное число больше 1!", x => 1 < x));
                            break;
                        case 2:
                            collection.Print();
                            break;
                        case 3:
                            ProcessCarQueries(
                                collection,
                                ChooseAllAutosReleasedAfter2000_EM,
                                ChooseAllAutosReleasedAfter2000_LINQ,
                                x => x.ForEach(item => Console.WriteLine(item)),
                                "Выборка",
                                x => x.Count <= 0,
                                "Машин выпушенных с 2000 года не было найдено!"
                            );
                            break;
                        case 4:
                            ProcessCarQueries(
                                collection,
                                CountAllTrucks_EM,
                                CountAllTrucks_LINQ,
                                x => Console.WriteLine(x),
                                "Подсчёт кол-ва грузовиков в коллекции",
                                x => x == 0,
                                "Грузовиков не было найдено!"
                            );
                            break;
                        case 5:
                            ProcessCarQueries(
                                collection,
                                PriceOfAllCars_EM,
                                PriceOfAllCars_LINQ,
                                x => Console.WriteLine($"{x} руб."),
                                "Суммарная стоимость всех машин",
                                x => x == 0,
                                "Суммарная стоимость не была найдена!"
                            );
                            break;
                        case 6:
                            ProcessCarQueries(
                                collection,
                                NewestCars_EM,
                                NewestCars_LINQ,
                                x => x.ForEach(item => Console.WriteLine(item)),
                                "Новейшие машины",
                                x => x.Count <= 0,
                                "Новейших машин нет!"
                            );
                            break;
                        case 7:
                            ProcessCarQueries(
                                collection,
                                OldestCars_EM,
                                OldestCars_LINQ,
                                x => x.ForEach(item => Console.WriteLine(item)),
                                "Самые старые машины",
                                x => x.Count <= 0,
                                "Самых старых машин нет!"
                            );
                            break;
                        case 8:
                            ProcessCarQueries(
                                collection,
                                AveragePriceOfPassengerCars_EM,
                                AveragePriceOfPassengerCars_LINQ,
                                x => Console.WriteLine($"{x:f0} руб."),
                                "Средняя стоимость легковых машин",
                                x => x == 0,
                                "Легковых машин нет!"
                            );
                            break;
                        case 9:
                            ProcessCarQueries(
                                collection,
                                GroupByYear_EM,
                                GroupByYear_LINQ,
                                x => x.ForEach(group => { Console.WriteLine($"{group.Key} ({group.Count()}):"); foreach (var item in group) { Console.WriteLine($"      {item}"); } }),
                                "Группировка всех авто по году выпуска",
                                x => x.Count <= 0,
                                "Группировку невозможно выполнить!"
                            );
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            } while (response != 10);
        }
    }
}
