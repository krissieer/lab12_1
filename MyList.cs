using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ClassLibraryLab10;

namespace lab12_1
{
    public class MyList<T> where T : IInit, ICloneable, new()
    {
        Point<T>? beg = null;
        Point<T>? end = null;

        int count = 0;

        public int Count => count;

        public Point<T> MakeRandomData()
        {
            T data = new T();
            data.IRandomInit();
            return  new Point<T>(data);
        }

        public T MakeRandomItem()
        {
            T data = new T();
            data.IRandomInit();
            return data;
        }

        /// <summary>
        /// Добавление элемента в конец списка
        /// </summary>
        /// <param name="item"></param>
        public void AddToEnd(T item)
        {
            T newData = (T)item.Clone();
            Point<T> newItem = new Point<T>(newData);
            count++;    
            if (end != null)
            {
                end.Next = newItem;
                newItem.Prev = end;
                end = newItem;
            }
            else
            {
                beg = newItem;
                end = beg;
            }
        }

        /// <summary>
        /// Конструктор без параметов
        /// </summary>
        public MyList() { }

        /// <summary>
        /// Конструктор с параметрами(длина списка)
        /// </summary>
        /// <param name="size"></param>
        /// <exception cref="Exception"></exception>
        public MyList(int size)
        {
            if (size <= 0)
                throw new Exception("Длина меньше 0");
            beg = MakeRandomData();
            end = beg;
            for (int i = 1; i < size; i++)
            {
                T newItem = MakeRandomItem();
                AddToEnd(newItem);
            }
            count = size;
        }

        /// <summary>
        /// Конструктор с параметрами (массов элементов)
        /// </summary>
        /// <param name="coll"></param>
        /// <exception cref="Exception"></exception>
        public MyList(params T[] coll)
        {
            if (coll == null)
                throw new Exception("Пустой список (null)");
            if (coll.Length == 0)
                throw new Exception("Нет элементов в списке");
            T newData  = (T)coll[0].Clone();
            beg = new Point<T> (newData);
            end = beg;
            for (int i = 1; i < coll.Length; i++)
            {
                AddToEnd(coll[i]);
            }
            count = coll.Length;
        }

        /// <summary>
        /// Печать списка и количества элементов
        /// </summary>
        public void PrintList()
        {
            if (count == 0)
                Console.WriteLine("Список пустой");
            Point<T>? curr = beg;
            for (int i = 0; curr != null; i++)
            {
                Console.WriteLine(curr);
                curr = curr.Next;
            }
            Console.WriteLine($"Длина - {count}");
        }

        /// <summary>
        /// Поиск элемента по имени с начала
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Point<T>? FindBeginName(string name)
        {
            Point<T>? curr = beg;
            while (curr != null)
            {
                if (curr.Data is MusicalInstrument m && m.Name.Equals(name))
                    return curr;
                curr = curr.Next;
            }
            return null;
        }

        /// <summary>
        /// Добавление элемента после элемента с заданным именем
        /// </summary>
        /// <param name="name"></param>
        /// <param name="item"></param>
        /// <exception cref="Exception"></exception>
        public void AddAfter(string name, T item)
        {
            T newData = (T)item.Clone();
            Point<T> newItem = new Point<T>(newData);
            count++;
            Point<T>? pos = FindBeginName(name);

            if (pos == null)
                throw new Exception("Нет элемента с указанным именем");
            else
            {
                // один элемент в списке
                if (beg == end & beg == pos)
                {
                    pos.Next = newItem;
                    newItem.Prev = pos;
                    end = newItem;
                }
                // искомый элемент - последний в списке
                else if (pos == end)
                {
                    end = newItem;
                    pos.Next = newItem;
                    newItem.Prev = pos;
                }
                else
                {
                    Point<T> next = pos.Next;
                    pos.Next = newItem;
                    newItem.Prev = pos;
                    newItem.Next = next;
                    next.Prev = newItem;
                }
            }
        }

        /// <summary>
        /// Поиск элемента по имени с конца
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Point<T>? FindEndName(string name)
        {
            Point<T>? curr = end;
            while (curr != null)
            {
                if (curr.Data is MusicalInstrument m && m.Name.Equals(name))
                    return curr;
                curr = curr.Prev;
            }
            return null;
        }

        /// <summary>
        /// Удаление последнего элемента по заданному имени
        /// </summary>
        /// <param name="name"></param>
        /// <exception cref="Exception"></exception>
        public void DeleteLast(string name)
        {
            if (beg == null)
                throw new Exception("Лист пустой");
            Point<T>? pos = FindEndName(name);

            if (pos == null) 
                throw new Exception("Нет элемента с указанным именем");
            count--;

            // один элемент в списке
            if (beg == end)
            {
                beg = end = null;
            }
            // удаление первого элемента списка
            else if (pos.Prev == null)
            {
                beg = beg?.Next;
                beg.Prev = null;
            }
            // удаление последнего элемента списка
            else if (pos.Next == null)
            {
                end = end.Prev;
                end.Next = null;
            }
            else
            {
                Point<T> next = pos.Next;
                Point<T> prev = pos.Prev;
                pos.Next.Prev = prev;
                pos.Prev.Next = next;
            }
        }

        /// <summary>
        /// Удаление списка
        /// </summary>
        public void DeleteList()
        {
            beg = end = null;
            count = 0;
        }

        /// <summary>
        /// Глубокое копирование списка
        /// </summary>
        /// <returns></returns>
        public MyList<T> Clone()
        {
            MyList<T> cloneList = new MyList<T>();

            Point<T>? curr = beg;
            while (curr != null)
            {
                T cloneData = (T)curr.Data.Clone();
                cloneList.AddToEnd(cloneData);
                curr = curr.Next;
            }
            return cloneList;
        }

        public MyList<T> ChangeItem(T item, MyList<T> list)
        {
            if (beg == null)
                throw new Exception("Лист пустой");
            else
            {
                list.beg.Data = item;
                return list;
            }           
        }
    }
}
