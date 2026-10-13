using System.Net.Http.Json;

namespace RSSFeedReader.UI.Services;

public class SubscriptionClient
{
    private readonly HttpClient _httpClient;

    public SubscriptionClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IReadOnlyList<SubscriptionItem>> GetSubscriptionsAsync()
    {
        var result = await _httpClient.GetFromJsonAsync<List<SubscriptionItem>>("/api/subscriptions");
        return result ?? new List<SubscriptionItem>();
    }

    public async Task<SubscriptionItem?> AddSubscriptionAsync(string url)
    {
        var response = await _httpClient.PostAsJsonAsync("/api/subscriptions", new { url });

        if (!response.IsSuccessStatusCode)
        {
            var message = await response.Content.ReadAsStringAsync();
            throw new InvalidOperationException(string.IsNullOrWhiteSpace(message) ? "The URL could not be added." : message.Trim());
        }

        return await response.Content.ReadFromJsonAsync<SubscriptionItem>();
    }
}

public class SubscriptionItem
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
