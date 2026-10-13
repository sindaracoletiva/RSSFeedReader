namespace RSSFeedReader.Api.Validation;

public static class UrlValidator
{
    public static (bool IsValid, string? Message) Validate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return (false, "Enter a valid RSS or Atom feed URL.");
        }

        var trimmed = value.Trim();

        if (!Uri.TryCreate(trimmed, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps) ||
            string.IsNullOrWhiteSpace(uri.Host))
        {
            return (false, "The feed URL is malformed or not a valid HTTP/HTTPS address.");
        }

        if (trimmed.Contains(' '))
        {
            return (false, "The feed URL cannot contain spaces.");
        }

        return (true, null);
    }
}
