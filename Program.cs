using ClassLibraryLab10;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;

namespace lab12_1
{
    public class Program
    {
        /// <summary>
        /// Создание списка из элементов иерархии
        /// </summary>
        /// <param name="list"></param>
        static MyList<MusicalInstrument> MakeList(MyList<MusicalInstrument> list)
        {
            Guitar g = new Guitar();
            g.IRandomInit();
            Piano p = new Piano();
            p.IRandomInit();
            ElectricGuitar e = new ElectricGuitar();
            e.IRandomInit();
            MusicalInstrument m1 = new MusicalInstrument();
            m1.IRandomInit();
            MusicalInstrument m2 = new MusicalInstrument();
            m2.IRandomInit();
            list = new MyList<MusicalInstrument>(g, p, e, m1, m2);
            return list;
        }

        /// <summary>
        /// Удаление элеиента с указанным именем и печать списка
        /// </summary>
        /// <param name="list"></param>
        static void DeleteLastItem(MyList<MusicalInstrument> list)
        {
            string name;
            Console.WriteLine("Введите название объекта, который необходимо удалить: ");
            name = Console.ReadLine();
            Console.WriteLine($"Удаляемый элемент: {list.FindEndName(name)}");
            list.DeleteLast(name);           
            list.PrintList();
        }

        /// <summary>
        /// Добавление элемента после элемента с указанным именем и печать списка
        /// </summary>
        /// <param name="list"></param>
        static void AddItem(MyList<MusicalInstrument> list)
        {
            MusicalInstrument m3 = new MusicalInstrument();
            m3.IRandomInit();
            string name;
            Console.WriteLine("Введите название, после которого необходимо добавить новый элемент: ");
            name = Console.ReadLine();
            list.AddAfter(name, m3);
            Console.WriteLine($"Элемент, после которого добавляется новый элемент: {list.FindBeginName(name)}");
            Console.WriteLine($"Добавляемый элемент : {m3}");
            list.PrintList();
        }

        /// <summary>
        /// Клонирование листа, проверка на првильностть клонирования и печать
        /// </summary>
        /// <param name="list"></param>
        static MyList<MusicalInstrument> CloneList(MyList<MusicalInstrument> list)
        {
            MyList<MusicalInstrument> clonedList = new MyList<MusicalInstrument>();
            clonedList = list.Clone();
            clonedList.PrintList();
            Console.WriteLine();
            return clonedList;
        }

        static void CheckClone (MyList<MusicalInstrument> clonedList)
        {
            Console.WriteLine("Клон списка с измененным первым элементом:");
            MusicalInstrument m4 = new MusicalInstrument();
            m4.IRandomInit();
            clonedList.ChangeItem(m4, clonedList);
            clonedList.PrintList();
        }

        static void Main(string[] args)
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>();
            MyList<MusicalInstrument> clonedList = new MyList<MusicalInstrument>();
            
            int ans;
            bool isConvert;

            do
            {
                Console.WriteLine("1. Создание списка");
                Console.WriteLine("2. Добавление элемента в конец списка");
                Console.WriteLine("3. Удаление последнего элемент с заданным информационным полем (именем)");
                Console.WriteLine("4. Добавление элемента после элемента с заданным информационным полем (именем)");
                Console.WriteLine("5. Глубокое копирование списка");
                Console.WriteLine("6. Удаление списка из памяти ");
                Console.WriteLine("7. Печать исходного списка ");
                
                Console.WriteLine("0. Назад");

                do
                {
                    isConvert = int.TryParse( Console.ReadLine(), out ans);
                    if (!isConvert)
                        Console.WriteLine("Число введено неправильно. Введите число еще раз");
                } while (!isConvert);

                switch(ans)
                {
                    case 1: //Создание списка
                        {
                            Console.WriteLine("===  Создание списка  ===");
                            list = MakeList(list);
                            list.PrintList();
                            Console.WriteLine();
                            break;
                        }
                    case 2: //Добавление элемента в конец списка
                        {
                            Console.WriteLine("===   Добавление элемента в конец списка  ===");
                            Guitar g1 = new Guitar();
                            g1.IRandomInit();
                            list.AddToEnd(g1);
                            list.PrintList();
                            Console.WriteLine();
                            break;
                        }
                    case 3: //Удаление последнего элемент с заданным информационным полем (именем)
                        {
                            Console.WriteLine("===  Удаление последнего элемент с заданным информационным полем (именем)  ===");
                            DeleteLastItem(list);
                            Console.WriteLine();
                            break;
                        }
                    case 4: // Добавление элемента после элемента с заданным информационным полем (именем)
                        {
                            Console.WriteLine("===  Добавление элемента после элемента с заданным информационным полем (именем)  ===");
                            AddItem(list);
                            Console.WriteLine();
                            break;
                        }
                    case 5: //Глубокое копирование
                        {
                            Console.WriteLine("===  Глубокое копирование ===");
                            clonedList = CloneList(list);
                            CheckClone(clonedList);
                            Console.WriteLine();
                            break;
                        }
                    case 6: //Удаление всего списка 
                        {
                            Console.WriteLine("===  Удаление всего списка   ===");
                            list.DeleteList();
                            list.PrintList();
                            Console.WriteLine();
                            break;
                        }
                    case 7: //Печать исходного списка 
                        {
                            Console.WriteLine("===  Исходный массив   ===");
                            list.PrintList();
                            Console.WriteLine();
                            break;
                        }
                    

                }
            }while (ans != 0);
        }
    }
}
