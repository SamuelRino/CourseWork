using CourseWork.Classes;
using CourseWork.Models;
using CourseWork.Pages.DialogWindows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class MachinesPage : Page
    {
        bool deletedVisibilty = false;
        public MachinesPage()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            using (var _db = new VendingDbContext())
            {
                var machines = _db.VendingMachines
                                 .Include(m => m.Location)
                                 .Include(m => m.Status)                              
                                 .ToList();

                if (deletedVisibilty == false)
                {
                    machines = machines.Where(m => m.IsDeleted == false).ToList();
                }

                lvMachines.ItemsSource = machines;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить автомат?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                var machine = button.DataContext as VendingMachine;

                using (var db = new VendingDbContext())
                {
                    var m = db.VendingMachines.FirstOrDefault(x => x.MachineId == machine.MachineId);
                    if (m != null)
                    {
                        m.IsDeleted = true;
                        db.SaveChanges();
                    }
                }
                RefreshData();
            }
        }

        private void btnRestock_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var machine = button.DataContext as VendingMachine;

            DataMachine.machine = machine;

            RestockMachineWindow w = new();
            w.ShowDialog();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataMachine.machine = null;

            AddEditMachineWindow w = new();
            w.ShowDialog();

            RefreshData();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            var machine = btn.DataContext as VendingMachine;

            DataMachine.machine = machine;

            AddEditMachineWindow w = new();
            w.ShowDialog();

            RefreshData();
        }

        private void cbShowDeleted_Checked(object sender, RoutedEventArgs e)
        {
            cIsDeleted.Width = 50;
            deletedVisibilty = true;
            RefreshData();
        }

        private void cbShowDeleted_Unchecked(object sender, RoutedEventArgs e)
        {
            cIsDeleted.Width = 0;
            deletedVisibilty = false;
            RefreshData();
        }      
    }
}
