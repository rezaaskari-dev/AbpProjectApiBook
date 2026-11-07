using System;
using System.Linq;
using System.Threading.Tasks;
using AbpApiBook.Entities;
using Elastic.Clients.Elasticsearch;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AbpApiBook.Elasticsearch
{
    public class BookIndexingAppService : ApplicationService
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly ElasticsearchClient _elasticClient;

        public BookIndexingAppService(
            IRepository<Book, Guid> bookRepository,
            ElasticsearchClient elasticClient)
        {
            _bookRepository = bookRepository;
            _elasticClient = elasticClient;
        }

        public async Task<int> ImportBooksAsync()
        {
            var books = await _bookRepository.GetListAsync();

            var documents = books.Select(book => new BookSearchDocument
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                PublishedYear = book.PublishedYear
            }).ToList();

            var response = await _elasticClient.BulkAsync(b => b
                .Index("books-v2")
                .IndexMany(documents, (operation, document) =>
                    operation.Id(document.Id)
                )
            );

            if (!response.IsValidResponse || response.Errors)
            {
                throw new Exception(
                    $"Elasticsearch bulk indexing failed: {response.DebugInformation}"
                );
            }

            return documents.Count;
        }
    }
}