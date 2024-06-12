using CarsLibrary;
using L14_1;

namespace L14_1_Test
{
    [TestClass]
    public class CarTypeCountTests
    {
        [TestMethod]
        public void Property_CarType_ShouldSetAndGetCorrectly_CarTypeCount()
        {
            // Arrange
            Type expectedType = typeof(string); // Выбранный тип для теста
            CarTypeCount carTypeCount = new CarTypeCount();

            // Act
            carTypeCount.CarType = expectedType;

            // Assert
            Assert.AreEqual(expectedType, carTypeCount.CarType);
        }

        [TestMethod]
        public void Property_TotalCount_ShouldSetAndGetCorrectly_CarTypeCount()
        {
            // Arrange
            int expectedCount = 10;
            CarTypeCount carTypeCount = new CarTypeCount();

            // Act
            carTypeCount.TotalCount = expectedCount;

            // Assert
            Assert.AreEqual(expectedCount, carTypeCount.TotalCount);
        }

        [TestMethod]
        public void Constructor_ShouldInitializePropertiesToDefaultValues_CarTypeCount()
        {
            // Arrange & Act
            CarTypeCount carTypeCount = new CarTypeCount();

            // Assert
            Assert.IsNull(carTypeCount.CarType);
            Assert.AreEqual(0, carTypeCount.TotalCount);
        }

        [TestMethod]
        public void Constructor_ShouldAllowSettingProperties_CarTypeCount()
        {
            // Arrange
            Type expectedType = typeof(int);
            int expectedCount = 5;

            // Act
            CarTypeCount carTypeCount = new CarTypeCount
            {
                CarType = expectedType,
                TotalCount = expectedCount
            };

            // Assert
            Assert.AreEqual(expectedType, carTypeCount.CarType);
            Assert.AreEqual(expectedCount, carTypeCount.TotalCount);
        }
    }
}