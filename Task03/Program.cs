using System;
using System.Collections.Generic;
using System.Linq;

/*Все действия по обработке данных выполнять с использованием LINQ
 * 
 * Объявите перечисление Manufacturer, состоящее из элементов
 * Dell (код производителя - 0), Asus (1), Apple (2), Microsoft (3).
 * 
 * Обратите внимание на класс ComputerInfo, он содержит поле типа Manufacturer
 * 
 * На вход подается число N.
 * На следующих N строках через пробел записана информация о компьютере: 
 * фамилия владельца, код производителя (от 0 до 3) и год выпуска (в диапазоне 1970-2020).
 * Затем с помощью средств LINQ двумя разными способами (как запрос или через методы)
 * отсортируйте коллекцию следующим образом:
 * 1. Первоочередно объекты ComputerInfo сортируются по фамилии владельца в убывающем порядке
 * 2. Для объектов, у которых фамилии владельцев сопадают, 
 * сортировка идет по названию компании производителя (НЕ по коду) в возрастающем порядке.
 * 3. Если совпадают и фамилия, и имя производителя, то сортировать по году выпуска в порядке убывания.
 * 
 * Выведите элементы каждой коллекции на экран в формате:
 * <Фамилия_владельца>: <Имя_производителя> [<Год_производства>]
 * 
 * Пример ввода:
 * 3
 * Ivanov 1970 0
 * Ivanov 1971 0
 * Ivanov 1970 1
 * 
 * Пример вывода:
 * Ivanov: Asus [1970]
 * Ivanov: Dell [1971]
 * Ivanov: Dell [1970]
 * 
 * Ivanov: Asus [1970]
 * Ivanov: Dell [1971]
 * Ivanov: Dell [1970]
 * 
 * 
 *  * Обрабатывайте возможные исключения путем вывода на экран типа этого исключения 
 * (не использовать GetType(), пишите тип руками).
 * Например, 
 *          catch (SomeException)
            {
                Console.WriteLine("SomeException");
            }
 * При некорректных входных данных (не связанных с созданием объекта) выбрасывайте FormatException
 * При невозможности создать объект класса ComputerInfo выбрасывайте ArgumentException!
 */
namespace Task03
{
    public class Program
    {
        public static void Main()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("ru-RU");
            int N;
            List<ComputerInfo> computerInfoList = new List<ComputerInfo>();

            try
            {
                N = int.Parse(Console.ReadLine());
                if (N <= 0)
                    throw new FormatException();
                for (int i = 0; i < N; i++)
                {
                    var spl = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (spl.Length < 3)
                        throw new ArgumentException();

                    string name = spl[0];
                    int year = int.Parse(spl[1]), code = int.Parse(spl[2]);
                    if (!Enum.IsDefined(typeof(Manufacturer), code))
                        throw new ArgumentException();
                    Manufacturer manufacturer = (Manufacturer)code;

                    computerInfoList.Add(new ComputerInfo(name, manufacturer, year));
                }

                // выполните сортировку одним выражением
                var computerInfoQuery = from computer in computerInfoList
                                        orderby computer.Owner descending,
                                        computer.ComputerManufacturer.ToString(),
                                        computer.Year descending
                                        select computer;

                PrintCollectionInOneLine(computerInfoQuery);

                Console.WriteLine();

                // выполните сортировку одним выражением
                var computerInfoMethods = computerInfoList
                    .OrderByDescending(o => o.Owner)
                    .ThenBy(o => o.ComputerManufacturer.ToString())
                    .ThenByDescending(o => o.Year);

                PrintCollectionInOneLine(computerInfoMethods);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException");
            }
            catch (FormatException)
            {
                Console.WriteLine("FormatException");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("InvalidOperationException");
            }
            catch (OverflowException)
            {
                Console.WriteLine("OverflowException");
            }
        }

        // выведите элементы коллекции на экран с помощью кода, состоящего из одной линии (должна быть одна точка с запятой)
        public static void PrintCollectionInOneLine(IEnumerable<ComputerInfo> collection)
            => collection.Select(t => $"{t}")
                .ToList()
                .ForEach(Console.WriteLine);
    }


    public class ComputerInfo
    {
        public string Owner { get; set; }
        public Manufacturer ComputerManufacturer { get; set; }
        public int Year { get; set; }

        public ComputerInfo(string owner, Manufacturer computerManufacturer, int year)
        {
            if (year < 1970 || year > 2020)
                throw new ArgumentException();
            Owner = owner;
            ComputerManufacturer = computerManufacturer;
            Year = year;
        }

        public override string ToString()
            => $"{Owner}: {ComputerManufacturer} [{Year}]";
    }

    public enum Manufacturer
    {
        Dell = 0,
        Asus = 1,
        Apple = 2,
        Microsoft = 3
    }
}
