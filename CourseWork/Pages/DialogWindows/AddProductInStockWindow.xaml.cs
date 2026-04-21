using CourseWork.Classes;
using CourseWork.Models;
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
    /// Логика взаимодействия для AddProductInStockWindow.xaml
    /// </summary>
    public partial class AddProductInStockWindow : Window
    {
        private VendingMachine _machine;
        private MachineStock _stock;
        private VendingDbContext _db = new();
        public AddProductInStockWindow()
        {
            InitializeComponent();
            cbProduct.ItemsSource = _db.Products.ToList();
            cbProduct.DisplayMemberPath = "Name";

            _machine = DataMachine.machine;

            _stock = new() { MachineId= _machine.MachineId };

            DataContext = _stock;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (cbProduct.SelectedItem != null)
            {
                try
                {
                    _stock.ProductId = ((Product)cbProduct.SelectedItem).ProductId;

                    _stock.Product = null;
                    _stock.Machine = null;

                    _db.MachineStocks.Add(_stock);
                    _db.SaveChanges();

                    MessageBox.Show("Продукт успешно добавлен в инвентарь!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите продукт.", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
