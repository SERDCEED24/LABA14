using CarsLibrary;
using MyCollectionLibrary;
using L14_2;

namespace L14_2_Test
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void ProcessCarQueries_WithNonEmptyCollection_ReturnsTrue()
        {
            // Arrange
            MyCollection<Car> collection = null;
            Program.fillCollection(ref collection, 10);
            Func<MyCollection<Car>, int> extensionMethodQuery = Program.PriceOfAllCars_EM;
            Func<MyCollection<Car>, int> linqQuery = Program.PriceOfAllCars_LINQ;
            Action<int> displayResult = p => Console.WriteLine(p);
            string message = "Test Message";
            Predicate<int> isEmpty = p => p == 0;
            string errorMessage = "Test Error Message";

            // Act
            var result = Program.ProcessCarQueries(collection, extensionMethodQuery, linqQuery, displayResult, message, isEmpty, errorMessage);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ProcessCarQueries_WithEmptyCollection_ReturnsFalse()
        {
            // Arrange
            MyCollection<Car> collection = new MyCollection<Car>();
            Func<MyCollection<Car>, List<Car>> extensionMethodQuery = Program.ChooseAllAutosReleasedAfter2000_EM;
            Func<MyCollection<Car>, List<Car>> linqQuery = Program.ChooseAllAutosReleasedAfter2000_LINQ;
            Action<List<Car>> displayResult = cars => Console.WriteLine(string.Join(", ", cars));
            string message = "Test Message";
            Predicate<List<Car>> isEmpty = cars => cars.Count == 0;
            string errorMessage = "Test Error Message";

            // Act
            var result = Program.ProcessCarQueries(collection, extensionMethodQuery, linqQuery, displayResult, message, isEmpty, errorMessage);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ProcessCarQueries_WithEmptyQueries_ReturnsFalse()
        {
            // Arrange
            MyCollection<Car> collection = new MyCollection<Car>();
            Program.fillCollection(ref collection, 10);
            Func<MyCollection<Car>, List<Car>> extensionMethodQuery = col => new List<Car>();
            Func<MyCollection<Car>, List<Car>> linqQuery = col => new List<Car>(); 
            Action<List<Car>> displayResult = cars => Console.WriteLine(string.Join(", ", cars));
            string message = "Test Message";
            Predicate<List<Car>> isEmpty = cars => cars.Count == 0;
            string errorMessage = "Test Error Message";

            // Act
            var result = Program.ProcessCarQueries(collection, extensionMethodQuery, linqQuery, displayResult, message, isEmpty, errorMessage);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void FillCollection_CreatesCorrectNumberOfCars()
        {
            // Arrange
            MyCollection<Car> collection = null;
            int expectedCount = 5;

            // Act
            var result = Program.fillCollection(ref collection, expectedCount);

            // Assert
            Assert.IsTrue(result);
            Assert.IsNotNull(collection);
            Assert.AreEqual(expectedCount, collection.Count);
        }

        [TestMethod]
        public void ChooseAllAutosReleasedAfter2000_EM_ReturnsCorrectCars()
        {
            // Arrange
            var collection = new MyCollection<Car>
            {
                new Car { Year = 1999 },
                new Car { Year = 2001 },
                new Car { Year = 2005 },
                new Car { Year = 2000 }
            };

            // Act
            var result = Program.ChooseAllAutosReleasedAfter2000_EM(collection);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.TrueForAll(car => car.Year > 2000));
        }

        [TestMethod]
        public void ChooseAllAutosReleasedAfter2000_LINQ_ReturnsCorrectCars()
        {
            // Arrange
            var collection = new MyCollection<Car>
            {
                new Car { Year = 1999 },
                new Car { Year = 2001 },
                new Car { Year = 2005 },
                new Car { Year = 2000 }
            };

            // Act
            var result = Program.ChooseAllAutosReleasedAfter2000_LINQ(collection);

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.TrueForAll(car => car.Year > 2000));
        }

        [TestMethod]
        public void CountAllTrucks_EM_ReturnsCorrectCount()
        {
            // Arrange
            var collection = new MyCollection<Car>
            {
                new Truck(),
                new Car(),
                new Truck(),
                new PassengerCar()
            };

            // Act
            var result = Program.CountAllTrucks_EM(collection);

            // Assert
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void CountAllTrucks_LINQ_ReturnsCorrectCount()
        {
            // Arrange
            var collection = new MyCollection<Car>
            {
                new Truck(),
                new Car(),
                new Truck(),
                new PassengerCar()
            };

            // Act
            var result = Program.CountAllTrucks_LINQ(collection);

            // Assert
            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void PriceOfAllCars_EM_ReturnsCorrectSum()
        {
            // Arrange
            var collection = new MyCollection<Car>
            {
                new Car { Price = 10000 },
                new Car { Price = 20000 },
                new Car { Price = 30000 }
            };

            // Act
            var result = Program.PriceOfAllCars_EM(collection);

            // Assert
            Assert.AreEqual(60000, result);
        }

        [TestMethod]
        public void PriceOfAllCars_LINQ_ReturnsCorrectSum()
        {
            // Arrange
            var collection = new MyCollection<Car>
            {
                new Car { Price = 10000 },
                new Car { Price = 20000 },
                new Car { Price = 30000 }
            };

            // Act
            var result = Program.PriceOfAllCars_LINQ(collection);

            // Assert
            Assert.AreEqual(60000, result);
        }
        [TestMethod]
        public void NewestCars_EM_ReturnsCorrectCars()
        {
            // Arrange
            var collection = new MyCollection<Car>
            {
                new Car { Year = 2000 },
                new Car { Year = 2005 },
                new Car { Year = 2010 },
                new Car { Year = 2015 }
            };

            // Act
            var result = Program.NewestCars_EM(collection);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2015, result[0].Year);
        }

        [TestMethod]
        public void NewestCars_LINQ_ReturnsCorrectCars()
        {
            // Arrange
            var collection = new MyCollection<Car>
            {
                new Car { Year = 2000 },
                new Car { Year = 2005 },
                new Car { Year = 2010 },
                new Car { Year = 2015 }
            };

            // Act
            var result = Program.NewestCars_LINQ(collection);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2015, result[0].Year);
        }

        [TestMethod]
        public void OldestCars_EM_ReturnsCorrectCars()
        {
            // Arrange
            var collection = new MyCollection<Car>
            {
                new Car { Year = 2000 },
                new Car { Year = 2005 },
                new Car { Year = 2010 },
                new Car { Year = 2015 }
            };

            // Act
            var result = Program.OldestCars_EM(collection);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2000, result[0].Year);
        }

        [TestMethod]
        public void OldestCars_LINQ_ReturnsCorrectCars()
        {
            // Arrange
            var collection = new MyCollection<Car>
            {
                new Car { Year = 2000 },
                new Car { Year = 2005 },
                new Car { Year = 2010 },
                new Car { Year = 2015 }
            };

            // Act
            var result = Program.OldestCars_LINQ(collection);

            // Assert
            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(2000, result[0].Year);
        }

        [TestMethod]
        public void AveragePriceOfPassengerCars_EM_ReturnsCorrectAverage()
        {
            // Arrange
            var collection = new MyCollection<Car>
            {
                new PassengerCar { Price = 10000 },
                new PassengerCar { Price = 20000 },
                new PassengerCar { Price = 30000 }
            };

            // Act
            var result = Program.AveragePriceOfPassengerCars_EM(collection);

            // Assert
            Assert.AreEqual(20000, result);
        }

        [TestMethod]
        public void AveragePriceOfPassengerCars_LINQ_ReturnsCorrectAverage()
        {
            // Arrange
            var collection = new MyCollection<Car>
            {
                new PassengerCar { Price = 10000 },
                new PassengerCar { Price = 20000 },
                new PassengerCar { Price = 30000 }
            };

            // Act
            var result = Program.AveragePriceOfPassengerCars_LINQ(collection);

            // Assert
            Assert.AreEqual(20000, result);
        }

        [TestMethod]
        public void GroupByYear_EM_ReturnsCorrectGroups()
        {
            // Arrange
            var collection = new MyCollection<Car>
            {
                new Car { Year = 2000 },
                new Car { Year = 2000 },
                new Car { Year = 2010 },
                new Car { Year = 2010 },
                new Car { Year = 2015 }
            };

            // Act
            var result = Program.GroupByYear_EM(collection);

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(2, result.Find(group => group.Key == 2000).Count());
            Assert.AreEqual(2, result.Find(group => group.Key == 2010).Count());
            Assert.AreEqual(1, result.Find(group => group.Key == 2015).Count());
        }

        [TestMethod]
        public void GroupByYear_LINQ_ReturnsCorrectGroups()
        {
            // Arrange
            var collection = new MyCollection<Car>
            {
                new Car { Year = 2000 },
                new Car { Year = 2000 },
                new Car { Year = 2010 },
                new Car { Year = 2010 },
                new Car { Year = 2015 }
            };

            // Act
            var result = Program.GroupByYear_LINQ(collection);

            // Assert
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(2, result.FirstOrDefault(group => group.Key == 2000)?.Count() ?? 0);
            Assert.AreEqual(2, result.FirstOrDefault(group => group.Key == 2010)?.Count() ?? 0);
            Assert.AreEqual(1, result.FirstOrDefault(group => group.Key == 2015)?.Count() ?? 0);
        }
    }
}