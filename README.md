## MinuitMachine

Dieses Repository enthält eine Blazor WebAssembly Anwendung, die auf eine Swagger-basierte API mit Basic Authentication zugreifen kann.

### Voraussetzungen
- .NET SDK 8.x

### Projektstruktur
- `src/MinuitMachine.Client` – Blazor WebAssembly Client

### Schnellstart
```bash
dotnet build src/MinuitMachine.Client
dotnet run --project src/MinuitMachine.Client
```
Dann im Browser `http://localhost:5179` öffnen. Unter "API-Test" können Basis-URL, Benutzername und Passwort eingegeben und ein GET-Request getestet werden. Standardmäßig kann die Swagger-Definition unter `/swagger/v1/swagger.json` abgerufen werden.

### Hinweise zur API-Integration
- Authentifizierung: HTTP Basic (`Authorization: Basic <base64(user:pass)>`)
- API-Basis-URL und Zugangsdaten sind zur Laufzeit konfigurierbar (UI unter "API-Test").
- NSwag-Codegenerierung kann ergänzt werden, sobald eine konkrete Swagger-URL vorliegt.

Weitere Details für KI-Agenten: `.github/copilot-instructions.md`.