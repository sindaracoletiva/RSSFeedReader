using RSSFeedReader.Api.Models;
using RSSFeedReader.Api.Validation;

namespace RSSFeedReader.Api.Services;

public interface ISubscriptionService
{
    IReadOnlyCollection<Subscription> GetAll();
    Subscription Add(string url);
}

public class SubscriptionService : ISubscriptionService
{
    private readonly List<Subscription> _subscriptions = new();

    public IReadOnlyCollection<Subscription> GetAll()
    {
        return _subscriptions.AsReadOnly();
    }

    public Subscription Add(string url)
    {
        var validationResult = UrlValidator.Validate(url);
        if (!validationResult.IsValid)
        {
            throw new ArgumentException(validationResult.Message);
        }

        var subscription = new Subscription
        {
            Url = url.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        _subscriptions.Add(subscription);
        return subscription;
    }
}
