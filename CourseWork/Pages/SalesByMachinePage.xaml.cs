using CourseWork.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Pages
{
    public partial class SalesByMachinePage : Page
    {
        public SalesByMachinePage()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            using (var _db = new VendingDbContext())
            {
                var salesData = _db.VwSalesByMachines.ToList();
                lvSalesByMachine.ItemsSource = salesData;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}