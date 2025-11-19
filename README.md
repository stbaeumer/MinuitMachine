# MinuitMachine

Eine Blazor WebAssembly Anwendung zum Testen von Swagger/OpenAPI Schnittstellen.

## Übersicht

MinuitMachine ist eine Blazor WebAssembly-Anwendung, die es ermöglicht, Swagger/OpenAPI-Schnittstellen einfach zu testen und zu verwenden. Die Anwendung bietet eine benutzerfreundliche Oberfläche zur Konfiguration von API-Endpunkten und zum Ausführen von HTTP-Anfragen.

## Features

- **API-Konfiguration**: Einfache Eingabe der Basis-URL und optionaler API-Schlüssel
- **Swagger-Dokument laden**: Möglichkeit, Swagger/OpenAPI-Spezifikationen direkt zu laden
- **Endpoint-Konfiguration**: 
  - Flexible Endpunkt-Pfad-Eingabe
  - Unterstützung für GET, POST, PUT und DELETE HTTP-Methoden
  - Dynamisches Hinzufügen und Entfernen von Parametern
- **API-Response-Anzeige**: Übersichtliche Darstellung der API-Antworten mit Status-Code

## Technologie-Stack

- **.NET 10.0**: Aktuelle .NET-Version
- **Blazor WebAssembly**: Client-seitige Web-Anwendung
- **C# 12**: Moderne C#-Funktionen

## Installation und Start

### Voraussetzungen

- .NET 10.0 SDK oder höher

### Anwendung starten

```bash
# Repository klonen
git clone https://github.com/stbaeumer/MinuitMachine.git
cd MinuitMachine

# Abhängigkeiten wiederherstellen
dotnet restore

# Anwendung bauen
dotnet build

# Anwendung starten
dotnet run
```

Die Anwendung ist dann unter `http://localhost:5000` oder einem anderen Port verfügbar.

## Verwendung

### 1. API-Konfiguration

Im linken Bereich "API Configuration" können Sie folgende Einstellungen vornehmen:

- **Base URL** (erforderlich): Die Basis-URL Ihrer API (z.B. `https://api.example.com`)
- **API Key** (optional): Bearer-Token für die Authentifizierung
- **Swagger/OpenAPI Document URL** (optional): URL zur Swagger-Spezifikation

### 2. Endpoint-Konfiguration

Im rechten Bereich "Endpoint Configuration" konfigurieren Sie den spezifischen Endpunkt:

- **Endpoint Path** (erforderlich): Der Pfad zum API-Endpunkt (z.B. `/api/users`)
- **HTTP Method**: Wählen Sie zwischen GET, POST, PUT oder DELETE
- **Parameters**: Fügen Sie dynamisch Parameter hinzu
  - Klicken Sie auf "Add Parameter" um einen neuen Parameter hinzuzufügen
  - Geben Sie Namen und Wert ein
  - Entfernen Sie Parameter mit dem "Remove"-Button

### 3. API-Aufruf ausführen

Klicken Sie auf "Execute API Call", um die Anfrage auszuführen. Die Antwort wird im Bereich "API Response" angezeigt.

## Beispiel-Verwendung

### Beispiel: JSONPlaceholder API testen

1. **Base URL**: `https://jsonplaceholder.typicode.com`
2. **Endpoint Path**: `/posts/1`
3. **HTTP Method**: `GET`
4. Klicken Sie auf "Execute API Call"

### Beispiel: Mit Parametern

1. **Base URL**: `https://api.example.com`
2. **Endpoint Path**: `/search`
3. **HTTP Method**: `GET`
4. Fügen Sie Parameter hinzu:
   - Name: `query`, Value: `test`
   - Name: `limit`, Value: `10`
5. Klicken Sie auf "Execute API Call"

## Projektstruktur

```
MinuitMachine/
├── Models/              # Datenmodelle
│   ├── ApiConfiguration.cs
│   ├── ApiResponse.cs
│   └── SwaggerEndpoint.cs
├── Services/            # Service-Klassen
│   └── SwaggerApiService.cs
├── Pages/               # Razor-Seiten
│   ├── SwaggerApi.razor
│   ├── Home.razor
│   ├── Counter.razor
│   └── Weather.razor
├── Layout/              # Layout-Komponenten
│   ├── MainLayout.razor
│   └── NavMenu.razor
└── Program.cs           # Einstiegspunkt
```

## CORS-Hinweise

Bitte beachten Sie, dass beim Testen von externen APIs möglicherweise CORS (Cross-Origin Resource Sharing) Probleme auftreten können. Dies ist eine Sicherheitsfunktion von Webbrowsern. Um externe APIs zu testen, müssen diese CORS-Unterstützung bieten oder Sie müssen einen Backend-Proxy verwenden.

## Lizenz

Siehe [LICENSE](LICENSE) Datei für Details.

## Autor

Stefan Bäumer