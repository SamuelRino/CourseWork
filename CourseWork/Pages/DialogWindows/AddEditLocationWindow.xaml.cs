using CourseWork.Classes;
using CourseWork.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Windows;

namespace CourseWork.Pages.DialogWindows
{
    public partial class AddEditLocationWindow : Window
    {
        private Location _location;
        private VendingDbContext _db = new();

        public AddEditLocationWindow()
        {
            InitializeComponent();

            if (DataLocation.location != null)
            {
                _location = _db.Locations.FirstOrDefault(l => l.LocationId == DataLocation.location.LocationId);

                tbTitle.Text = "Изменение локации";
                btnSave.Content = "Сохранить";
            }
            else
            {
                _location = new();
                tbTitle.Text = "Создание локации";
                btnSave.Content = "Создать";
            }

            DataContext = _location;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DataLocation.location != null)
                {
                    _db.SaveChanges();
                }
                else
                {
                    _db.Locations.Add(_location);
                    _db.SaveChanges();
                }
                MessageBox.Show("Локация успешно сохранена!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (DbUpdateException ex)
            {
                StringBuilder message = new StringBuilder();
                message.AppendLine("Возникла ошибка при сохранении.");

                if (ex.InnerException is SqlException sqlEx)
                {
                    if (sqlEx.Number == 515)
                    {
                        message.AppendLine("Не заполнены обязательные поля (Адрес).");
                    }
                }

                MessageBox.Show(message.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}