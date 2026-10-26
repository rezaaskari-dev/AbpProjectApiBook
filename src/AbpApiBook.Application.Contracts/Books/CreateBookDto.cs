using System;
using System.Collections.Generic;
using System.Text;

namespace AbpApiBook.Books
{
    public class CreateBookDto
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public int PublishedYear { get; set; }
    }
}