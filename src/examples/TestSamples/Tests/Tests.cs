using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TestSamples {
    [TestClass]
    public class Tests {
        [TestMethod]
        public void TestComparer() {
            var list = new List<string> { "b@", "b", "a", "c", "@", "a@" };
            var expected = new[] { "a", "b", "c", "@", "a@", "b@" };
            list.Sort(new TestComparer());
            Assert.IsTrue(list.SequenceEqual(expected));
        }

        [TestMethod]
        public void TestGetFilteredMeals() {
            TestGetFilteredMealsCore(Helper.GetFilteredMeals);
        }
        [TestMethod]
        public void TestGetFilteredMealsLINQ() {
            TestGetFilteredMealsCore(Helper.GetFilteredMealsLINQ);
        }
        static void TestGetFilteredMealsCore(Func<IEnumerable<Group>, decimal, IEnumerable<Meal>> getFilteredMeals) {
            var meal1 = new Meal { Price = 500 };
            var meal2 = new Meal { Price = 600 };
            var groups = new List<Group> {
                new Group { Meals = new List<Meal> {
                    meal1,
                    new Meal { Price = 400 },
                    new Meal { Price = 300 }
                } },
                new Group { Meals = new List<Meal> {
                    meal2,
                } },
                new Group { Meals = new List<Meal>() },
                new Group ()
            };
            var expected = new[] { meal1, meal2 };
            var filteredMeals = getFilteredMeals(groups, 500);
            Assert.IsTrue(expected.SequenceEqual(filteredMeals));
        }

        [TestMethod]
        public void TestINotifyPropertyChanged() {
            var employee = new Employee();
            List<string> updatedProperties = new List<string>();
            ((INotifyPropertyChanged)employee).PropertyChanged += (d, e) => updatedProperties.Add(e.PropertyName);
            
            employee.FirstName = "a";
            Assert.IsTrue(new[] { nameof(Employee.FirstName), nameof(Employee.FullName) }.SequenceEqual(updatedProperties));
            
            updatedProperties.Clear();
            employee.LastName = "b";
            Assert.IsTrue(new[] { nameof(Employee.LastName), nameof(Employee.FullName) }.SequenceEqual(updatedProperties));

            updatedProperties.Clear();
            employee.Age = 5;
            Assert.IsTrue(new[] { nameof(Employee.Age) }.SequenceEqual(updatedProperties));
        }
    }
}