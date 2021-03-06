﻿using System;
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

        public Book(int id, string name, string press, int yearPress, string category, string theme, string authorLastName, string authorFirstName, int quantity)
        {
            Id = id;
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

        public Book(int id, string name, string press, int yearPress, string category, string theme, string authorLastName, string authorFirstName, int quantity, List<Student> students) : this(id, name, press, yearPress, category, theme, authorLastName, authorFirstName, quantity)
        {
            Students = students;
        }

        public int Id { get; set; }
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
