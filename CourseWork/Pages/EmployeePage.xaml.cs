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
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            using (var _db = new VendingDbContext())
            {
                var employees = _db.Employees
                                 .Include(e => e.Role)
                                 .ToList();

                lvEmployees.ItemsSource = employees;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить сотрудника?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                var employee = button.DataContext as Employee;

                using (var db = new VendingDbContext())
                {
                    var emp = db.Employees.FirstOrDefault(x => x.EmployeeId == employee.EmployeeId);
                    if (emp != null)
                    {
                        try
                        {
                            db.Employees.Remove(emp);
                            db.SaveChanges();
                        }
                        catch
                        {
                            MessageBox.Show("Невозможно удалить сотрудника, так как он привязан к истории обслуживания или пополнения автоматов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                RefreshData();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataEmployee.employee = null;
            AddEditEmployeeWindow w = new();
            w.ShowDialog();
            RefreshData();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            var employee = btn.DataContext as Employee;

            DataEmployee.employee = employee;
            AddEditEmployeeWindow w = new();
            w.ShowDialog();
            RefreshData();
        }
    }
}