using System;
using System.Collections.Generic;

namespace TestSamples {
    // Ожидаемый результат:
    // * Строки с символом "@" помещаются в конец коллекции, сортируются в алфавитном порядке;
    // * Строки без символа "@" помещаются в начало коллекции, также сортируются в алфавитном порядке.
    // Например: [ "b@", "b", "a", "c", "@", "a@" ] =>  [ "a", "b", "c", "@", "a@", "b@" ]
    public class TestComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            bool xHasAt = x.Contains("@");
            bool yHasAt = y.Contains("@");

            if (!xHasAt && yHasAt)
            {
                return -1; // x меньше y
            }
            else if (xHasAt && !yHasAt)
            {
                return 1;  // x больше y
            }
            else
            {
                return string.Compare(x, y, StringComparison.Ordinal); // Сравниваем в алфавитном порядке
            }
        }
    }
}