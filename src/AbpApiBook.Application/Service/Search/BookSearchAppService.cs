using AbpApiBook.Books.Dto.Search;
using Elastic.Clients.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace AbpApiBook.Service.Search;

public class BookSearchAppService : ApplicationService
{
    private readonly ElasticsearchClient _elasticClient;

    public BookSearchAppService(ElasticsearchClient elasticClient)
    {
        _elasticClient = elasticClient;
    }

    public async Task<List<BookSearchResultDto>> SearchAsync(
        BookSearchInputDto input)
    {
        var response = await _elasticClient.SearchAsync<BookSearchResultDto>(
            s => s
                .Indices("books")
                //.Query(q => q
                //    .Match(m => m
                //        .Field(f => f.Title)
                //        .Query(input.SearchTerm)
                //    )
                //)
                .Query(q => q
    .MultiMatch(m => m
        .Fields(new[] { "title", "author" })
        .Query(input.SearchTerm)
    )
)
        );

        if (!response.IsValidResponse)
        {
            throw new Exception("Elasticsearch search failed.");
        }

        return response.Documents.ToList();
    }
}