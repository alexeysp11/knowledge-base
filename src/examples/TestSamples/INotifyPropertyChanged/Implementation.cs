namespace TestSamples {
    // Реализуйте интерфейс INotifyPropertyChanged,
    // чтобы к свойствам Employee можно было привязываться.
    // Реализация свойств должна быть максимально короткой.
    public class Employee {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + LastName;
        public int Age { get; set; }
    }
}