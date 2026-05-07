// MainWindow.xaml.cs
using System.Windows;
using Jegyertekesito.Models;
using Jegyertekesito.Services;

namespace Jegyertekesito
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadTickets(); // Táblázat feltöltése induláskor
        }

        // Adatok betöltése a DataGrid-be
        private void LoadTickets()
        {
            TicketsGrid.ItemsSource = null; // Először töröljük a régi adatokat
            TicketsGrid.ItemsSource = DatabaseHelper.GetAllTickets(); // Újratöltés
        }

        // HOZZÁADÁS
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new JegyDialog(); // Paraméter nélkül -> hozzáadási mód
            dialog.Owner = this; // Szülőablak beállítása
            bool? result = dialog.ShowDialog();

            if (result == true && dialog.Ticket != null)
            {
                DatabaseHelper.AddTicket(dialog.Ticket);
                LoadTickets(); // Táblázat frissítése
                MessageBox.Show("Jegy sikeresen hozzáadva!", "Siker",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // SZERKESZTÉS
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Ellenőrizzük, hogy van-e kijelölt sor
            if (TicketsGrid.SelectedItem is not Ticket selectedTicket)
            {
                MessageBox.Show("Kérem, válasszon ki egy jegyet a szerkesztéshez!",
                    "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Dialógus megnyitása a kijelölt jegy adataival -> szerkesztési mód
            var dialog = new JegyDialog(selectedTicket);
            dialog.Owner = this;
            bool? result = dialog.ShowDialog();

            if (result == true && dialog.Ticket != null)
            {
                DatabaseHelper.UpdateTicket(dialog.Ticket);
                LoadTickets(); // Táblázat frissítése
                MessageBox.Show("Jegy sikeresen módosítva!", "Siker",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // TÖRLÉS
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Ellenőrizzük, hogy van-e kijelölt sor
            if (TicketsGrid.SelectedItem is not Ticket selectedTicket)
            {
                MessageBox.Show("Kérem, válasszon ki egy törölni kívánt jegyet!",
                    "Figyelmeztetés", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Megerősítő üzenet a jegy nevével
            string message = $"Biztosan törölni szeretné a következő jegyet?\n\n" +
                             $"Név: {selectedTicket.Nev}\n" +
                             $"Ár: {selectedTicket.Ar:N0} Ft\n" +
                             $"Darabszám: {selectedTicket.Darabszam}";

            MessageBoxResult confirmation = MessageBox.Show(
                message,
                "Törlés megerősítése",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (confirmation == MessageBoxResult.Yes)
            {
                DatabaseHelper.DeleteTicket(selectedTicket.Id);
                LoadTickets(); // Táblázat frissítése
                MessageBox.Show("Jegy sikeresen törölve!", "Siker",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}