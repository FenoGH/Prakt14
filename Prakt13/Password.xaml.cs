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
using System.Windows.Shapes;

namespace Prakt13
{
    /// <summary>
    /// Логика взаимодействия для Password.xaml
    /// </summary>
    public partial class Password : Window
    {
        public Password()
        {
            InitializeComponent();
        }

        private void Window_Activate(object sender, RoutedEventArgs e)
        {
            txtPas.Focus();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (txtPas.Password == "123") Close();
            else
            {
                MessageBox.Show("Введен неверный пароль");
                txtPas.Focus();
            }
        }

        private void Esc_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (txtPas.Password != "123")
            {
                e.Cancel = true;
            }
        }
    }
}
