using CourseWork.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Pages
{
    public partial class LowStockPage : Page
    {
        public LowStockPage()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            using (var _db = new VendingDbContext())
            {
                var lowStockAlerts = _db.VwLowStockAlerts.ToList();
                lvLowStock.ItemsSource = lowStockAlerts;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}