using CourseWork.Models;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Pages
{
    public partial class EmployeeActivityPage : Page
    {
        public EmployeeActivityPage()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            using (var _db = new VendingDbContext())
            {
                var activityList = _db.VwEmployeeActivities.ToList();
                lvEmployeeActivity.ItemsSource = activityList;
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }
    }
}