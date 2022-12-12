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
using Libmas;
using System.IO;
using Microsoft.Win32;
using System.Data.Common;
using System.Windows.Threading;
using System.ComponentModel;
using Prakt13.Properties;

namespace Prakt13
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        double a = 1;
        double b = 0.96;
        double c = 0.80;
        int[,] array;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Load(object sender, EventArgs e)
        {
            Password pas = new Password();
            pas.Owner = this;
            pas.ShowDialog();
            if (File.Exists(".\\config.ini"))
            {
                LibMas.OpenArrayCfg(ref array);
                MatrixDG.ItemsSource = VisualArray.ToDataTable(array).DefaultView;
            }
            else
            {
                array = new int[0, 0];

            }
        }
        

        private void Column_TextChanged(object sender, TextChangedEventArgs e)
        {
            
                int.TryParse(Row.Text, out int row); int.TryParse(Column.Text, out int column);
            if (column > 0)
            {
                array = new int[row, column];
                LibMas.ArrayCompletion(ref array);
                MatrixDG.ItemsSource = VisualArray.ToDataTable(array).DefaultView;
                AVG.Clear();
                ColC.Text = $"Column {column}";
                RowC.Text = $"Row {row}";
            }
            else
            {
                AVG.Clear();
                MessageBox.Show("Задайте корректный размер для матрицы");
            }

        }

        private void Row_TextChanged(object sender, TextChangedEventArgs e)
        {
            int.TryParse(Row.Text, out int row); int.TryParse(Column.Text, out int column);
            if (row > 0 )
            {
                array = new int[row, column];
                LibMas.ArrayCompletion(ref array);
                MatrixDG.ItemsSource = VisualArray.ToDataTable(array).DefaultView;
                AVG.Clear();
                RowC.Text = $"Row {row}";
                ColC.Text = $"Column {column}";
            }
            else
            {
                AVG.Clear();
                MessageBox.Show("Задайте корректный размер для матрицы");
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (array.Length > 0)
            {
                SaveFileDialog sfg = new SaveFileDialog();
                sfg.Filter = "Текстовые файлы | *.txt";
                sfg.FilterIndex = 1;
                if (array == null)
                {
                    MessageBox.Show("Невозможно сохранить пустой массив");
                }
                else if (sfg.ShowDialog() == true)
                {
                    LibMas.SaveArray(array, sfg.FileName);
                }
            }
            else
            {
                MessageBox.Show("Для того что бы произвести решение сначало создайте матрицу");
            }
        }

        private void Clean_Click(object sender, RoutedEventArgs e)
        {
            RowC.Clear();
            ColC.Clear();
            LibMas.CleanArray(ref array);
            MatrixDG.ItemsSource = null;
            Column.Clear();
            Row.Clear();
            AVG.Clear();
            Column.Focus();
            IndexArrayStatus.Text = Convert.ToString(0);
        }

        private void Match_Click(object sender, RoutedEventArgs e)
        {
            if (array.Length != 0)
            {
                LibMas.Match(array, out string avg);
                AVG.Text = avg;
            }
            else
            {
                MessageBox.Show("Для того что бы произвести решение сначало создайте матрицу");
            }
        }

        private void Upload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog pfg = new OpenFileDialog();
            pfg.Filter = "Текстовые файлы | *.txt";
            pfg.FilterIndex = 1;
            if (pfg.ShowDialog() == true)
            {
                LibMas.UploadArray(ref array, pfg.FileName);
                MatrixDG.ItemsSource = VisualArray.ToDataTable(array).DefaultView;
            }
            RowC.Text = $"Row {Convert.ToString(array.GetLength(0))}";
            ColC.Text = $"Column {Convert.ToString(array.GetLength(1))}";

        }

        private void Task_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Борькин Иван ИСП-31 Вариант№8\nЗадание:\nДана матрица размера M * N. В каждом ее столбце найти количество элементов, больших среднего арифметического всех элементов этого столбца");
        }

        

        

        

        private void MatrixDG_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            IndexArrayStatus.Text = $"({e.Row.GetIndex() + 1};{ e.Column.DisplayIndex + 1})";
        }

        private void CopyRez_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(AVG.Text);

        }

        private void StandartTheme(object sender, RoutedEventArgs e)
        {
            timer.IsEnabled = false;
            MainGrid.Background = new SolidColorBrush(Color.FromRgb(255, 250, 205));
            
        }

        private void GradientThene(object sender, RoutedEventArgs e)
        {
            
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            timer.IsEnabled = true;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (c < 1)
            {
                c += 0.02;
                b -= 0.02;

            }
            else
            {
                c -= 0.2;
                b += 0.2;
            }






            MainGrid.Background = new SolidColorBrush(ColorUtils.GetRgb(a, b, c));

        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Вы уверены что хотите завершить работу с программой ?", "Выход", MessageBoxButton.YesNo, MessageBoxImage.Asterisk, MessageBoxResult.Yes);
            if (res == MessageBoxResult.Yes)
            {
                this.Close();
            }
            else { e.Cancel = true; }
        }

        private void Savecfg(object sender, RoutedEventArgs e)
        {
            Setup set = new Setup();
            set.Owner = this;
            set.ShowDialog();
            LibMas.OpenArrayCfg(ref array);
            MatrixDG.ItemsSource = VisualArray.ToDataTable(array).DefaultView;
        }
    }
    
}
