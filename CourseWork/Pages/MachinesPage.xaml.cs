using CourseWork.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseWork.Pages
{
    /// <summary>
    /// Логика взаимодействия для MachinesPage.xaml
    /// </summary>
    public partial class MachinesPage : Page
    {
        public MachinesPage()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            using (var db = new VendingDbContext())
            {
                var machines = db.VendingMachines
                                 .Include(m => m.Location)
                                 .Include(m => m.Status)
                                 .Where(m => m.IsDeleted == false)
                                 .ToList();

                lvMachines.ItemsSource = machines;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить автомат?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                var machine = button.DataContext as VendingMachine;

                using (var db = new VendingDbContext())
                {
                    var m = db.VendingMachines.FirstOrDefault(x => x.MachineId == machine.MachineId);
                    if (m != null)
                    {
                        m.IsDeleted = true;
                        db.SaveChanges();
                    }
                }
                RefreshData();
            }
        }

        private void btnRestock_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var machine = button.DataContext as VendingMachine;
            int employeeId = 2; // Петров П.П.
            int productId = 1;  // Эспрессо
            int quantityToAdd = 50;

            using (var db = new VendingDbContext())
            {
                db.Database.ExecuteSqlRaw(
                    "EXEC sp_RestockMachine @p0, @p1, @p2, @p3",
                    employeeId, machine.MachineId, productId, quantityToAdd);
            }

            MessageBox.Show("Автомат успешно пополнен!");
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Здесь будет открытие окна добавления автомата (как AddEditMachine в примере)
        }
    }
}
