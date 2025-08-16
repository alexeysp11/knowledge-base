using System.Collections.ObjectModel;
using System.Windows;

namespace SnoopWpf.MainView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class PropertyItem
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public string ValueSource { get; set; }
        }

        public class DiagnosticItem
        {
            public string Level { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Area { get; set; }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            // Fill Properties DataGrid
            ObservableCollection<PropertyItem> properties = new ObservableCollection<PropertyItem>
            {
                new PropertyItem { Name = "Width", Value = "800", ValueSource = "Local" },
                new PropertyItem { Name = "Height", Value = "450", ValueSource = "Local" },
                new PropertyItem { Name = "Title", Value = "Test application: Snoop", ValueSource = "Local" }
            };
            PropertiesDataGrid.ItemsSource = properties;

            // Fill Diagnostics DataGrid
            ObservableCollection<DiagnosticItem> diagnostics = new ObservableCollection<DiagnosticItem>
            {
                new DiagnosticItem { Level = "Info", Name = "Startup", Description = "Application started", Area = "General" },
                new DiagnosticItem { Level = "Warning", Name = "Configuration", Description = "Default configuration loaded", Area = "Configuration" }
            };
            DiagnosticsDataGrid.ItemsSource = diagnostics;
        }
    }
}
