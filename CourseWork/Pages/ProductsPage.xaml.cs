using CourseWork.Classes;
using CourseWork.Models;
using CourseWork.Pages.DialogWindows;
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
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            using (var _db = new VendingDbContext())
            {
                var products = _db.Products
                                 .Include(p => p.Category)
                                 .ToList();

                lvProducts.ItemsSource = products;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить продукт?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                var product = button.DataContext as Product;

                using (var db = new VendingDbContext())
                {
                    var p = db.Products.FirstOrDefault(x => x.ProductId == product.ProductId);
                    if (p != null)
                    {
                        try
                        {
                            db.Products.Remove(p);
                            db.SaveChanges();
                        }
                        catch
                        {
                            MessageBox.Show("Невозможно удалить продукт, так как он используется в автоматах или продажах.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                RefreshData();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataProduct.product = null;
            AddEditProductWindow w = new();
            w.ShowDialog();
            RefreshData();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            var product = btn.DataContext as Product;

            DataProduct.product = product;
            AddEditProductWindow w = new();
            w.ShowDialog();
            RefreshData();
        }
    }
}