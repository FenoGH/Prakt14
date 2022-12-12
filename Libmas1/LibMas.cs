using System;
using System.Data;
using System.Data.Common;
using System.IO;

namespace Libmas
{
    public class LibMas
    {

        /// <summary>
        /// Заполнение массива случайными значениями
        /// </summary>
        /// <param name="array">Передаваемый и возвращаемый массив</param>
        public static void ArrayCompletion(ref int[,] array)
        {
            Random rnd = new Random();
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = rnd.Next(0, 100);
                }
            }
        }
        /// <summary>
        /// Очистка массива
        /// </summary>
        /// <param name="mas">Передаваемый массив</param>
        public static void CleanArray(ref int[,] array)
        {
            array = null;
        }
        /// <summary>
        /// Сохраняет двумерный массив
        /// </summary>
        /// <param name="mas">Двумерный массив</param>
        /// <param name="path">Путь до файла</param>
        public static void SaveArray(int[,] mas, string path)
        {
            using (StreamWriter file = new StreamWriter(path))
            {
                file.WriteLine(mas.GetLength(0));
                file.WriteLine(mas.GetLength(1));

                for (int i = 0; i < mas.GetLength(0); i++)
                {
                    for (int j = 0; j < mas.GetLength(1); j++)
                    {
                        file.WriteLine(mas[i, j]);
                    }
                }
                file.Close();
            }
        }
        /// <summary>
        /// Считывание массива из файла
        /// </summary>
        /// <param name="mas"></param>
        /// <param name="path"></param>
        public static void UploadArray(ref int[,] mas, string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                int.TryParse(reader.ReadLine(), out int row);
                int.TryParse(reader.ReadLine(), out int column);

                mas = new int[row, column];

                for (int i = 0; i < mas.GetLength(0); i++)
                {
                    for (int j = 0; j < mas.GetLength(1); j++)
                    {
                        int.TryParse(reader.ReadLine(), out int value);
                        mas[i, j] = value;
                    }
                }
                reader.Close();
            }
        }
        /// <summary>
        /// Находит колличество эллементов больших среднего арифметического столбца
        /// </summary>
        /// <param name="mas"> Передаваемая матрица </param>
        /// <param name="nums"> Возвращаемая строка со списком колличества эллементов </param>
        public static void Match(int[,] mas, out string nums)
        {
            double average = 0;
            int avg = 0;
            int c = 0; 
            nums = "";
            for (int j = 0; j < mas.GetLength(1); j++)
            {
                avg = 0;
                average = 0;
                for (int i = 0; i < mas.GetLength(0); i++)
                {
                    avg += mas[i, j];
                    average=avg / mas.GetLength(0);
                    c = 0;
                    for(int k=0; k < mas.GetLength(0); k++)
                    {
                        if (mas[k, j] > average)
                        {
                            c++;
                        }
                        
                    }

                    

                }
                nums +=   $"{j+1} столбец {Convert.ToString(c)}\n";

            }
            

        }
        public static void SaveArrayCfg(int row, int column)
        {
            StreamWriter file = new StreamWriter(".\\config.ini");
            file.WriteLine(row);
            file.WriteLine(column);
            file.Close();
        }
        public static void OpenArrayCfg(ref int[,] mas)
        {
            StreamReader file = new StreamReader(".\\config.ini");
            int rows = Convert.ToInt32(file.ReadLine());
            int columns = Convert.ToInt32(file.ReadLine());
            mas = null;
            mas = new int[rows, columns];
            Random rnd = new Random();
            for (int i = 0; i < mas.GetLength(0); i++)
            {
                for (int j = 0; j < mas.GetLength(1); j++)
                {
                    mas[i, j] = rnd.Next(0, 100);
                }
            }
            file.Close();
        }
    }

    public static class VisualArray
    {
        //Метод для одномерного массива
        public static DataTable ToDataTable<T>(this T[] arr)
        {
            var res = new DataTable();
            for (int i = 0; i < arr.Length; i++)
            {
                res.Columns.Add("col" + (i + 1), typeof(T));
            }
            var row = res.NewRow();
            for (int i = 0; i < arr.Length; i++)
            {
                row[i] = arr[i];
            }
            res.Rows.Add(row);
            return res;
        }
        //Метод для двухмерного массива
        public static DataTable ToDataTable<T>(this T[,] matrix)
        {
            var res = new DataTable();
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                res.Columns.Add("col" + (i + 1), typeof(T));
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var row = res.NewRow();

                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    row[j] = matrix[i, j];
                }

                res.Rows.Add(row);
            }

            return res;
        }

    }
}

