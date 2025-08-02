using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSamples
{
    public class Group
    {
        public string Name { get; set; }
        public IEnumerable<Meal> Meals { get; set; }
    }

    public class Meal
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public static class Helper
    {
        // Без использования LINQ реализуйте метод GetFilteredMeals, возвращающий IEnumerable<Meal>,
        // где Meal.Price выше или равна minPrice
        public static IEnumerable<Meal> GetFilteredMeals(IEnumerable<Group> categories, decimal minPrice)
        {
            List<Meal> filteredMeals = new List<Meal>();
            foreach (var group in categories)
            {
                if (group.Meals != null && group.Meals.Any())
                {
                    foreach (var meal in group.Meals)
                    {
                        if (meal.Price >= minPrice)
                        {
                            filteredMeals.Add(meal);
                        }
                    }
                }
            }
            return filteredMeals;
        }
        
        // То же самое, но с использованием LINQ
        public static IEnumerable<Meal> GetFilteredMealsLINQ(IEnumerable<Group> categories, decimal minPrice)
        {
            return categories
                .Where(g => g.Meals != null && g.Meals.Any())
                .SelectMany(g => g.Meals)
                .Where(m => m.Price >= minPrice);
        }

        // Переработайте метод GetFilteredMeals так, чтобы в нем можно было использовать любое условие
        public static IEnumerable<Meal> GetFilteredMealsLINQ(IEnumerable<Group> categories, Func<Meal, bool> predicate)
        {
            return categories.SelectMany(g => g.Meals).Where(predicate);
        }
    }
}