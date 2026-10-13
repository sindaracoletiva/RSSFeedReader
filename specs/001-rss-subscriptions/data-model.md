# Data Model: RSS Subscription MVP

## Core Entities

### Subscription

Represents one feed URL the user wants to track during the current session.

| Field | Type | Description | Validation |
|-------|------|-------------|------------|
| id | string | Unique identifier for the subscription in memory | Required |
| url | string | Original feed URL entered by the user | Must be non-empty, trimmed, and parse as a valid absolute URI |
| addedAt | datetime | Time when the subscription was accepted | Required |

**Relationships**
- A Subscription belongs to a single in-memory SubscriptionList.
- A SubscriptionList contains zero or more accepted subscriptions.

### SubscriptionList

Represents the currently visible set of subscriptions in the running app session.

| Field | Type | Description | Validation |
|-------|------|-------------|------------|
| subscriptions | array<Subscription> | The active list of accepted feeds | Must only contain valid subscriptions |

**Relationships**
- Owned by the application session only.
- No persistence or cross-session storage is required for this MVP.

## Validation Rules

- Empty, null, or whitespace-only values must be rejected.
- Malformed URLs must be rejected with a clear user-facing message.
- The app should reject obviously invalid values before adding to the list.
- Feed URLs are accepted as provided by the user; there is no feed-fetching or feed-content validation in MVP.
- Duplicate URLs may be treated as invalid or ignored in the current session to keep the list clean, but this is kept as a simple UI/API validation decision rather than a production system requirement.

## State Transitions

1. Idle: user is ready to submit a URL.
2. Submitted: user enters a value and triggers validation.
3. Accepted: valid URL is added to the in-memory list.
4. Rejected: invalid input is blocked and the list remains unchanged.

No persistence, background synchronization, or feed-refresh state is included in the MVP model.
