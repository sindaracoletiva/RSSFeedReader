# Subscription API Contract

## Overview

This project exposes a minimal contract for managing the subscription list during the MVP. The API is intentionally small and does not include feed fetching or parsing endpoints.

## Endpoints

### GET /api/subscriptions

Returns the list of currently accepted subscriptions in the running app session.

**Success response**
- Status: `200 OK`
- Content type: `application/json`
- Example:

```json
[
  {
    "id": "sub-123",
    "url": "https://example.com/feed.xml",
    "addedAt": "2026-10-13T15:00:00Z"
  }
]
```

### POST /api/subscriptions

Adds a new subscription after basic validation.

**Request body**

```json
{
  "url": "https://example.com/feed.xml"
}
```

**Success response**
- Status: `201 Created`
- Content type: `application/json`
- Example:

```json
{
  "id": "sub-124",
  "url": "https://example.com/feed.xml",
  "addedAt": "2026-10-13T15:00:00Z"
}
```

**Validation error response**
- Status: `400 Bad Request`
- Content type: `application/json`
- Example:

```json
{
  "message": "Please enter a valid feed URL."
}
```

## Scope Boundaries

- No feed-content parsing endpoints are included.
- No persistence or refresh endpoints are included.
- No production-grade URL validation is required beyond rejecting empty or malformed values with clear feedback.
