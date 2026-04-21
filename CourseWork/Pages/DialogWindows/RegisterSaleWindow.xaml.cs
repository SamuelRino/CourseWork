using CourseWork.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Pages.DialogWindows
{
    public partial class RegisterSaleWindow : Window
    {
        private VendingDbContext _db = new();

        public RegisterSaleWindow()
        {
            InitializeComponent();

            cbMachine.ItemsSource = _db.VendingMachines.ToList();
            cbMachine.DisplayMemberPath = "SerialNumber";

            cbPaymentMethod.ItemsSource = _db.DictPaymentMethods.ToList();
            cbPaymentMethod.DisplayMemberPath = "MethodName";
        }

        private void cbMachine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbMachine.SelectedItem is VendingMachine selectedMachine)
            {
                cbProduct.ItemsSource = _db.MachineStocks
                    .Where(s => s.MachineId == selectedMachine.MachineId && s.Quantity > 0)
                    .Select(s => s.Product)
                    .ToList();

                cbProduct.DisplayMemberPath = "Name";
            }
        }

        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            if (cbMachine.SelectedIndex != -1 && cbProduct.SelectedIndex != -1 && cbPaymentMethod.SelectedIndex != -1)
            {
                try
                {
                    var machineId = ((VendingMachine)cbMachine.SelectedItem).MachineId;
                    var productId = ((Product)cbProduct.SelectedItem).ProductId;
                    var paymentMethodId = ((DictPaymentMethod)cbPaymentMethod.SelectedItem).MethodId;

                    _db.Database.ExecuteSqlRaw("EXEC sp_RegisterSale @p0, @p1, @p2", machineId, productId, paymentMethodId);

                    MessageBox.Show("Продажа успешно зарегистрирована!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Возникла ошибка при регистрации продажи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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