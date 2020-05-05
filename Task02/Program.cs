﻿using System;
using System.Linq;

/* В задаче не использовать циклы for, while. Все действия по обработке данных выполнять с использованием LINQ
 * 
 * На вход подается строка, состоящая из целых чисел типа int, разделенных одним или несколькими пробелами.
 * Необходимо оставить только те элементы коллекции, которые предшествуют нулю, или все, если нуля нет.
 * Дважды вывести среднее арифметическое квадратов элементов новой последовательности.
 * Вывести элементы коллекции через пробел.
 * Остальные указания см. непосредственно в коде.
 * 
 * Пример входных данных:
 * 1 2 0 4 5
 * 
 * Пример выходных:
 * 2,500
 * 2,500
 * 1 2
 * 
 * Обрабатывайте возможные исключения путем вывода на экран типа этого исключения 
 * (не использовать GetType(), пишите тип руками).
 * Например, 
 *          catch (SomeException)
            {
                Console.WriteLine("SomeException");
            }
 * В случае возникновения иных нештатных ситуаций (например, в случае попытки итерирования по пустой коллекции) 
 * выбрасывайте InvalidOperationException!
 */
namespace Task02
{
    public class Program
    {
        public static void Main()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            RunTesk02();
        }

        public static void RunTesk02()
        {
            int[] arr;
            try
            {
                // Попробуйте осуществить считывание целочисленного массива, записав это ОДНИМ ВЫРАЖЕНИЕМ.
                arr = Console.ReadLine().Split(new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

                bool isZero = true;

                double[] filteredCollection = arr.Select(x => Math.Pow(x, 2)).Where(x => { if (!isZero) return isZero; isZero = x != 0; return isZero; }).ToArray();


                // использовать статическую форму вызова метода подсчета среднего
                double averageUsingStaticForm = Enumerable.Average(filteredCollection);
                // использовать объектную форму вызова метода подсчета среднего
                double averageUsingInstanceForm = filteredCollection.Average();
                Console.WriteLine($"{averageUsingInstanceForm:f3}\n{averageUsingStaticForm:f3}");

                // вывести элементы коллекции в одну строку
                Console.WriteLine(filteredCollection.Select(x => ((int)Math.Pow(x, 0.5)).ToString()).Aggregate((x, y) => x + " " + y));

            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("ArgumentNullException");
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine("OutOfMemoryException");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("InvalidOperationException");
            }
            catch (OverflowException)
            {
                Console.WriteLine("OverflowException");
            }
            catch (FormatException)
            {
                Console.WriteLine("FormatException");
            }

        }

    }
}
