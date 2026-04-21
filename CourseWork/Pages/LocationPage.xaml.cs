using CourseWork.Classes;
using CourseWork.Models;
using CourseWork.Pages.DialogWindows;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace CourseWork.Pages
{
    public partial class LocationsPage : Page
    {
        public LocationsPage()
        {
            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            using (var _db = new VendingDbContext())
            {
                var locations = _db.Locations.ToList();
                lvLocations.ItemsSource = locations;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить локацию?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                var button = sender as Button;
                var location = button.DataContext as Location;

                using (var db = new VendingDbContext())
                {
                    var loc = db.Locations.FirstOrDefault(x => x.LocationId == location.LocationId);
                    if (loc != null)
                    {
                        try
                        {
                            db.Locations.Remove(loc);
                            db.SaveChanges();
                        }
                        catch
                        {
                            MessageBox.Show("Невозможно удалить локацию, так как к ней уже привязан один или несколько автоматов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
                RefreshData();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataLocation.location = null;
            AddEditLocationWindow w = new();
            w.ShowDialog();
            RefreshData();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            var location = btn.DataContext as Location;

            DataLocation.location = location;
            AddEditLocationWindow w = new();
            w.ShowDialog();
            RefreshData();
        }
    }
}