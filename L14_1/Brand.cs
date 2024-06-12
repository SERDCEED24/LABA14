using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L14_1
{
    public class Brand
    {
        // Свойства
        public string Name { get; set; }
        public int CarCount { get; set; }
        public double AveragePrice { get; set; }

        // Конструктор
        public Brand(string name)
        {
            Name = name;
            CarCount = 0;
            AveragePrice = 0;
        }
        public Brand(string name, int carCount, double averagePrice)
        {
            Name = name;
            CarCount = carCount;
            AveragePrice = averagePrice;
        }
        public override string ToString()
        {
            return $"Название бренда: {Name}, Кол-во созданных объектов: {CarCount}, Средняя цена автомобилей бренда: {AveragePrice}";
        }
    }
}
