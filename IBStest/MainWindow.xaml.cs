using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Microsoft.Win32;

namespace IBStest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PersonModel _person;
        private bool _icqInvalid;

        public MainWindow()
        {
            InitializeComponent();
            _person = new PersonModel();
            DataContext = _person;
            List<FavoriteDish> list = new List<FavoriteDish>();
            dgDishes.ItemsSource = list;
        }

        private void CbCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Label lCountry = (Label)CbCountry.SelectedItem;
            if (lCountry.Content.ToString() == "Зимбабве")
            {
                gIcq.IsEnabled = true;
            }
            else
            {
                TbIcq.Clear();
                Validation.ClearInvalid(TbIcq.GetBindingExpression(TextBox.TextProperty));
                gIcq.IsEnabled = false;
                _icqInvalid = false;
            }
        }

        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            string error;
            _icqInvalid = false;
            if (!PersonModel.IsCorrectIcq(((TextBox)sender).Text, out error))
            {
                _icqInvalid = true;
                MessageBox.Show(error);
            }
        }

        private void OnAutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            var displayName = GetPropertyDisplayName(e.PropertyDescriptor);

            if (!string.IsNullOrEmpty(displayName))
            {
                e.Column.Header = displayName;
            }

        }

        public static string GetPropertyDisplayName(object descriptor)
        {
            var pd = descriptor as PropertyDescriptor;

            if (pd != null)
            {
                // Check for DisplayName attribute and set the column header accordingly
                var displayName = pd.Attributes[typeof(DisplayNameAttribute)] as DisplayNameAttribute;

                if (displayName != null && displayName != DisplayNameAttribute.Default)
                {
                    return displayName.DisplayName;
                }

            }
            else
            {
                var pi = descriptor as PropertyInfo;

                if (pi != null)
                {
                    // Check for DisplayName attribute and set the column header accordingly
                    Object[] attributes = pi.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                    for (int i = 0; i < attributes.Length; ++i)
                    {
                        var displayName = attributes[i] as DisplayNameAttribute;
                        if (displayName != null && displayName != DisplayNameAttribute.Default)
                        {
                            return displayName.DisplayName;
                        }
                    }
                }
            }

            return null;
        }

        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            if (_icqInvalid)
            {
                MessageBox.Show("Введите правильный ICQ номер!");
                return;
            }
            _person.Icq = TbIcq.Text;
            if (CbCountry.SelectedItem != null && 
                !string.IsNullOrEmpty(CbCountry.SelectedItem.ToString()))
            {
                Label l = (Label) CbCountry.SelectedItem;
                _person.Country = l.Content.ToString();
            }
            
            List<FavoriteDish> dishes = new List <FavoriteDish>();
            foreach (object rowDish in dgDishes.Items)
            {
                FavoriteDish dish;
                try
                {
                    dish = (FavoriteDish)rowDish;
                }
                catch (InvalidCastException)
                {
                    break;
                }
                dishes.Add(dish);
            }
            _person.Dishes = dishes;
            

            SaveFileDialog dialog = new SaveFileDialog();
            dialog.FileName = "data";
            dialog.DefaultExt = ".csv";
            dialog.Filter = "CSV файлы (.csv)|*.csv";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                string filename = dialog.FileName;
                CsvFile csvFile = new CsvFile(filename, _person);
            }
        }



    }
}
