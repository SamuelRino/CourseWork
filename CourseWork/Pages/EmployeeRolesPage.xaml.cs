using CourseWork.Classes;
using CourseWork.Models;
using CourseWork.Pages.DialogWindows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Pages
{
    public partial class EmployeeRolesPage : Page
    {
        public EmployeeRolesPage()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            using (var _db = new VendingDbContext())
            {
                var roles = _db.DictEmployeeRoles.ToList();
                lvRoles.ItemsSource = roles;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить должность?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                var role = button.DataContext as DictEmployeeRole;

                using (var db = new VendingDbContext())
                {
                    var r = db.DictEmployeeRoles.FirstOrDefault(x => x.RoleId == role.RoleId);
                    if (r != null)
                    {
                        try
                        {
                            db.DictEmployeeRoles.Remove(r);
                            db.SaveChanges();
                        }
                        catch
                        {
                            MessageBox.Show("Невозможно удалить должность, так как существуют сотрудники, привязанные к ней.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                RefreshData();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataEmployeeRole.role = null;
            AddEditEmployeeRoleWindow w = new();
            w.ShowDialog();
            RefreshData();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            var role = btn.DataContext as DictEmployeeRole;

            DataEmployeeRole.role = role;
            AddEditEmployeeRoleWindow w = new();
            w.ShowDialog();
            RefreshData();
        }
    }
}