using System;
using System.Collections.Generic;
using System.Text;

namespace Wypozyczenia
{
    public class Sprzet
    {
        public int Id { get; set; }
        public string Nazwa { get; set; } = "";
        public string NumerSeryjny { get; set; } = "";
        public string Status { get; set; } = "Dostępny";
    }
}
