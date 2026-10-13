# Quickstart: RSS Subscription MVP

## Prerequisites

- A local development environment for the ASP.NET Core + Blazor project
- The backend and frontend are both running on their configured local ports
- A browser is available to interact with the UI

## Validation Scenarios

### 1. Accept a valid feed URL

1. Open the app in the browser.
2. Enter a valid-looking RSS or Atom feed URL.
3. Submit the form.
4. Confirm the subscription appears in the UI list.

Expected outcome: The feed is added immediately and the list updates without requiring any additional workflow.

### 2. Reject empty or malformed input

1. Open the app in the browser.
2. Leave the input empty or enter a malformed URL such as `not a url`.
3. Submit the form.
4. Confirm no new entry is added to the list.

Expected outcome: The app displays a clear validation message and the current list remains unchanged.

### 3. Confirm minimal MVP scope

1. Attempt to add a subscription.
2. Verify the app does not fetch or parse feed content as part of the add flow.
3. Confirm there is no persistence layer or background refresh process in the MVP.

Expected outcome: The app remains focused on subscription management and does not expand into feed reading or production-style validation.

## Success Signals

- Valid URLs are accepted and shown in the UI quickly.
- Invalid submissions do not alter the list.
- The app stays within the defined MVP scope and does not trigger feed-fetching logic.
