using CourseWork.Classes;
using CourseWork.Models;
using Microsoft.Data.SqlClient;
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
    public partial class AddEditProductWindow : Window
    {
        private Product _product;
        private VendingDbContext _db = new();

        public AddEditProductWindow()
        {
            InitializeComponent();
            cbCategory.ItemsSource = _db.DictProductCategories.ToList();
            cbCategory.DisplayMemberPath = "CategoryName";

            if (DataProduct.product != null)
            {
                var products = _db.Products
                    .Include(p => p.Category);
                _product = products.FirstOrDefault(p => p.ProductId == DataProduct.product.ProductId);

                tbTitle.Text = "Изменение продукта";
                btnSave.Content = "Сохранить";
            }
            else
            {
                _product = new();
                tbTitle.Text = "Создание продукта";
                btnSave.Content = "Создать";
            }

            DataContext = _product;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataProduct.product != null)
                {
                    _db.SaveChanges();
                }
                else
                {
                    _db.Products.Add(_product);
                    _db.SaveChanges();
                }
                MessageBox.Show("Продукт успешно сохранён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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

                if (number == 547)
                {
                    message.AppendLine("Цена должна быть больше нуля.");
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