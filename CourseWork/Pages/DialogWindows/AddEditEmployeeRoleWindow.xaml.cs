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
    public partial class AddEditEmployeeRoleWindow : Window
    {
        private DictEmployeeRole _role;
        private VendingDbContext _db = new();

        public AddEditEmployeeRoleWindow()
        {
            InitializeComponent();

            if (DataEmployeeRole.role != null)
            {
                _role = _db.DictEmployeeRoles.FirstOrDefault(r => r.RoleId == DataEmployeeRole.role.RoleId);

                tbTitle.Text = "Изменение должности";
                btnSave.Content = "Сохранить";
            }
            else
            {
                _role = new();
                tbTitle.Text = "Создание должности";
                btnSave.Content = "Создать";
            }

            DataContext = _role;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataEmployeeRole.role != null)
                {
                    _db.SaveChanges();
                }
                else
                {
                    _db.DictEmployeeRoles.Add(_role);
                    _db.SaveChanges();
                }
                MessageBox.Show("Должность успешно сохранена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
                        message.AppendLine("Не заполнено обязательное поле (Название должности).");
                    }
                    else if (sqlEx.Number == 2601 || sqlEx.Number == 2627)
                    {
                        message.AppendLine("Должность с таким названием уже существует.");
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