using CarsLibrary;
using L14_1;

namespace L14_1_Test
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void fillQueue_ShouldCreateQueueWithSpecifiedLength_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            int length = 5;

            // Act
            bool result = Program.fillQueue(ref queue, length);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(3, queue.Count);
            foreach (var workshop in queue)
            {
                Assert.AreEqual(length, workshop.Count);
            }
        }

        [TestMethod]
        public void PrintQueue_ShouldDisplayQueueContents_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            Program.fillQueue(ref queue, 3);
            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);

                // Act
                Program.PrintQueue(queue);

                // Assert
                var output = sw.ToString();
                Assert.IsTrue(output.Contains("Цех 1:"));
                Assert.IsTrue(output.Contains("Цех 2:"));
                Assert.IsTrue(output.Contains("Цех 3:"));
            }
        }

        [TestMethod]
        public void PrintQueue_ShouldHandleEmptyQueue_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);

                // Act
                Program.PrintQueue(queue);

                // Assert
                var output = sw.ToString().Trim();
                Assert.AreEqual("Завод не создан!", output);
            }
        }

        [TestMethod]
        public void ChooseAllAutosReleasedAfter2000_EM_ShouldReturnCars_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            Program.fillQueue(ref queue, 5);

            // Act
            var result = Program.ChooseAllAutosReleasedAfter2000_EM(queue);

            // Assert
            Assert.IsTrue(result.All(car => car.Year > 2000));
        }

        [TestMethod]
        public void ChooseAllAutosReleasedAfter2000_LINQ_ShouldReturnCars_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            Program.fillQueue(ref queue, 5);

            // Act
            var result = Program.ChooseAllAutosReleasedAfter2000_LINQ(queue);

            // Assert
            Assert.IsTrue(result.All(car => car.Year > 2000));
        }

        [TestMethod]
        public void FindIntersectionsInWorkshops_EM_ShouldReturnIntersectedCars_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            Program.fillQueue(ref queue, 5);
            Program.AddSameCarToAllWorkshops(ref queue);

            // Act
            var result = Program.FindIntersectionsInWorkshops_EM(queue);

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void FindIntersectionsInWorkshops_LINQ_ShouldReturnIntersectedCars_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            Program.fillQueue(ref queue, 5);
            Program.AddSameCarToAllWorkshops(ref queue);

            // Act
            var result = Program.FindIntersectionsInWorkshops_LINQ(queue);

            // Assert
            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        public void AverageReleaseYearOfCars_LINQ_Equals_AverageReleaseYearOfCars_EM_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            Program.fillQueue(ref queue, 5);
            var expected = Program.AverageReleaseYearOfCars_EM(queue);

            // Act
            var actual = Program.AverageReleaseYearOfCars_LINQ(queue);

            // Assert
            Assert.IsTrue( expected == actual);
        }

        [TestMethod]
        public void GroupByYear_EM_ShouldGroupCarsByYear_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            Program.fillQueue(ref queue, 5);

            // Act
            var result = Program.GroupByYear_EM(queue);

            // Assert
            Assert.IsTrue(result.All(g => g.Count() > 0));
        }

        [TestMethod]
        public void GroupByYear_LINQ_ShouldGroupCarsByYear_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            Program.fillQueue(ref queue, 5);

            // Act
            var result = Program.GroupByYear_LINQ(queue);

            // Assert
            Assert.IsTrue(result.All(g => g.Count() > 0));
        }

        [TestMethod]
        public void CarTypes_EM_ShouldReturnCarTypeCounts_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            Program.fillQueue(ref queue, 5);

            // Act
            var result = Program.CarTypes_EM(queue);

            // Assert
            Assert.IsTrue(result.All(ct => ct.TotalCount > 0));
        }

        [TestMethod]
        public void CarTypes_LINQ_ShouldReturnCarTypeCounts_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            Program.fillQueue(ref queue, 5);

            // Act
            var result = Program.CarTypes_LINQ(queue);

            // Assert
            Assert.IsTrue(result.All(ct => ct.TotalCount > 0));
        }

        [TestMethod]
        public void GetBrandInfo_EM_ShouldReturnBrandInfo_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            Program.fillQueue(ref queue, 5);

            // Act
            var result = Program.GetBrandInfo_EM(queue);

            // Assert
            Assert.IsTrue(result.All(b => b.CarCount >= 0));
        }

        [TestMethod]
        public void GetBrandInfo_LINQ_ShouldReturnBrandInfo_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            Program.fillQueue(ref queue, 5);

            // Act
            var result = Program.GetBrandInfo_LINQ(queue);

            // Assert
            Assert.IsTrue(result.All(b => b.CarCount >= 0));
        }

        [TestMethod]
        public void AddSameCarToAllWorkshops_ShouldAddCar_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            Program.fillQueue(ref queue, 5);

            // Act
            bool result = Program.AddSameCarToAllWorkshops(ref queue);

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual(6, queue.ElementAt(0).Count);
            Assert.AreEqual(6, queue.ElementAt(1).Count);
            Assert.AreEqual(6, queue.ElementAt(2).Count);
        }

        [TestMethod]
        public void AddSameCarToAllWorkshops_ShouldHandleEmptyQueue_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);

                // Act
                bool result = Program.AddSameCarToAllWorkshops(ref queue);

                // Assert
                var output = sw.ToString().Trim();
                Assert.AreEqual("Завод не создан!", output);
                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void ProcessCarQueries_ShouldHandleEmptyQueue_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            Func<Queue<List<Car>>, List<Car>> dummyQuery = q => new List<Car>();
            Action<List<Car>> dummyDisplay = x => { };
            Predicate<List<Car>> dummyPredicate = x => x.Count == 0;
            string message = "Test message";
            string errorMessage = "Test error message";
            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);

                // Act
                bool result = Program.ProcessCarQueries(queue, dummyQuery, dummyQuery, dummyDisplay, message, dummyPredicate, errorMessage);

                // Assert
                var output = sw.ToString().Trim();
                Assert.AreEqual("Завод не создан!", output);
                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void ProcessCarQueries_ShouldReturnFalseOnEmptyResult_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            Program.fillQueue(ref queue, 5);
            Func<Queue<List<Car>>, List<Car>> emptyQuery = q => new List<Car>();
            Action<List<Car>> dummyDisplay = x => { };
            Predicate<List<Car>> emptyPredicate = x => x.Count == 0;
            string message = "Test message";
            string errorMessage = "Test error message";
            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);

                // Act
                bool result = Program.ProcessCarQueries(queue, emptyQuery, emptyQuery, dummyDisplay, message, emptyPredicate, errorMessage);

                // Assert
                var output = sw.ToString().Trim();
                Assert.AreEqual("Test error message", output);
                Assert.IsFalse(result);
            }
        }

        [TestMethod]
        public void ProcessCarQueries_ShouldReturnTrueWithValidResults_Program()
        {
            // Arrange
            Queue<List<Car>> queue = new Queue<List<Car>>();
            Program.fillQueue(ref queue, 5);
            Func<Queue<List<Car>>, List<Car>> nonEmptyQuery = q => q.SelectMany(x => x).ToList();
            Action<List<Car>> dummyDisplay = x => { };
            Predicate<List<Car>> nonEmptyPredicate = x => x.Count == 0;
            string message = "Test message";
            string errorMessage = "Test error message";
            using (var sw = new System.IO.StringWriter())
            {
                Console.SetOut(sw);

                // Act
                bool result = Program.ProcessCarQueries(queue, nonEmptyQuery, nonEmptyQuery, dummyDisplay, message, nonEmptyPredicate, errorMessage);

                // Assert
                Assert.IsTrue(result);
                var output = sw.ToString().Trim();
                Assert.IsTrue(output.Contains("Test message методами расширения:"));
                Assert.IsTrue(output.Contains("\nTest message LINQ запросами:"));
            }
        }
    }
}