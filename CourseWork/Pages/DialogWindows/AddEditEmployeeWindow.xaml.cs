using CourseWork.Classes;
using CourseWork.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace CourseWork.Pages.DialogWindows
{
    public partial class AddEditEmployeeWindow : Window
    {
        private Employee _employee;
        private VendingDbContext _db = new();

        public AddEditEmployeeWindow()
        {
            InitializeComponent();

            cbRole.ItemsSource = _db.DictEmployeeRoles.ToList();
            cbRole.DisplayMemberPath = "RoleName";

            if (DataEmployee.employee != null)
            {
                var employees = _db.Employees
                    .Include(e => e.Role);
                _employee = employees.FirstOrDefault(e => e.EmployeeId == DataEmployee.employee.EmployeeId);

                tbTitle.Text = "Изменение данных сотрудника";
                btnSave.Content = "Сохранить";
            }
            else
            {
                _employee = new();
                tbTitle.Text = "Создание сотрудника";
                btnSave.Content = "Создать";
            }

            DataContext = _employee;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataEmployee.employee != null)
                {
                    _db.SaveChanges();
                }
                else
                {
                    _db.Employees.Add(_employee);
                    _db.SaveChanges();
                }
                MessageBox.Show("Данные сотрудника успешно сохранены!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
                        message.AppendLine("Не заполнены обязательные поля (ФИО или Должность).");
                    }
                    else if (sqlEx.Number == 2601 || sqlEx.Number == 2627)
                    {
                        message.AppendLine("Нарушение уникальности данных (возможно, такой Email или Телефон уже существует).");
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