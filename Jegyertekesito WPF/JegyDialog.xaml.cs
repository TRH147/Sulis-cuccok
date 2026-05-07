// JegyDialog.xaml.cs
using System.Windows;
using Jegyertekesito.Models;

namespace Jegyertekesito
{
    public partial class JegyDialog : Window
    {
        public Ticket? Ticket { get; private set; }
        private readonly Ticket? _existingTicket; // Meglévő jegy szerkesztés esetén

        // KONSTRUKTOR: paraméterben kaphat egy meglévő jegyet szerkesztéshez
        public JegyDialog(Ticket? existingTicket = null)
        {
            InitializeComponent();
            _existingTicket = existingTicket;

            if (existingTicket != null)
            {
                // SZERKESZTÉSI MÓD
                this.Title = "Jegy szerkesztése";

                // Mezők előtöltése a meglévő adatokkal
                NameTextBox.Text = existingTicket.Nev;
                PriceTextBox.Text = existingTicket.Ar.ToString("F2"); // Két tizedesjegy formázás
                QuantityTextBox.Text = existingTicket.Darabszam.ToString();

                // Név mező kapja a fókuszt, és legyen kijelölve a szöveg
                NameTextBox.Focus();
                NameTextBox.SelectAll();
            }
            else
            {
                // HOZZÁADÁSI MÓD
                this.Title = "Új jegy hozzáadása";
                NameTextBox.Focus(); // Név mező kapja a fókuszt
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // ----- VALIDÁCIÓ -----

            // 1. Név validálása
            string nev = NameTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(nev))
            {
                MessageBox.Show("A jegynév nem lehet üres!",
                    "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                NameTextBox.Focus();
                return;
            }

            // 2. Ár validálása
            if (!decimal.TryParse(PriceTextBox.Text, out decimal ar))
            {
                MessageBox.Show("Az árnak érvényes tizedes számnak kell lennie!",
                    "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                PriceTextBox.Focus();
                PriceTextBox.SelectAll();
                return;
            }
            if (ar < 0)
            {
                MessageBox.Show("Az ár nem lehet negatív!",
                    "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                PriceTextBox.Focus();
                PriceTextBox.SelectAll();
                return;
            }

            // 3. Darabszám validálása
            if (!int.TryParse(QuantityTextBox.Text, out int darabszam))
            {
                MessageBox.Show("A darabszámnak érvényes egész számnak kell lennie!",
                    "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                QuantityTextBox.Focus();
                QuantityTextBox.SelectAll();
                return;
            }
            if (darabszam < 0)
            {
                MessageBox.Show("A darabszám nem lehet negatív!",
                    "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                QuantityTextBox.Focus();
                QuantityTextBox.SelectAll();
                return;
            }

            // ----- JEGY OBJEKTUM LÉTREHOZÁSA -----
            Ticket = new Ticket
            {
                Nev = nev,
                Ar = ar,
                Darabszam = darabszam
            };

            // Ha szerkesztés történt, tartsuk meg az ID-t
            if (_existingTicket != null)
            {
                Ticket.Id = _existingTicket.Id;
            }

            // Sikeres validáció -> ablak bezárása DialogResult = true értékkel
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Mégsem -> ablak bezárása DialogResult = false értékkel (alapértelmezett)
            DialogResult = false;
            Close();
        }
    }
}