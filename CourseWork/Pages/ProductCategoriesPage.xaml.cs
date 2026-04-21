using CourseWork.Classes;
using CourseWork.Models;
using CourseWork.Pages.DialogWindows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Pages
{
    public partial class ProductCategoriesPage : Page
    {
        public ProductCategoriesPage()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            using (var _db = new VendingDbContext())
            {
                var categories = _db.DictProductCategories.ToList();
                lvCategories.ItemsSource = categories;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить категорию?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                var category = button.DataContext as DictProductCategory;

                using (var db = new VendingDbContext())
                {
                    var cat = db.DictProductCategories.FirstOrDefault(x => x.CategoryId == category.CategoryId);
                    if (cat != null)
                    {
                        try
                        {
                            db.DictProductCategories.Remove(cat);
                            db.SaveChanges();
                        }
                        catch
                        {
                            MessageBox.Show("Невозможно удалить категорию, так как существуют продукты, привязанные к ней.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                RefreshData();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataProductCategory.category = null;
            AddEditProductCategoryWindow w = new();
            w.ShowDialog();
            RefreshData();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            var category = btn.DataContext as DictProductCategory;

            DataProductCategory.category = category;
            AddEditProductCategoryWindow w = new();
            w.ShowDialog();
            RefreshData();
        }
    }
}