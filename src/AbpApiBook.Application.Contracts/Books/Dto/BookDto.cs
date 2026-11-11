using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace AbpApiBook.Books.Dto
{
    public class BookDto : EntityDto<Guid>
    {

        public string Title { get; set; }

        public string Author { get; set; }

        public int PublishedYear { get; set; }

        public string? ImageUrl { get; set; }
    }
}
