using CourseWork.Pages;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MachinesPage machinesPage = new();
        private readonly MachineStockPage machineStockPage = new();
        private readonly ProductsPage productsPage = new();
        private readonly EmployeesPage employeesPage = new();
        private readonly LocationsPage locationsPage = new();

        private readonly ProductCategoriesPage productCategoriesPage = new();
        private readonly EmployeeRolesPage employeesRolesPage = new();

        private readonly SalesPage salesPage = new();
        private readonly MaintenanceLogsPage maintenanceLogsPage = new();
        private readonly RestockLogsPage restockLogsPage = new();
        public MainWindow()
        {
            InitializeComponent();
            fMainFrame.Navigate(machinesPage);
        }

        private void btnToMachines_Click(object sender, RoutedEventArgs e)
        {
            fMainFrame.Navigate(machinesPage);
        }

        private void btnToMachineStosk_Click(object sender, RoutedEventArgs e)
        {
            fMainFrame.Navigate(machineStockPage);
        }

        private void btnToProducts_Click(object sender, RoutedEventArgs e)
        {
            fMainFrame.Navigate(productsPage);
        }

        private void btnToEmployees_Click(object sender, RoutedEventArgs e)
        {
            fMainFrame.Navigate(employeesPage);
        }

        private void btnToLocations_Click(object sender, RoutedEventArgs e)
        {
            fMainFrame.Navigate(locationsPage);
        }

        private void btnToDictProductCategories_Click(object sender, RoutedEventArgs e)
        {
            fMainFrame.Navigate(productCategoriesPage);
        }

        private void btnToDictEmployeeRoles_Click(object sender, RoutedEventArgs e)
        {
            fMainFrame.Navigate(employeesRolesPage);
        }

        private void btnToOpSales_Click(object sender, RoutedEventArgs e)
        {
            fMainFrame.Navigate(salesPage);
        }

        private void btnToOpMaintenance_Click(object sender, RoutedEventArgs e)
        {
            fMainFrame.Navigate(maintenanceLogsPage);
        }

        private void btnToOpRestocks_Click(object sender, RoutedEventArgs e)
        {
            fMainFrame.Navigate(restockLogsPage);
        }
    }
}