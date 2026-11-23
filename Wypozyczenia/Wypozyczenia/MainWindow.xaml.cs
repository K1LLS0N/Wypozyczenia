using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Wypozyczenia
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Sprzet> SprzetList { get; } = new();
        public MainWindow()
        {
            SQLitePCL.Batteries.Init();
            InitializeComponent();
            DataContext = this;
            try
            {
                foreach (var s in Database.GetSprzet())
                    SprzetList.Add(s);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Nie udało się otworzyć bazy danych:\n" + ex.Message,"Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void DodajSprzet_Click(object sender, RoutedEventArgs e)
        {
            var okno = new DodajSprzet { Owner = this };
            if (okno.ShowDialog() == true && okno.NowySprzet != null)
                SprzetList.Add(okno.NowySprzet);

        }
    }
}