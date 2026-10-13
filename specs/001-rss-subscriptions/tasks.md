# Tasks: RSS Subscription MVP

**Input**: Design documents from `/specs/001-rss-subscriptions/`

**Prerequisites**: plan.md (required), spec.md (required for user stories), research.md, data-model.md, contracts/

**Organization**: Tasks are grouped by user story to enable independent implementation and testing of each story.

## Format: `[ID] [P?] [Story] Description`

- **[P]**: Can run in parallel (different files, no dependencies)
- **[Story]**: Which user story this task belongs to (e.g., US1, US2)
- Include exact file paths in descriptions

## Phase 1: Setup (Shared Infrastructure)

**Purpose**: Project initialization and core structure for the minimal MVP

- [ ] T001 Create the backend and frontend project structure required by the implementation plan in backend/ and frontend/
- [ ] T002 [P] Initialize the ASP.NET Core Web API project skeleton in backend/RSSFeedReader.Api/
- [ ] T003 [P] Initialize the Blazor WebAssembly UI project skeleton in frontend/RSSFeedReader.UI/
- [ ] T004 [P] Configure launch settings and API base URL alignment between backend/RSSFeedReader.Api/Properties/launchSettings.json and frontend/RSSFeedReader.UI/wwwroot/appsettings.json

---

## Phase 2: Foundational (Blocking Prerequisites)

**Purpose**: Core infrastructure that MUST be complete before any story work begins

**⚠️ CRITICAL**: No user story work can begin until this phase is complete

- [ ] T005 Define the in-memory subscription model in backend/RSSFeedReader.Api/Models/Subscription.cs with fields for an identifier, the user-entered URL, and creation time
- [ ] T006 [P] Implement the in-memory subscription store and list operations in backend/RSSFeedReader.Api/Services/SubscriptionService.cs
- [ ] T007 [P] Add the basic URL validation helper in backend/RSSFeedReader.Api/Validation/UrlValidator.cs to reject empty, whitespace-only, malformed, and obviously invalid values with a clear message
- [ ] T008 Implement the subscription API endpoints in backend/RSSFeedReader.Api/Controllers/SubscriptionsController.cs for listing and adding subscriptions
- [ ] T009 Configure CORS and startup wiring in backend/RSSFeedReader.Api/Program.cs so the UI can call the API locally without expanding MVP scope

**Checkpoint**: Foundation ready - user story implementation can now begin in parallel

---

## Phase 3: User Story 1 - Add a subscription to the feed list (Priority: P1) 🎯 MVP

**Goal**: Enable a user to submit a valid-looking feed URL and add it to the in-memory list immediately while rejecting invalid input with clear feedback.

**Independent Test**: A user can enter a valid feed URL, submit it, and confirm it appears in the list; invalid input triggers a validation message and leaves the list unchanged.

### Tests for User Story 1 (MUST be written before implementation)

- [ ] T010 [P] [US1] Add a .NET test for valid subscription creation in backend/RSSFeedReader.Api.Tests/SubscriptionControllerTests.cs that asserts a valid URL is accepted and stored in memory
- [ ] T011 [P] [US1] Add a .NET test for invalid URLs in backend/RSSFeedReader.Api.Tests/SubscriptionControllerTests.cs that asserts empty, whitespace-only, malformed, and obviously invalid URLs are rejected without changing the list
- [ ] T012 [P] [US1] Add a .NET test for GET/list behavior in backend/RSSFeedReader.Api.Tests/SubscriptionControllerTests.cs that asserts the list returns accepted subscriptions in the current session
- [ ] T013 [P] [US1] Add a .NET test for multiple subscriptions in backend/RSSFeedReader.Api.Tests/SubscriptionControllerTests.cs that asserts multiple valid entries are preserved and remain in the current session list

### Implementation for User Story 1

- [ ] T014 [P] [US1] Create the subscription form and state management in frontend/RSSFeedReader.UI/Pages/Subscriptions.razor
- [ ] T015 [P] [US1] Add the frontend subscription client in frontend/RSSFeedReader.UI/Services/SubscriptionClient.cs to post URLs to POST /api/subscriptions
- [ ] T016 [US1] Implement backend add-subscription logic in backend/RSSFeedReader.Api/Services/SubscriptionService.cs and backend/RSSFeedReader.Api/Controllers/SubscriptionsController.cs to accept valid URLs, reject invalid input with clear feedback, and keep the list in memory only
- [ ] T017 [US1] Implement the UI add-subscription flow in frontend/RSSFeedReader.UI/Pages/Subscriptions.razor so the list updates immediately and invalid submissions do not change the current list
- [ ] T018 [US1] Add the empty and malformed input feedback messaging in frontend/RSSFeedReader.UI/Pages/Subscriptions.razor to reflect the basic validation requirement without introducing feed-checking or production-grade validation

**Checkpoint**: At this point, User Story 1 should be fully functional and testable independently

---

## Phase 4: User Story 2 - Review all current subscriptions (Priority: P1)

**Goal**: Display the active list of subscriptions clearly and keep it visible and trustworthy during the current session.

**Independent Test**: A user can open the page and confirm the list contains every accepted subscription without any missing or stale entries.

### Tests for User Story 2 (MUST be written before implementation)

- [ ] T019 [P] [US2] Add a .NET test for GET/list behavior in backend/RSSFeedReader.Api.Tests/SubscriptionControllerTests.cs that asserts the list endpoint returns all accepted subscriptions for the current session
- [ ] T020 [P] [US2] Add a .NET test for multiple valid subscriptions in backend/RSSFeedReader.Api.Tests/SubscriptionControllerTests.cs that asserts the list preserves all accepted entries and order remains consistent for the session

### Implementation for User Story 2

- [ ] T021 [P] [US2] Create the subscription list display in frontend/RSSFeedReader.UI/Pages/Subscriptions.razor to render accepted entries as individual rows
- [ ] T022 [US2] Add the GET /api/subscriptions data loading path in frontend/RSSFeedReader.UI/Services/SubscriptionClient.cs and wire the page lifecycle to refresh the list
- [ ] T023 [US2] Implement the empty-state and multi-entry presentation in frontend/RSSFeedReader.UI/Pages/Subscriptions.razor for the no-subscriptions and multiple-subscriptions cases described in the spec

**Checkpoint**: At this point, User Stories 1 and 2 should both work independently

---

## Phase 5: Polish & Cross-Cutting Concerns

**Purpose**: Confirm the MVP remains disciplined and does not expand into feed fetching or persistence

- [ ] T024 [P] Review all implementation files to ensure the app stays within MVP scope and avoids feed fetching, parsing, persistence, and production-grade validation beyond the required basic URL checks
- [ ] T025 Run the .NET test suite for the feature in backend/RSSFeedReader.Api.Tests/ to confirm valid creation, rejection of invalid input, GET/list behavior, and multiple-subscription retention all pass
- [ ] T026 [P] Update any feature documentation or developer notes needed to describe the limited MVP boundary and the validation rules for invalid URLs

---

## Dependencies & Execution Order

### Phase Dependencies

- **Setup (Phase 1)**: No dependencies - can start immediately
- **Foundational (Phase 2)**: Depends on Setup completion - BLOCKS all user stories
- **User Story phases (Phase 3 and 4)**: Depend on Foundational completion
- **Polish (Phase 5)**: Depends on all desired story tasks being complete

### User Story Dependencies

- **User Story 1 (P1)**: Can start after Foundational (Phase 2) and does not depend on User Story 2
- **User Story 2 (P1)**: Can start after Foundational (Phase 2) and does not depend on User Story 1, although it should reflect the same in-memory data model

### Parallel Opportunities

- Setup tasks T002-T004 can run in parallel
- Foundational tasks T006-T009 can run in parallel once T005 is complete
- User Story 1 tests T010-T013 can run in parallel before implementation, and implementation tasks T014-T018 can run in parallel after the tests are added
- User Story 2 tests T019-T020 can run in parallel before implementation, and implementation tasks T021-T023 can run in parallel once the list model and API are in place
- Final polish tasks T024-T026 can run in parallel after story completion

---

## Implementation Strategy

### MVP First

1. Complete Phase 1: Setup
2. Complete Phase 2: Foundational
3. Write tests for User Story 1 and User Story 2 before implementation
4. Complete Phase 3: User Story 1
5. Complete Phase 4: User Story 2
6. Run the .NET tests and validate the quickstart scenarios while confirming the app stays within the narrow MVP boundary

### Incremental Delivery

1. Keep the project focused on add-and-list subscriptions only.
2. Validate user input at the boundary without adding feed parsing or fetch logic.
3. Extend only after the MVP is confirmed and future work is explicitly approved.
