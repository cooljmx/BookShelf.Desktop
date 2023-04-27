using System;
using System.Net.Http;
using System.Threading.Tasks;
using BookShelf.Domain.Rest;
using Newtonsoft.Json;

namespace BookShelf.Infrastructure.Rest;

internal class ApiRequestExecutor : IApiRequestExecutor
{
    private readonly Uri _baseAddress = new("http://localhost:50000");
    private readonly IHttpClientFactory _httpClientFactory;

    public ApiRequestExecutor(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<TResponse> GetAsync<TResponse>(string request)
    {
        using var httpClient = _httpClientFactory.CreateClient();
        httpClient.BaseAddress = _baseAddress;

        var httpResponseMessage = await httpClient.GetAsync(request);

        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<TResponse>(content);

        if (response != null)
            return response;

        throw new InvalidOperationException("Response can't be null");
    }
}