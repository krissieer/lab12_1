using ClassLibraryLab10;
using lab12_1;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Test_12_1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void MakeRandomData()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>();

            Point<MusicalInstrument> point = list.MakeRandomData();

            Assert.IsNotNull(point);
            Assert.IsNotNull(point.Data);
        }

        [TestMethod]
        public void MakeRandomItem()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>();

            MusicalInstrument newItem = list.MakeRandomItem();

            Assert.AreNotEqual(newItem,default(MusicalInstrument));
        }

        [TestMethod]
        public void AddToEndNull()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>();
            
            MusicalInstrument m = new MusicalInstrument("ff",33);
            Point<MusicalInstrument> newItem = new Point<MusicalInstrument>(m);

            list.AddToEnd(m);

            Assert.AreEqual(1, list.Count);
            Assert.AreEqual(newItem.Next, null);
        }

        [TestMethod]
        public void AddToEnd()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>();
            MusicalInstrument m = new MusicalInstrument("ff", 33);
            list.AddToEnd(m);

            MusicalInstrument m2 = new MusicalInstrument("ee", 22);
            Point<MusicalInstrument> newItem = new Point<MusicalInstrument>(m2);
            list.AddToEnd(m2);

            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(newItem.Next, null);
        }


        [TestMethod]
        public void ConstructorWithParamSize()
        {
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(5);

            Assert.AreEqual(5, list.Count);
        }

        [TestMethod]
        public void ConstructorWithParamSizeZeroException()
        {
            try
            {
                MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(0);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Длина меньше 0", ex.Message);
            }
        }

        [TestMethod]
        public void ConstructorWithParamsCollection()
        {
            MusicalInstrument m1 = new MusicalInstrument("m1", 1);
            MusicalInstrument m2 = new MusicalInstrument("m2", 2);
            MusicalInstrument m3 = new MusicalInstrument("m3", 3);
            MusicalInstrument[] elements = { m1, m2, m3 };
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(elements);

            Point<MusicalInstrument> beg = new Point<MusicalInstrument>(elements[0]);
            Point<MusicalInstrument> end = new Point<MusicalInstrument>(elements[elements.Length-1]);
            Assert.AreEqual(elements.Length, list.Count);
            Assert.IsNotNull(end);
            Assert.IsNotNull(beg);
        }

        [TestMethod]
        public void ConstructorWithParamsCollectionExceptionEmpty()
        {
            try
            {
                MusicalInstrument[] elements = { };
                MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(elements);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Нет элементов в списке", ex.Message);
            }
        }

        [TestMethod]
        public void ConstructorWithParamsCollectionExceptionNull()
        {
            try
            {
                MusicalInstrument[] elements = null;
                MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(elements);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Пустой список (null)", ex.Message);
            }
        }

        [TestMethod]
        public void FindBeginName()
        {
            MusicalInstrument m1 = new MusicalInstrument("m1", 1);
            MusicalInstrument m2 = new MusicalInstrument("m2", 2);
            MusicalInstrument m3 = new MusicalInstrument("m3", 3);
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(m1,m2,m3);
            string name = m3.Name;

            Point<MusicalInstrument> pos = list.FindBeginName(name);
            string searchName = pos.Data.Name;

            Assert.AreEqual(searchName, name);
        }

        [TestMethod]
        public void AddAfterMiddle()
        {
            MusicalInstrument m1 = new MusicalInstrument("m1", 1);
            MusicalInstrument m2 = new MusicalInstrument("m2", 2);
            MusicalInstrument m3 = new MusicalInstrument("m3", 3);
            MusicalInstrument m4 = new MusicalInstrument("m4", 4);
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(m1, m2, m3);
            string name = m2.Name;

            list.AddAfter(name, m4);

            Assert.AreEqual(list.Count, 4);
            Assert.IsNotNull(list.FindBeginName(name));
        }

        [TestMethod]
        public void AddAfterLast()
        {
            MusicalInstrument m1 = new MusicalInstrument("m1", 1);
            MusicalInstrument m2 = new MusicalInstrument("m2", 2);
            MusicalInstrument m3 = new MusicalInstrument("m3", 3);
            MusicalInstrument m4 = new MusicalInstrument("m4", 4);
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(m1, m2, m3);
            string name = m3.Name;

            list.AddAfter(name, m4);

            Assert.AreEqual(list.Count, 4);
            Assert.IsNotNull(list.FindBeginName(name));
        }

        [TestMethod]
        public void AddAfterFirst()
        {
            MusicalInstrument m1 = new MusicalInstrument("m1", 1);
            MusicalInstrument m4 = new MusicalInstrument("m4", 4);
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(m1);
            string name = m1.Name;

            list.AddAfter(name, m4);

            Assert.AreEqual(list.Count, 2);
            Assert.IsNotNull(list.FindBeginName(name));
        }

        [TestMethod]
        public void AddAfterException()
        {
            try
            {
                MusicalInstrument m1 = new MusicalInstrument("m1", 1);
                MusicalInstrument m2 = new MusicalInstrument("m2", 2);
                MusicalInstrument m4 = new MusicalInstrument("m4", 4);
                MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(m1,m2);
                string name = m4.Name;
                list.AddAfter(name, m4);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Нет элемента с указанным именем", ex.Message);
            }
        }

        [TestMethod]
        public void FindEndName()
        {
            MusicalInstrument m1 = new MusicalInstrument("m1", 1);
            MusicalInstrument m2 = new MusicalInstrument("m2", 2);
            MusicalInstrument m3 = new MusicalInstrument("m3", 3);
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(m1, m2, m3);
            string name = m1.Name;

            Point<MusicalInstrument> pos = list.FindEndName(name);
            string searchName = pos.Data.Name;

            Assert.AreEqual(searchName, name);
        }

        [TestMethod]
        public void DeleteLastMiddleEelem()
        {
            MusicalInstrument m1 = new MusicalInstrument("m1", 1);
            MusicalInstrument m2 = new MusicalInstrument("m2", 2);
            MusicalInstrument m3 = new MusicalInstrument("m3", 3);
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(m1,m2,m3);
            string name = m2.Name;

            list.DeleteLast(name);

            Assert.AreEqual(list.Count, 2);
            Assert.IsNull(list.FindEndName(name));
        }

        [TestMethod]
        public void DeleteLastFirstElem()
        {
            MusicalInstrument m1 = new MusicalInstrument("m1", 1);
            MusicalInstrument m2 = new MusicalInstrument("m2", 2);
            MusicalInstrument m3 = new MusicalInstrument("m3", 3);
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(m1, m2, m3);
            string name = m1.Name;

            list.DeleteLast(name);

            Assert.AreEqual(list.Count, 2);
            Assert.IsNull(list.FindEndName(name));
        }

        [TestMethod]
        public void DeleteLastLastElem()
        {
            MusicalInstrument m1 = new MusicalInstrument("m1", 1);
            MusicalInstrument m2 = new MusicalInstrument("m2", 2);
            MusicalInstrument m3 = new MusicalInstrument("m3", 3);
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(m1, m2, m3);
            string name = m3.Name;

            list.DeleteLast(name);

            Assert.AreEqual(list.Count, 2);
            Assert.IsNull(list.FindEndName(name));
        }


        [TestMethod]
        public void DeleteLastOneEelem()
        {
            MusicalInstrument m1 = new MusicalInstrument("m1", 1);
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(m1);
            string name = m1.Name;

            list.DeleteLast(name);

            Assert.AreEqual(list.Count, 0);
            Assert.IsNull(list.FindEndName(name));
        }

        [TestMethod]
        public void DeleteLastExceptionNoEelem()
        {
            try
            {
                MusicalInstrument m1 = new MusicalInstrument("m1", 1);
                MusicalInstrument m2 = new MusicalInstrument("m2", 2);
                MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(m2);
                string name = m1.Name;
                list.DeleteLast(name);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Нет элемента с указанным именем", ex.Message);
            }
        }

        [TestMethod]
        public void DeleteLastExceptionNull()
        {
            try
            {
                MusicalInstrument m2 = new MusicalInstrument("m2", 2);
                MyList<MusicalInstrument> list = new MyList<MusicalInstrument>();
                string name = m2.Name;
                list.DeleteLast(name);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Лист пустой", ex.Message);
            }
        }

        [TestMethod]
        public void DeleteList()
        {
            MusicalInstrument m1 = new MusicalInstrument("m1", 1);
            MusicalInstrument m2 = new MusicalInstrument("m2", 2);
            MusicalInstrument m3 = new MusicalInstrument("m3", 3);
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(m1, m2, m3);

            list.DeleteList();

            Assert.AreEqual(list.Count, 0);
        }

        [TestMethod]
        public void CloneList()
        {
            MusicalInstrument m1 = new MusicalInstrument("m1", 1);
            MusicalInstrument m2 = new MusicalInstrument("m2", 2);
            MusicalInstrument m3 = new MusicalInstrument("m3", 3);
            MyList<MusicalInstrument> list = new MyList<MusicalInstrument>(m1, m2, m3);
            MyList<MusicalInstrument> clonedList = list.Clone();
            Assert.AreEqual(list.Count, clonedList.Count);

            MusicalInstrument m4 = new MusicalInstrument();
            m4.IRandomInit();
            clonedList.ChangeItem(m4, clonedList);
            Assert.AreNotEqual(clonedList, list);
        }

        [TestMethod]
        public void ChangeItemException()
        {
            try
            {
                MusicalInstrument m1 = new MusicalInstrument("m1", 1);
                MyList<MusicalInstrument> list = new MyList<MusicalInstrument>();

                list.ChangeItem(m1,list);
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Лист пустой", ex.Message);
            }
        }

        // Класс Point
        [TestMethod]
        public void ConstructorWithoutParam()
        {
            Point<MusicalInstrument> p = new Point<MusicalInstrument>();
            Assert.IsNull(p.Next);
            Assert.IsNull(p.Prev);
            Assert.IsNull(p.Data);        
        }


        [TestMethod]
        public void ToStringNull()
        {
            Point<MusicalInstrument> p = new Point<MusicalInstrument>();
            var res1 = p.ToString();
            Assert.AreEqual(res1, "");
        }

        [TestMethod]
        public void ToString()
        {
            MusicalInstrument m = new MusicalInstrument("m", 44);
            Point<MusicalInstrument> p = new Point<MusicalInstrument>(m);
            var res1 = p.ToString();
            Assert.AreEqual(res1, "44: m,");
        }
    }
}