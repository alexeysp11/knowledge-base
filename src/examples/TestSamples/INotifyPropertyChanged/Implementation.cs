using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TestSamples
{
    // Реализуйте интерфейс INotifyPropertyChanged,
    // чтобы к свойствам Employee можно было привязываться.
    // Реализация свойств должна быть максимально короткой.
    public class Employee : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private int _age;

        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (_lastName != value)
                {
                    _lastName = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(FullName));
                }
            }
        }

        public string FullName => FirstName + LastName;

        public int Age
        {
            get => _age;
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}