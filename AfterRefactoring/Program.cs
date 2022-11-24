using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AfterRefactoring
{
    internal class Program
    {
        static List<Transport> transports = new List<Transport>();
        static void Main(string[] args)
        {
            //Initilizing tranports
            transports.Add(new Automobile()
            {
                mark = "Nisan",
                model = "V8",
                horsePower = 4
            });
            transports.Add(new MotoBike()
            {
                mark = "Yamaha",
                model = "14.1",
                horsePower = 1,
                wheelchair = false
            });
            transports.Add(new Truck()
            {
                mark = "Honda",
                model = "SEX PISTOLS",
                horsePower = 10,
                trailer = true
            });
            while (true)
            {
                PrintInterface();
                Console.WriteLine("\nДля входа из программы нажмите enter.");
                if (Console.ReadLine() == "")
                {
                    break;
                }
            }
        }
        //Method prining main program interface
        static void PrintInterface()
        {
            foreach (var item in transports)
            {
                item.CalcLoadCapacity();
                item.PrintTransportInfo();
                Console.WriteLine("\n");
            }

            Console.WriteLine("чтобы перейтив режим поиска введите \"search\"");

            if (Console.ReadLine().ToLower() == "search")
            {
                Search();
            }
            else
            {
                Console.Clear();
            }

        }
        //Method realizing enter Load capacity filter
        static string[] EnterLoadCapactityFilter(string[] text)
        {
            Console.Clear();
            Console.WriteLine("Введите нужные рамки по грузоподъемности (например 10-13)");
            text = Console.ReadLine().Split('-');
            return text;
        }
        //Method realizing search by the filter
        static void Search()
        {
            const string separator = "___________________________________________";
            string[] text = { "" };
            while (true)
            {
                while (true)
                {
                    text = EnterLoadCapactityFilter(text);
                    if (text[0] != "" && text[1] != "")
                    {
                        break;
                    }
                }

                Console.WriteLine(separator);
                int min = Convert.ToInt32(text[0]);
                int max = Convert.ToInt32(text[1]);

                foreach (Transport item in transports)
                {
                    if (item.loadCapacity >= min && item.loadCapacity <= max)
                    {
                        item.PrintTransportInfo();
                        Console.WriteLine("\n");
                    }
                    else
                    {
                        Console.WriteLine("Транспорта по данному фильтру не найдено.\n");
                        break;
                    }
                }

                Console.WriteLine("Чтобы вернуться назад нажмите enter, чтобы воспользоваться поиском еще раз введите \"retry\"");
                string end_text = Console.ReadLine().ToLower();
                if (end_text == "")
                {
                    break;
                }
                else if (end_text == "retry")
                {
                    Search();
                }

            }
        }
        static string ConvertBoolToString(bool b)
        {
            return b ? "есть" : "нет";
        }
        abstract class Transport
        {
            public string mark { get; set; }
            public string model { get; set; }
            public double horsePower { get; set; }
            public double loadCapacity { get; set; }
            //Method printing default transport information
            public virtual void PrintTransportInfo()
            {
                Console.WriteLine("Марка - {0,5} \nмодель - {1,5} \nлошадиные силы - {2,4} \nгрузоподъесность - {3,4}", mark, model, horsePower, loadCapacity);
            }

            //Method calculating Load Capacity
            public virtual void CalcLoadCapacity()
            {
                loadCapacity = horsePower * 2.5 / Math.PI;
            }
        }

        class Automobile : Transport
        {
        }
        class MotoBike : Transport
        {
            public bool wheelchair { get; set; }

            public override void CalcLoadCapacity()
            {
                if (wheelchair)
                {
                    loadCapacity = horsePower * 2.5 / Math.PI;
                }
                else
                {

                    loadCapacity = 0;
                }
            }
            public override void PrintTransportInfo()
            {
                string rusWheelchair = ConvertBoolToString(wheelchair);
                Console.WriteLine("Марка - {0,5} \nмодель - {1,5} \nлошадиные силы - {2,4} \nгрузоподъесность - {3,4}\nналичие коляски - {4,4}", mark, model, horsePower, loadCapacity, rusWheelchair);
            }
        }
        class Truck : Transport
        {
            public bool trailer;
            public override void CalcLoadCapacity()
            {
                if (trailer)
                {
                    loadCapacity = horsePower * 2.5 / Math.PI * 2;
                }
                else
                {
                    loadCapacity = horsePower * 2.5 / Math.PI;
                }

            }
        }
    }

}
