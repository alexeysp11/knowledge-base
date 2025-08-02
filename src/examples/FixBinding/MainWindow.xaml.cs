using System.Collections.ObjectModel;
using System.Windows;

namespace FixBindingsSample {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }
    }
    public class MainViewModel {
        public string Property { get; set; }
        public ObservableCollection<Item> Items { get; }
        public MainViewModel() {
            Items = new ObservableCollection<Item>();
            for (int i = 0; i < 100; i++)
                Items.Add(new Item { Id = i, Name = string.Format("Name_{0}", i) });
            Property = "Works";
        }
    }
    public class Item {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}