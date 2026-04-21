using CourseWork.Models;
using CourseWork.Pages.DialogWindows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Pages
{
    public partial class SalesPage : Page
    {
        public SalesPage()
        {
            InitializeComponent();
            LoadFilters();
            RefreshData();
        }

        private void LoadFilters()
        {
            using (var _db = new VendingDbContext())
            {
                var machines = _db.VendingMachines.ToList();
                machines.Insert(0, new VendingMachine { SerialNumber = "Все", MachineId = 0 });

                cbMachineFilter.ItemsSource = machines;
                cbMachineFilter.DisplayMemberPath = "SerialNumber";
                cbMachineFilter.SelectedIndex = 0;
            }
        }

        private void RefreshData()
        {
            using (var _db = new VendingDbContext())
            {
                var query = _db.Sales
                               .Include(s => s.Machine)
                               .Include(s => s.Product)
                               .Include(s => s.Method)
                               .AsQueryable();

                if (dpStart.SelectedDate.HasValue)
                    query = query.Where(s => s.SaleDate >= dpStart.SelectedDate.Value);

                if (dpEnd.SelectedDate.HasValue)
                {
                    var endDate = dpEnd.SelectedDate.Value.AddDays(1).AddTicks(-1);
                    query = query.Where(s => s.SaleDate <= endDate);
                }

                if (cbMachineFilter.SelectedItem is VendingMachine sm && sm.MachineId != 0)
                    query = query.Where(s => s.MachineId == sm.MachineId);

                lvSales.ItemsSource = query.OrderByDescending(s => s.SaleDate).ToList();
            }
        }

        private void Filter_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (IsLoaded) RefreshData();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            dpStart.SelectedDate = null;
            dpEnd.SelectedDate = null;
            cbMachineFilter.SelectedIndex = 0;
            RefreshData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            RegisterSaleWindow w = new();
            w.ShowDialog();
            RefreshData();
        }
    }
}