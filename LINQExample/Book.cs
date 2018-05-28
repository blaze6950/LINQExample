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

        public Book(string name, string press, string yearPress, string category, string theme, string authorLastName, string authorFirstName, string quantity)
        {
            Name = name;
            Press = press;
            YearPress = yearPress;
            Category = category;
            Theme = theme;
            AuthorLastName = authorLastName;
            AuthorFirstName = authorFirstName;
            Quantity = quantity;
        }

        public String Name { get; set; }
        public String Press { get; set; }
        public String YearPress { get; set; }
        public String Category { get; set; }
        public String Theme { get; set; }
        public String AuthorLastName { get; set; }
        public String AuthorFirstName { get; set; }
        public String Quantity { get; set; }
    }
}
