using CourseWork.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;

namespace CourseWork.Pages.DialogWindows
{
    public partial class LogMaintenanceWindow : Window
    {
        private VendingDbContext _db = new();

        public LogMaintenanceWindow()
        {
            InitializeComponent();

            cbMachine.ItemsSource = _db.VendingMachines.Where(m => m.IsDeleted == false).ToList();
            cbMachine.DisplayMemberPath = "SerialNumber";

            cbEmployee.ItemsSource = _db.Employees.Where(e => e.RoleId == 3).ToList();
            cbEmployee.DisplayMemberPath = "FullName";
        }

        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            if (cbMachine.SelectedIndex != -1 && cbEmployee.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(tbDescription.Text))
            {
                string costText = string.IsNullOrWhiteSpace(tbCost.Text) ? "0" : tbCost.Text;
                if (decimal.TryParse(costText, out decimal cost) && cost >= 0)
                {
                    try
                    {
                        var machineId = ((VendingMachine)cbMachine.SelectedItem).MachineId;
                        var employeeId = ((Employee)cbEmployee.SelectedItem).EmployeeId;
                        var description = tbDescription.Text;

                        _db.Database.ExecuteSqlRaw("EXEC sp_LogMaintenance @p0, @p1, @p2, @p3",
                            machineId, employeeId, description, cost);

                        MessageBox.Show("Запись об обслуживании успешно добавлена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Возникла ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Введите корректную сумму в поле 'Стоимость материалов'.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Заполните все обязательные поля (Автомат, Сотрудник, Описание).", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}