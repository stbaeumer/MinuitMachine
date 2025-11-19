# Copilot Instructions for MinuitMachine

These instructions help AI coding agents be productive in this repo.

## Project Overview
- Type: Blazor WebAssembly client (`net8.0`).
- Goal: Call a Swagger-described HTTP API using Basic Auth.
- Location: Blazor app in `src/MinuitMachine.Client`.

## Key Files & Structure
- `src/MinuitMachine.Client/MinuitMachine.Client.csproj`: Blazor WASM project.
- `src/MinuitMachine.Client/Program.cs`: DI registrations and app bootstrap.
- `src/MinuitMachine.Client/Services/CredentialStore.cs`: Holds API base URL and Basic credentials; builds `Authorization` header.
- `src/MinuitMachine.Client/Services/ApiClient.cs`: Minimal HTTP wrapper adding Basic Auth per request.
- `src/MinuitMachine.Client/Pages/ApiTest.razor`: UI to input Base URL, username, password; test GET and load Swagger JSON.
- `src/MinuitMachine.Client/wwwroot/index.html`: Blazor boot page; `wwwroot/css/app.css` basic styling.

## Running Locally
- Requires .NET SDK 8.x on Linux/macOS/Windows.
- Commands:
  - Restore/build: `dotnet build src/MinuitMachine.Client`
  - Run dev server: `dotnet run --project src/MinuitMachine.Client`
  - Open: `http://localhost:5179` (HTTPS variant typically available).

## API Access & Auth Pattern
- Auth: HTTP Basic (`Authorization: Basic <base64(user:pass)>`).
- Credentials & Base URL are stored in `CredentialStore` (in-memory; not persisted).
- Requests are constructed by `ApiClient`, which attaches the header for each request.
- UI (`/apitest`) lets users set:
  - Base URL (e.g., `https://api.example.com`)
  - Swagger JSON path (default `/swagger/v1/swagger.json`)
  - Arbitrary test path (default `/api/health`)

## Swagger Integration (NSwag — planned)
- Generation is prepared conceptually; actual Swagger URL varies by environment.
- Preferred approach:
  1. Add `NSwag.MSBuild` to the client project.
  2. Add `nswag.json` referencing the concrete Swagger JSON URL.
  3. Generate typed client into `src/MinuitMachine.Client/Services/Generated/`.
- When implementing, ensure generated clients accept an external `HttpClient` and do not hardcode base addresses; wire through `CredentialStore` for Basic Auth.

## Conventions
- Persist server IP/base URL only: `serverIp` and `baseUrl` are stored in `localStorage` via `LocalStorageService`. Do not persist username/password.
- Allow configuring API base URL at runtime (via UI) rather than compile-time constants.
- For API calls in components, prefer injecting a service (e.g., a typed client or `ApiClient`) instead of using `new HttpClient()` in components.
- Keep UI minimal and functional; avoid framework-specific CSS beyond `wwwroot/css/app.css` unless justified.

## Adding a New Endpoint Call (Example)
1. Define a method in a service, e.g., `ApiClient`:
   - GET JSON: `public Task<T?> GetJsonAsync<T>(string path)` using `HttpClient.GetFromJsonAsync<T>(...)` and Basic Auth header.
2. Inject and call from a page:
   - `@inject ApiClient Api`
   - `var data = await Api.GetJsonAsync<MyDto>("/api/items");`
3. Map DTOs adjacent to usage or under `Models/`.

## Troubleshooting
- CORS: Ensure the target API allows requests from the Blazor origin during development.
- HTTPS: Use valid certificates or relax dev verification server-side; the client assumes standard browser TLS.
- 401/403: Check Basic credentials and that the header is sent (use browser devtools → Network).

## Local Storage Keys
- `serverIp`: Raw IP string (e.g., `192.168.134.142`) set in `/apitest`.
- `baseUrl`: Derived or edited base URL (e.g., `http://192.168.134.142`). Used by `CredentialStore`.

## PR Expectations
- Keep changes scoped; avoid large refactors unrelated to API access.
- Update this file when introducing NSwag, new auth flows, or structural changes.
- Include a brief README snippet with any new build/run steps.
