using RSSFeedReader.Api.Controllers;
using RSSFeedReader.Api.Models;
using RSSFeedReader.Api.Services;

namespace RSSFeedReader.Api.Tests;

public class SubscriptionControllerTests
{
    [Fact]
    public void AddSubscription_AcceptsValidUrl_AndStoresIt()
    {
        var service = new SubscriptionService();
        var controller = new SubscriptionsController(service);

        var result = controller.AddSubscription(new CreateSubscriptionRequest
        {
            Url = "https://example.com/feed.xml"
        });

        Assert.NotNull(result.Value);
        Assert.Single(service.GetAll());
        Assert.Equal("https://example.com/feed.xml", result.Value!.Url);
    }

    [Fact]
    public void AddSubscription_RejectsEmptyOrMalformedUrl_WithoutChangingTheList()
    {
        var service = new SubscriptionService();
        var controller = new SubscriptionsController(service);

        controller.AddSubscription(new CreateSubscriptionRequest { Url = "" });
        controller.AddSubscription(new CreateSubscriptionRequest { Url = " " });
        controller.AddSubscription(new CreateSubscriptionRequest { Url = "not a url" });

        Assert.Empty(service.GetAll());
    }

    [Fact]
    public void GetSubscriptions_ReturnsCurrentSessionSubscriptions()
    {
        var service = new SubscriptionService();
        var controller = new SubscriptionsController(service);

        controller.AddSubscription(new CreateSubscriptionRequest { Url = "https://example.com/feed.xml" });

        var result = controller.GetSubscriptions();

        var items = result.Value!.ToList();
        Assert.Single(items);
        Assert.Equal("https://example.com/feed.xml", items[0].Url);
    }

    [Fact]
    public void AddSubscription_PreservesMultipleUrls_InTheCurrentSession()
    {
        var service = new SubscriptionService();
        var controller = new SubscriptionsController(service);

        controller.AddSubscription(new CreateSubscriptionRequest { Url = "https://example.com/feed.xml" });
        controller.AddSubscription(new CreateSubscriptionRequest { Url = "https://example.org/news.xml" });

        var items = controller.GetSubscriptions().Value!.ToList();

        Assert.Equal(2, items.Count);
        Assert.Equal("https://example.com/feed.xml", items[0].Url);
        Assert.Equal("https://example.org/news.xml", items[1].Url);
    }
}
