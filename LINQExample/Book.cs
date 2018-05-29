using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQExample
{
    class Book
    {
        public Book()
        {
        }

        public Book(string name, string press, int yearPress, string category, string theme, string authorLastName, string authorFirstName, int quantity)
        {
            Name = name;
            Press = press;
            YearPress = yearPress;
            Category = category;
            Theme = theme;
            AuthorLastName = authorLastName;
            AuthorFirstName = authorFirstName;
            Quantity = quantity;
            Students = new List<Student>();
        }

        public Book(string name, string press, int yearPress, string category, string theme, string authorLastName, string authorFirstName, int quantity, List<Student> students)
        {
            Name = name;
            Press = press;
            YearPress = yearPress;
            Category = category;
            Theme = theme;
            AuthorLastName = authorLastName;
            AuthorFirstName = authorFirstName;
            Quantity = quantity;
            Students = students;
        }

        public String Name { get; set; }
        public String Press { get; set; }
        public int YearPress { get; set; }
        public String Category { get; set; }
        public String Theme { get; set; }
        public String AuthorLastName { get; set; }
        public String AuthorFirstName { get; set; }
        public int Quantity { get; set; }
        public List<Student> Students{ get; set; }
    }
}
