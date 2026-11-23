using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wypozyczenia
{
    /// <summary>
    /// </summary>
    public partial class DodajSprzet : Window
    {
        public Sprzet? NowySprzet { get; private set; }

        public DodajSprzet()
        {
            InitializeComponent();
            NazwaBox.Focus();
        }

        private void Zapisz_Click(object sender, RoutedEventArgs e)
        {
            string nazwa = NazwaBox.Text.Trim();
            string numer = NumerBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(nazwa))
            { PokazBlad("Podaj nazwę sprzętu."); return; }
            if (string.IsNullOrWhiteSpace(numer))
            { PokazBlad("Podaj numer seryjny."); return; }

            try
            {
                if (Database.NumerSeryjnyIstnieje(numer))
                { PokazBlad("Sprzęt z takim numerem seryjnym już istnieje."); return; }

                var sprzet = new Sprzet
                {
                    Nazwa = nazwa,
                    NumerSeryjny = numer,
                    Status = "Dostępny"
                };
                sprzet.Id = Database.AddSprzet(sprzet);

                NowySprzet = sprzet;
                DialogResult = true;
            }
            catch (Exception ex)
            {
                PokazBlad("Błąd zapisu do bazy: " + ex.Message);
            }
        }

        private void Anuluj_Click(object sender, RoutedEventArgs e) => DialogResult = false;

        private void PokazBlad(string tekst)
        {
            BladText.Text = tekst;
            BladText.Visibility = Visibility.Visible;
        }
    }
}
