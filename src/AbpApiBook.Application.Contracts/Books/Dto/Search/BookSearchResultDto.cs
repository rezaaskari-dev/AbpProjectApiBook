using System;
using System.Collections.Generic;
using System.Text;

namespace AbpApiBook.Books.Dto.Search
{

    public class BookSearchResultDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        public double Price { get; set; }

        public int PublishedYear { get; set; }
    }
}
