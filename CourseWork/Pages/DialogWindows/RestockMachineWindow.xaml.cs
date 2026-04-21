using CourseWork.Classes;
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
using System.Windows.Shapes;

namespace CourseWork.Pages.DialogWindows
{
    /// <summary>
    /// Логика взаимодействия для RestockMachineWindow.xaml
    /// </summary>
    public partial class RestockMachineWindow : Window
    {
        private VendingMachine _machine;
        private VendingDbContext _db = new();
        public RestockMachineWindow()
        {
            InitializeComponent();
            cbEmployee.ItemsSource = _db.Employees.ToList();
            cbEmployee.DisplayMemberPath = "FullName";
            cbProduct.ItemsSource = _db.Products.ToList();
            cbProduct.DisplayMemberPath = "Name";

            _machine = DataMachine.machine;
        }

        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            if (cbEmployee.SelectedIndex != -1 && cbProduct.SelectedIndex != -1 && !string.IsNullOrWhiteSpace(tbCount.Text))
            {
                bool isCorrect = int.TryParse(tbCount.Text, out int count);
                if (isCorrect)
                {
                    try
                    {
                        var employeeId = ((Employee)cbEmployee.SelectedItem).EmployeeId;
                        var machineId = _machine.MachineId;
                        var productId = ((Product)cbProduct.SelectedItem).ProductId;
                        _db.Database.ExecuteSqlRaw("EXEC sp_RestockMachine @p0, @p1, @p2, @p3", employeeId, machineId, productId, count);

                        MessageBox.Show("Пополнение прошло успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                        this.Close();
                    }
                    catch 
                    {
                        MessageBox.Show("Возникла ошибка при выполнении процедуры", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Введите корректное значение в поле Количество", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
