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

namespace IBStest
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PersonModel person;

        public MainWindow()
        {
            InitializeComponent();
            person = new PersonModel();
            DataContext = person;
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
            }
        }

        private void TextBox_Error(object sender, ValidationErrorEventArgs e)
        {
            string error;
            if (!PersonModel.IsCorrectIcq(((TextBox)sender).Text, out error))
            {
                MessageBox.Show(error);
            }
        }

        
    }
}
