using System;
using System.Collections.Generic;
using System.Linq;
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
            SeventhExersize(list);

            // 8.Сделать две выборки годов издания книг, одну по автору Архангельский, другую по Омельченко. Вывести все года в которые издавались оба этих автора.


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
    }
}
