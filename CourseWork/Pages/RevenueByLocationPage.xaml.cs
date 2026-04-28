using CourseWork.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Pages
{
    public partial class RevenueByLocationPage : Page
    {
        public RevenueByLocationPage()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            using (var _db = new VendingDbContext())
            {
                var revenueData = _db.VwRevenueByLocations.ToList();
                lvRevenue.ItemsSource = revenueData;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}