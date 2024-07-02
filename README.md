# Solution
Alex's crack at the challenge

## Documentation
The application runs in Docker. Start it using the following command:
```
docker compose up -d
```
This will spin up a container on port:8000. The server is Dotnet 8 ASP.NET Core API, which also serves the client Blazor app. On the home page there is an upload button which can be used to send one or more files to the server app.

### API
#### POST file/upload 
```json
{
    "files:
    [
        { 
            "name": "some-name",
            "content": "content encoded as base64 string",
            "encodingName": "utf-8"
        }
    ]
}
```