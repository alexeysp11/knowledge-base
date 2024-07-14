# ienumrable


## Разница между массивом и списком

- Массив имеет фиксированный размер, в то время как список (List) может динамически изменяться.
- Список предоставляет более широкий набор методов для управления данными, чем массив.

## Реализация интерфейса IEnumerable

Для реализации интерфейса `IEnumerable` в C# нужно определить метод GetEnumerator(), который возвращает объект, реализующий интерфейс `IEnumerator`. Пример:
```C#
    public class MyCollection : IEnumerable
    {
        private int[] items = { 1, 2, 3, 4, 5 };

        public IEnumerator GetEnumerator()
        {
            return new MyEnumerator(items);
        }
    }

    public class MyEnumerator : IEnumerator
    {
        private int[] items;
        private int position = -1;

        public MyEnumerator(int[] items)
        {
            this.items = items;
        }

        public bool MoveNext()
        {
            position++;
            return position < items.Length;
        }

        public object Current
        {
            get { return items[position]; }
        }

        public void Reset()
        {
            position = -1;
        }
    }
```
    
