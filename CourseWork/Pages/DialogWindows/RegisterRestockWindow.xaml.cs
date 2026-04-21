using CourseWork.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;

namespace CourseWork.Pages.DialogWindows
{
    public partial class RegisterRestockWindow : Window
    {
        private VendingDbContext _db = new();

        public RegisterRestockWindow()
        {
            InitializeComponent();

            cbMachine.ItemsSource = _db.VendingMachines.Where(m => m.IsDeleted == false).ToList();
            cbMachine.DisplayMemberPath = "SerialNumber";

            cbEmployee.ItemsSource = _db.Employees.ToList();
            cbEmployee.DisplayMemberPath = "FullName";

            cbProduct.ItemsSource = _db.Products.ToList();
            cbProduct.DisplayMemberPath = "Name";
        }

        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            if (cbMachine.SelectedIndex != -1 && cbEmployee.SelectedIndex != -1 && cbProduct.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(tbCount.Text))
            {
                if (int.TryParse(tbCount.Text, out int count) && count > 0)
                {
                    try
                    {
                        var machineId = ((VendingMachine)cbMachine.SelectedItem).MachineId;
                        var employeeId = ((Employee)cbEmployee.SelectedItem).EmployeeId;
                        var productId = ((Product)cbProduct.SelectedItem).ProductId;

                        _db.Database.ExecuteSqlRaw("EXEC sp_RestockMachine @p0, @p1, @p2, @p3",
                            employeeId, machineId, productId, count);

                        MessageBox.Show("Пополнение успешно зарегистрировано!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Возникла ошибка при выполнении процедуры: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Введите корректное значение больше нуля в поле 'Количество'.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}