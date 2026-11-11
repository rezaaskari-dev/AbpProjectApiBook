using System;
using System.Collections.Generic;
using System.Text;

namespace AbpApiBook.Books.Dto.Search
{

    public class BookSearchInputDto
    {
        public string SearchTerm { get; set; } = string.Empty;
    }
}
