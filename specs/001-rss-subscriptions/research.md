# Research: RSS Subscription MVP

## Decision Log

- Decision: Build the MVP as a small ASP.NET Core Web API + Blazor WebAssembly application with a single in-memory subscription list.
  - Rationale: The project goals and technology notes explicitly define this architecture as the preferred foundation for a minimal but extendable app. It keeps the MVP small, reviewable, and aligned with the constitution's requirement for minimal maintainable scope.
  - Alternatives considered: single-project app, a more elaborate SPA, or adding persistence/URL-fetching complexity immediately.
  - Why rejected: these options either expand scope beyond the MVP or introduce unnecessary architecture before the core user story is proven.

- Decision: Keep feed fetching, parsing, persistence, and production-grade validation out of the MVP.
  - Rationale: The feature spec is explicitly scoped to subscription management only. The constitution prohibits broadening scope without justification.
  - Alternatives considered: validating feed content, storing subscriptions in a database, or polling feeds in the background.
  - Why rejected: these features are intentionally deferred to extended MVP or later phases.

- Decision: Use basic URL validation only at the UI/API boundary for empty, whitespace-only, malformed, and obviously invalid values.
  - Rationale: The constitution requires security by default, but the MVP must not add production-grade verification or feed-content inspection. The validation rule is limited to rejecting unusable inputs and surfacing clear feedback.
  - Alternatives considered: full feed discovery, HTTP reachability tests, or full URI normalization rules.
  - Why rejected: they exceed the minimal MVP and risk drifting into network or parsing work that is explicitly out of scope.

- Decision: Model the subscription list as an in-memory collection for the current session only.
  - Rationale: The project goals explicitly say the app is a local MVP and in-memory storage is acceptable. This keeps the first implementation easy to test and aligns with the repository's minimal-scope approach.
  - Alternatives considered: database storage or local file storage.
  - Why rejected: both add persistence complexity that the project deliberately defers.

## Research Summary

The repository documents already define the likely solution shape: a cross-platform ASP.NET Core API and Blazor UI, with a minimal local demonstration focused on adding subscriptions and showing the list. No unresolved requirement blocks the MVP; the only guardrail is to keep the solution intentionally narrow and enforce a small validation layer instead of feed parsing or production-grade network validation.
