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
    /// <summary>
    /// Логика взаимодействия для MachineStockPage.xaml
    /// </summary>
    public partial class MachineStockPage : Page
    {
        public MachineStockPage()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            using (var _db = new VendingDbContext())
            {
                var stocks = _db.MachineStocks
                                  .Include(s => s.Machine)
                                  .Include(s => s.Product)
                                  .Where(s => s.Machine.IsDeleted == false)
                                  .GroupBy(s => s.Machine)
                                  .Select(s => new
                                  {
                                      Machine = s.Key,
                                      Data = s.ToList()
                                  })
                                  .ToList();

                lvMachines.ItemsSource = stocks;
            }
        }

        private void btnRestock_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            dynamic stock = button.DataContext;

            DataMachine.machine = stock.Machine;

            RestockMachineWindow w = new();
            w.ShowDialog();

            RefreshData();
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            dynamic stock = button.DataContext;

            DataMachine.machine = stock.Machine;

            AddProductInStockWindow w = new();
            w.ShowDialog();

            RefreshData();
        }
    }
}
