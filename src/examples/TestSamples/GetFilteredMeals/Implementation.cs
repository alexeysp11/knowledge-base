using System;
using System.Collections.Generic;

namespace TestSamples {
    public class Group {
        public string Name { get; set; }
        public IEnumerable<Meal> Meals { get; set; }
    }
    public class Meal {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
    public static class Helper {
        // Без использования LINQ реализуйте метод GetFilteredMeals, возвращающий IEnumerable<Meal>,
        // где Meal.Price выше или равна minPrice
        public static IEnumerable<Meal> GetFilteredMeals(IEnumerable<Group> categories, decimal minPrice) {
            throw new NotImplementedException();
        }
        // То же самое, но с использованием LINQ
        public static IEnumerable<Meal> GetFilteredMealsLINQ(IEnumerable<Group> categories, decimal minPrice) {
            throw new NotImplementedException();
        }
        // Переработайте метод GetFilteredMeals так, чтобы в нем можно было использовать любое условие
        public static IEnumerable<Meal> GetFilteredMealsLINQ(IEnumerable<Group> categories /* ??? */) {
            throw new NotImplementedException();
        }
    }
}