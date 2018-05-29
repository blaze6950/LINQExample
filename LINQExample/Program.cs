using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace LINQExample
{
    class Program
    {
        static void Main(string[] args)
        {
            DbLibrary db = new DbLibrary();
            var list = db.GetListBooks();

            // 1. Выбрать все книги определенной категории
            //FirstExersize(list, "C++ Builder");

            // 2.Выбрать название и количество книг в библиотеке автора Архангельский.
            //SecondExersize(list);

            // 3.Отсортировать все книги по году
            //ThirdExersize(list);

            // 4.Отсортировать все книги по издательству и году
            //FourthExersize(list);

            // 5.Посчитать количество книг определенного издательства
            //FifthExersize(list, "Бином");

            // 6.Найти самую старую книгу
            //SixthExersize(list);

            // 7.Выбрать все категории без повторений
            //SeventhExersize(list);

            // 8.Сделать две выборки годов издания книг, одну по автору Архангельский, другую по Омельченко. Вывести все года в которые издавались оба этих автора.
            //EighthExersize(list);

            ///////////29.05.2018///////////

            // 1. Выбрать все книги, сгруппировав по категории
            //NinethExersize(list);

            // 2.Выбрать все книги определенного издательства, сгруппировав по авторам
            //TenthExersize(list, "BHV");

            // 3. В класс Book добавить массив студентов, которые ее брали и с помощью LINQ отобразить все книги и студентов.
            //list = db.GetListBooksWithStudents();
            //EleventhExersize(list);
        }

        


        private static void FirstExersize(IEnumerable<Book> bookList, string category)
        {
            var res = (from b in bookList
                       where b.Category.Equals(category)
                       select b);

            Print(res);
        }
        private static void Print(IEnumerable<Book> bookList)
        {
            int i = 1;
            foreach (var book in bookList)
            {
                Console.WriteLine($"{i}. | \"{book.Name}\" {book.Category} {book.Theme} {book.YearPress} {book.Press} {book.AuthorFirstName} {book.AuthorLastName} {book.Quantity}");
                i++;
            }
        }

        private static void SecondExersize(IEnumerable<Book> bookList)
        {
            var res = (from b in bookList
                       where b.AuthorLastName.Equals("Архангельский")
                       select new{Name = b.Name, Quantity = b.Quantity});

            int i = 1;
            foreach (var b in res)
            {
                Console.WriteLine($"{i}. | {b.Name} --- {b.Quantity}");
                i++;
            }
        }

        private static void ThirdExersize(IEnumerable<Book> bookList)
        {
            var res = (from b in bookList
                       orderby b.YearPress
                       select b);

            Print(res);
        }

        private static void FourthExersize(IEnumerable<Book> bookList)
        {
            var res = (from b in bookList
                       orderby b.Press, b.YearPress
                       select b);

            Print(res);
        }

        private static void FifthExersize(IEnumerable<Book> bookList, string press)
        {
            var res = (from b in bookList
                       where b.Press == press
                       select b).Count();

            Console.WriteLine($"{press} - {res} books");
        }

        private static void SixthExersize(IEnumerable<Book> bookList)
        {
            var res = (from b in bookList
                       where b.YearPress == (from b1 in bookList select b1.YearPress).Min()
                       select b);

            Print(res);
        }

        private static void SeventhExersize(IEnumerable<Book> bookList)
        {
            var res = (from b in bookList
                       select b.Category).Distinct();

            foreach (var r in res)
            {
                Console.WriteLine(r);
            }
        }

        private static void EighthExersize(IEnumerable<Book> bookList)
        {
            var first = (from b in bookList
                         where b.AuthorLastName == "Архангельский"
                         select b.YearPress);

            //foreach (var r in first)
            //{
            //    Console.WriteLine(r.ToString());
            //}
            //Console.WriteLine("====================================");

            var second = (from b in bookList
                          where b.AuthorLastName == "Омельченко"
                          select b.YearPress);

            //foreach (var r in second)
            //{
            //    Console.WriteLine(r.ToString());
            //}
            //Console.WriteLine("====================================");

            var res = first.Union(second).Distinct();

            foreach (var r in res)
            {
                Console.WriteLine(r.ToString());
            }
        }

        private static void NinethExersize(IEnumerable<Book> bookList)
        {
            var res = (from b in bookList
                group b by b.Category
                into g
                select new {Category = g.Key, Items = g});

            foreach (var r in res)
            {
                Console.WriteLine(r.Category + ":");
                foreach (var p in r.Items)
                {
                    Console.WriteLine("\t" + p.Name);
                }
            }
        }

        private static void TenthExersize(IEnumerable<Book> bookList, String press)
        {
            var res = (from b in bookList
                where b.Press == press
                group b by b.AuthorLastName
                into g
                select new {Author = g.Key, Books = g});

            foreach (var r in res)
            {
                Console.WriteLine(r.Author + ":");
                foreach (var b in r.Books)
                {
                    Console.WriteLine("\t" + b.Name);
                }
            }
        }

        private static void EleventhExersize(IEnumerable<Book> bookList)
        {
            var res = (from b in bookList
                       from s in b.Students
                       select s);

            foreach (var r in res)
            {
                Console.WriteLine(r.FirstName + " " + r.LastName);
            }
        }
    }
}
