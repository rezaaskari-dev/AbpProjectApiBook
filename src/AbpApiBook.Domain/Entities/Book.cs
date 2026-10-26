
using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace AbpApiBook.Entities
{
    public class Book : AuditedAggregateRoot<Guid>
    {
        public string Title { get; private set; } = string.Empty;

        public string Author { get; private set; } = string.Empty;

        public int PublishedYear { get; private set; }

        protected Book()
        {
        }

        public Book(
            Guid id,
            string title,
            string author,
            int publishedYear)
            : base(id)
        {
            SetDetails(title, author, publishedYear);
        }

        public void Update(
            string title,
            string author,
            int publishedYear)
        {
            SetDetails(title, author, publishedYear);
        }

        private void SetDetails(
            string title,
            string author,
            int publishedYear)
        {
            ValidateTitle(title);
            ValidateAuthor(author);
            ValidatePublishedYear(publishedYear);

            Title = title;
            Author = author;
            PublishedYear = publishedYear;
        }

        private static void ValidateTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException(
                    "عنوان کتاب نمی‌تواند خالی باشد.",
                    nameof(title));
            }

            if (title.Trim().Length < 3)
            {
                throw new ArgumentException(
                    "عنوان کتاب باید حداقل 3 کاراکتر باشد.",
                    nameof(title));
            }
        }

        private static void ValidateAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                throw new ArgumentException(
                    "نام نویسنده نمی‌تواند خالی باشد.",
                    nameof(author));
            }

            if (author.Trim().Length < 4)
            {
                throw new ArgumentException(
                    "نام نویسنده باید حداقل 4 کاراکتر باشد.",
                    nameof(author));
            }
        }

        private static void ValidatePublishedYear(int publishedYear)
        {
            if (publishedYear < 1000 || publishedYear > 9999)
            {
                throw new ArgumentException(
                    "سال انتشار باید دقیقا 4 رقمی باشد؛ برای مثال 2001 یا 2008.",
                    nameof(publishedYear));
            }
        }
    }
}
