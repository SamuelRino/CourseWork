using CourseWork.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Pages
{
    public partial class StockFullInfoPage : Page
    {
        public StockFullInfoPage()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            using (var _db = new VendingDbContext())
            {
                var stockInfo = _db.VwStockFullInfos.ToList();
                lvStockFullInfo.ItemsSource = stockInfo;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}