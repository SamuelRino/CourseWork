using CourseWork.Classes;
using CourseWork.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseWork.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditMachineWindow.xaml
    /// </summary>
    public partial class AddEditMachineWindow : Window
    {
        private VendingMachine _machine;
        private VendingDbContext _db = new();
        public AddEditMachineWindow()
        {
            InitializeComponent();
            cbLocation.ItemsSource = _db.Locations.ToList();
            cbStatus.ItemsSource = _db.DictMachineStatuses.ToList();
            cbStatus.DisplayMemberPath = "Status";

            if (DataMachine.machine != null)
            {
                var machines = _db.VendingMachines
                    .Include(m => m.Location)
                    .Include(m => m.Status);
                _machine = machines.FirstOrDefault(m => m.StatusId == DataMachine.machine.MachineId);
                //if (_machine.IsDeleted == true) btnRestore.Visibility = Visibility.Visible;
                tbTitle.Text = "Изменение торгового автомата";
                btnSave.Content = "Сохранить";
            }
            else
            {
                _machine = new();
                tbTitle.Text = "Создание торгового автомата";
                btnSave.Content = "Создать";
            }

            DataContext = _machine;
        }

        private void btnRestore_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataMachine.machine != null)
                {
                    _db.SaveChanges();
                }
                else
                {
                    _db.VendingMachines.Add(_machine);
                    _db.SaveChanges();
                }
                MessageBox.Show("Аппарат успешно сохранён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (DbUpdateException ex)
            {
                StringBuilder message = new StringBuilder();
                message.AppendLine("Возникла ошибка при сохранении.");

                int number = ((SqlException)ex.InnerException).Number;

                if (number == 515)
                {
                    message.AppendLine("Не заполнены обязательные поля.");
                }

                if (number == 2601)
                {
                    message.AppendLine("Серийный номер не уникален.");
                }

                MessageBox.Show(message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }       
    }
}
