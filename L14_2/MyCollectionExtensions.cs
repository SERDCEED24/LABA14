using CarsLibrary;
using MyCollectionLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L14_2
{
    public static class MyCollectionExtensions
    {
        public static bool InCollectionOrAdd<T>(this MyCollection<T> collection, T item) where T : IInit, ICloneable, new()
        {
            if (collection.FindItem(item) == -1)
            {
                collection.Add(item);
                return false;
            }
            else
            {
                return true;
            }
        }
        public static void ForEach<T>(this MyCollection<T> collection, Action<T> action) where T : IInit, ICloneable, new()
        {
            foreach (T item in collection)
            {
                action(item);
            }
        }
        public static bool Replace<T>(this MyCollection<T> collection, T itemToReplace, T newItem) where T : IInit, ICloneable, new()
        {
            if (!collection.Contains(itemToReplace))
            {
                throw new Exception("Заменяемый элемент не найден!");
            }
            else
            {
                collection.Remove(itemToReplace);
                collection.Add(newItem);
                return true;
            }
        }
        public static MyCollection<T> Glue<T>(this MyCollection<T> mainCollection, MyCollection<T> secondaryCollection) where T : IInit, ICloneable, new()
        {
            MyCollection<T> collection = new MyCollection<T>(mainCollection);
            foreach (T item in secondaryCollection)
            {
                collection.Add((T)item.Clone());
            }
            return collection;
        }
    }
}
