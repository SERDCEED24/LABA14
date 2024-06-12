using CarsLibrary;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Channels;
namespace L14_1
{
    public class Program
    {
        public static bool fillQueue(ref Queue<List<Car>> queue, int length)
        {
            Random random = new Random();
            var carTypes = new List<Type> { typeof(Car), typeof(PassengerCar), typeof(SUV), typeof(Truck) };
            for (int i = 0; i < 3; i++)
            {
                var workshop = new List<Car>();
                for (int j = 0; j < length; j++)
                {
                    Type carType = carTypes[random.Next(carTypes.Count)];
                    Car car = (Car)Activator.CreateInstance(carType);
                    car.RandomInit();
                    workshop.Add(car);
                }
                queue.Enqueue(workshop);
            }
            Console.WriteLine("\nЗавод был успешно создан!");
            return true;
        }
        public static void PrintQueue(Queue<List<Car>> queue)
        {
            if (queue.Count != 0)
            {
                Queue<List<Car>> tempQueue = new Queue<List<Car>>(queue);
                for (int i = 0; i < queue.Count; i++)
                {
                    Console.WriteLine($"Цех {i + 1}:");
                    List<Car> curr = tempQueue.Dequeue();
                    foreach (Car item in curr)
                    {
                        Console.WriteLine(item);
                    }
                    if (tempQueue.Count != 0)
                        Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("Завод не создан!");
            }
        }
        public static List<Car> ChooseAllAutosReleasedAfter2000_EM(Queue<List<Car>> queue)
        {
            return queue.SelectMany(x => x.Where(y => y.Year > 2000)).ToList();
        }
        public static List<Car> ChooseAllAutosReleasedAfter2000_LINQ(Queue<List<Car>> queue)
        {
            return (from list in queue
                    from item in list
                    where item.Year > 2000
                    select item).ToList();
        }
        public static List<Car> FindIntersectionsInWorkshops_EM(Queue<List<Car>> queue)
        {
            return queue.ElementAt(0).Intersect(queue.ElementAt(1)).Intersect(queue.ElementAt(2)).ToList();
        }
        public static List<Car> FindIntersectionsInWorkshops_LINQ(Queue<List<Car>> queue)
        {
            return ((from car in queue.ElementAt(0)
                     select car)
                          .Intersect(from car in queue.ElementAt(1)
                                     select car)
                          .Intersect(from car in queue.ElementAt(2)
                                     select car)).ToList();
        }
        public static double AverageReleaseYearOfCars_EM(Queue<List<Car>> queue)
        {
            return queue.SelectMany(x => x).Average(car => car.Year);
        }
        public static double AverageReleaseYearOfCars_LINQ(Queue<List<Car>> queue)
        {
            return (from list in queue
                    from car in list
                    select car.Year).Average();
        }
        public static List<IGrouping<int, Car>> GroupByYear_EM(Queue<List<Car>> queue)
        {
            return queue.SelectMany(x => x).GroupBy(car => car.Year).ToList();
        }
        public static List<IGrouping<int, Car>> GroupByYear_LINQ(Queue<List<Car>> queue)
        {
            return (from list in queue
                    from car in list
                    group car by car.Year).ToList();
        }
        public static List<CarTypeCount> CarTypes_EM(Queue<List<Car>> queue)
        {
            return queue.SelectMany(x => x).GroupBy(y=>y.GetType()).Select(z => new CarTypeCount { CarType = z.Key, TotalCount = z.Count() }).ToList();
        }
        public static List<CarTypeCount> CarTypes_LINQ(Queue<List<Car>> queue)
        {
            var brandGroup = from list in queue
                             from car in list
                             group car by car.GetType();
            return (from list in brandGroup
                    let totalCount = list.Count()
                    select new CarTypeCount { CarType = list.Key, TotalCount = totalCount }).ToList();
        }
        public static List<Brand> GetBrandInfo_EM(Queue<List<Car>> queue)
        {
            List<Brand> brands = new List<Brand>();
            string[] brandnames = { "Toyota", "Honda", "Ford", "BMW", "Mercedes-Benz", "Audi", "Volkswagen", "Chevrolet", "Hyundai", "Nissan" };
            foreach (string name in brandnames)
            {
                brands.Add(new Brand(name));
            }
            brands.Add(new Brand("Toyota"));

            var cars = queue.SelectMany(workshop => workshop).ToList();

            var brandInfo = brands
                .GroupJoin(cars,
                           brand => brand.Name,
                           car => car.Brand,
                           (brand, carGroup) => new Brand(brand.Name, carGroup.Count(), carGroup.Any() ? carGroup.Average(c => c.Price) : 0))
                .ToList();

            return brandInfo;
        }
        public static List<Brand> GetBrandInfo_LINQ(Queue<List<Car>> queue)
        {
            List<Brand> brands = new List<Brand>();
            string[] brandnames = { "Toyota", "Honda", "Ford", "BMW", "Mercedes-Benz", "Audi", "Volkswagen", "Chevrolet", "Hyundai", "Nissan" };
            foreach (string name in brandnames)
            {
                brands.Add(new Brand(name));
            }
            brands.Add(new Brand("Toyota"));
            var cars = queue.SelectMany(workshop => workshop).ToList();
            var brandInfo = from brand in brands
                            join car in cars on brand.Name equals car.Brand into carGroup
                            select new Brand(brand.Name, carGroup.Count(), carGroup.Any() ? carGroup.Average(c => c.Price) : 0);
            return brandInfo.ToList();
        }
        public static bool ProcessCarQueries<T>(
        Queue<List<Car>> queue,
        Func<Queue<List<Car>>, T> extensionMethodQuery,
        Func<Queue<List<Car>>, T> linqQuery,
        Action<T> displayResult,
        string message,
        Predicate<T> isEmpty,
        string errorMessage)
        {
            if (queue.Count > 0)
            {
                T EM = extensionMethodQuery(queue);
                T LINQ = linqQuery(queue);

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
                Console.WriteLine("Завод не создан!");
                return false;
            }
        }
        public static bool AddSameCarToAllWorkshops(ref Queue<List<Car>> queue)
        {
            if (queue.Count > 0)
            {
                Car item = new Car();
                item.RandomInit();
                queue.ElementAt(0).Add(item);
                queue.ElementAt(1).Add((Car)item.Clone());
                queue.ElementAt(2).Add((Car)item.Clone());
                Console.WriteLine("Одинаковые машины были добавлены в каждый цех завода.");
                return true;
            }
            else
            {
                Console.WriteLine("Завод не создан!");
                return false;
            }
        }
        static void Main(string[] args)
        {
            string Menu = "\nВыберите действие:\n" +
                         "1. Создать завод по изготовлению автомобилей с тремя цехами.\n" +
                         "2. Вывести завод на экран.\n" +
                         "3. Добавить во все цеха по одинаковой машине (для проверки пересечения).\n" +
                         "4. Выбрать все автомобили, выпущенные после 2000 года (Where).\n" +
                         "5. Найти пересечение автомобилей между двумя цехами (Intersect).\n" +
                         "6. Найти средний год выпуска всех автомобилей (Average).\n" +
                         "7. Сгруппировать автомобили по году выпуска (Group By).\n" +
                         "8. Создать новый тип - тип автомобиля, включающий название типа и кол-во созданных объектов (Let).\n" +
                         "9. Вывести информацию о марках машин, указав количество автомобилей в каждой марке и их среднюю цену. (Join).\n" +
                         "10. Выход.\n";
            Queue<List<Car>> autoFactory = new Queue<List<Car>>();
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
                            Console.WriteLine("Введите вместимость цехов:");
                            fillQueue(
                                ref autoFactory,
                                VHS.Input("Ошибка! Введите натуральное число больше 1!", x => 1 < x));
                            break;
                        case 2:
                            PrintQueue(autoFactory);
                            break;
                        case 3:
                            AddSameCarToAllWorkshops(ref autoFactory);
                            break;
                        case 4:
                            ProcessCarQueries(
                                autoFactory,
                                ChooseAllAutosReleasedAfter2000_EM,
                                ChooseAllAutosReleasedAfter2000_LINQ,
                                x => x.ForEach(item => Console.WriteLine(item)),
                                "Выборка",
                                x => x.Count <= 0,
                                "Машин выпушенных с 2000 года не было найдено!"
                            );
                            break;
                        case 5:
                            ProcessCarQueries(
                                autoFactory,
                                FindIntersectionsInWorkshops_EM,
                                FindIntersectionsInWorkshops_LINQ,
                                x => x.ForEach(item => Console.WriteLine(item)),
                                "Пересечение",
                                x => x.Count <= 0,
                                "Пересечение не было найдено!"
                            );
                            break;
                        case 6:
                            ProcessCarQueries(
                                autoFactory,
                                AverageReleaseYearOfCars_EM,
                                AverageReleaseYearOfCars_LINQ,
                                x => Console.WriteLine($"{x:f0}"),
                                "Средний год выпуска всех автомобилей",
                                x => x == 0,
                                "Средний год не найден!"
                            );
                            break;
                        case 7:
                            ProcessCarQueries(
                                autoFactory,
                                GroupByYear_EM,
                                GroupByYear_LINQ,
                                x => x.ForEach(group => { Console.WriteLine($"{group.Key} ({group.Count()}):"); foreach (var item in group) { Console.WriteLine($"      {item}"); } }),
                                "Группировка всех авто по году выпуска",
                                x => x.Count <= 0,
                                "Группировку невозможно выполнить!"
                            );
                            break;
                        case 8:
                            ProcessCarQueries(
                                autoFactory,
                                CarTypes_EM,
                                CarTypes_LINQ,
                                x => x.ForEach(y => Console.WriteLine($"Тип машины = {y.CarType}, Кол-во созданных объектов: {y.TotalCount}")),
                                "Типы авто и количество созданных объектов каждого типа",
                                x => x.Count <= 0,
                                "Типы невозможно выделить!"
                            );
                            break;
                        case 9:
                            ProcessCarQueries(
                                autoFactory,
                                GetBrandInfo_EM,
                                GetBrandInfo_LINQ,
                                x => x.ForEach(y => Console.WriteLine(y)),
                                "Марки машин с количеством автомобилей в каждой марке и средней ценой по марке",
                                x => x.Count <= 0,
                                "Марки не найдены!"
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
