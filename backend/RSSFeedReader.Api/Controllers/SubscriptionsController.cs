using Microsoft.AspNetCore.Mvc;
using RSSFeedReader.Api.Models;
using RSSFeedReader.Api.Services;
using RSSFeedReader.Api.Validation;

namespace RSSFeedReader.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionsController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Subscription>> GetSubscriptions()
    {
        return _subscriptionService.GetAll().ToList();
    }

    [HttpPost]
    public ActionResult<Subscription> AddSubscription([FromBody] CreateSubscriptionRequest request)
    {
        if (request == null)
        {
            return BadRequest("Enter a valid RSS or Atom feed URL.");
        }

        var validationResult = UrlValidator.Validate(request.Url);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Message);
        }

        var subscription = _subscriptionService.Add(request.Url!);
        return subscription;
    }
}
