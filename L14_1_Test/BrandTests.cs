using CarsLibrary;
using L14_1;

namespace L14_1_Test
{
    [TestClass]
    public class BrandTests
    {
        [TestMethod]
        public void Constructor_InitializesNameProperty_Brand()
        {
            // Arrange
            string expectedName = "TestBrand";

            // Act
            Brand brand = new Brand(expectedName);

            // Assert
            Assert.AreEqual(expectedName, brand.Name);
        }

        [TestMethod]
        public void Constructor_InitializesCarCountAndAveragePriceProperties_Brand()
        {
            // Arrange
            string expectedName = "TestBrand";
            int expectedCarCount = 10;
            double expectedAveragePrice = 20000.0;

            // Act
            Brand brand = new Brand(expectedName, expectedCarCount, expectedAveragePrice);

            // Assert
            Assert.AreEqual(expectedName, brand.Name);
            Assert.AreEqual(expectedCarCount, brand.CarCount);
            Assert.AreEqual(expectedAveragePrice, brand.AveragePrice);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectFormat_Brand()
        {
            // Arrange
            string expectedName = "TestBrand";
            int expectedCarCount = 10;
            double expectedAveragePrice = 20000.0;
            string expectedToString = $"Название бренда: {expectedName}, Кол-во созданных объектов: {expectedCarCount}, Средняя цена автомобилей бренда: {expectedAveragePrice}";

            // Act
            Brand brand = new Brand(expectedName, expectedCarCount, expectedAveragePrice);
            string result = brand.ToString();

            // Assert
            Assert.AreEqual(expectedToString, result);
        }

        [TestMethod]
        public void DefaultConstructor_SetsCarCountAndAveragePriceToZero_Brand()
        {
            // Arrange
            string expectedName = "TestBrand";
            int expectedCarCount = 0;
            double expectedAveragePrice = 0.0;

            // Act
            Brand brand = new Brand(expectedName);

            // Assert
            Assert.AreEqual(expectedCarCount, brand.CarCount);
            Assert.AreEqual(expectedAveragePrice, brand.AveragePrice);
        }

        [TestMethod]
        public void Property_SettersWorkCorrectly_Brand()
        {
            // Arrange
            string initialName = "InitialBrand";
            int initialCarCount = 5;
            double initialAveragePrice = 15000.0;

            string newName = "UpdatedBrand";
            int newCarCount = 15;
            double newAveragePrice = 30000.0;

            Brand brand = new Brand(initialName, initialCarCount, initialAveragePrice);

            // Act
            brand.Name = newName;
            brand.CarCount = newCarCount;
            brand.AveragePrice = newAveragePrice;

            // Assert
            Assert.AreEqual(newName, brand.Name);
            Assert.AreEqual(newCarCount, brand.CarCount);
            Assert.AreEqual(newAveragePrice, brand.AveragePrice);
        }
    }
}