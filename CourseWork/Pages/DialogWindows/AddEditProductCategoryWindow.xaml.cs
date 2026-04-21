using CourseWork.Classes;
using CourseWork.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Windows;

namespace CourseWork.Pages.DialogWindows
{
    public partial class AddEditProductCategoryWindow : Window
    {
        private DictProductCategory _category;
        private VendingDbContext _db = new();

        public AddEditProductCategoryWindow()
        {
            InitializeComponent();

            if (DataProductCategory.category != null)
            {
                _category = _db.DictProductCategories.FirstOrDefault(c => c.CategoryId == DataProductCategory.category.CategoryId);

                tbTitle.Text = "Изменение категории";
                btnSave.Content = "Сохранить";
            }
            else
            {
                _category = new();
                tbTitle.Text = "Создание категории";
                btnSave.Content = "Создать";
            }

            DataContext = _category;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataProductCategory.category != null)
                {
                    _db.SaveChanges();
                }
                else
                {
                    _db.DictProductCategories.Add(_category);
                    _db.SaveChanges();
                }
                MessageBox.Show("Категория успешно сохранена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (DbUpdateException ex)
            {
                StringBuilder message = new StringBuilder();
                message.AppendLine("Возникла ошибка при сохранении.");

                if (ex.InnerException is SqlException sqlEx)
                {
                    if (sqlEx.Number == 515)
                    {
                        message.AppendLine("Не заполнено обязательное поле (Название категории).");
                    }
                    else if (sqlEx.Number == 2601 || sqlEx.Number == 2627)
                    {
                        message.AppendLine("Категория с таким названием уже существует.");
                    }
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