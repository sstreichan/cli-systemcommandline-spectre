# System.CommandLine + Spectre.Console Demo

🚀 **AOT-compatible CLI** using **System.CommandLine** for argument parsing and **Spectre.Console** for rich terminal UI.

## Features

✅ **Native AOT Compatible** - Compiles to native executable without .NET runtime  
✅ **System.CommandLine 2.0** - Official Microsoft CLI framework  
✅ **Spectre.Console** - Beautiful terminal UI with tables, panels, progress bars  
✅ **Zero Reflection** - Fully trimmable and AOT-safe  
✅ **Multiple Commands** - Demonstrates subcommands, options, and handlers  

## Commands

### 1. `greet` - Greet someone
```bash
dotnet run -- greet --name John --count 3
```

### 2. `info` - Display system information
```bash
dotnet run -- info
```

### 3. `list` - Display items
```bash
dotnet run -- list --items Apple Banana Cherry
```

### 4. `progress` - Show progress bar
```bash
dotnet run -- progress --duration 5
```

## Build & Run

### Development
```bash
dotnet restore
dotnet build
dotnet run -- --help
```

### Native AOT Publishing

**Windows (x64):**
```bash
dotnet publish -c Release -r win-x64 /p:PublishAot=true
.\bin\Release\net8.0\win-x64\publish\SystemCommandLineSpectre.exe greet --name World
```

**Linux (x64):**
```bash
dotnet publish -c Release -r linux-x64 /p:PublishAot=true
./bin/Release/net8.0/linux-x64/publish/SystemCommandLineSpectre greet --name World
```

**macOS (ARM64):**
```bash
dotnet publish -c Release -r osx-arm64 /p:PublishAot=true
./bin/Release/net8.0/osx-arm64/publish/SystemCommandLineSpectre greet --name World
```

## Binary Size

Typical AOT binary size: **~8-12 MB** (trimmed, compressed)

## Performance

- **Cold start:** ~50-100ms
- **Memory usage:** ~15-25 MB
- **No JIT compilation** required

## Technology Stack

| Package | Version | Purpose |
|---------|---------|--------|
| System.CommandLine | 2.0.2 | CLI argument parsing |
| Spectre.Console | 0.53.1 | Terminal UI rendering |
| .NET | 8.0+ | Runtime |

## Project Structure

```
.
├── SystemCommandLineSpectre.csproj  # Project file with AOT settings
├── Program.cs                       # Main application code
└── README.md                        # This file
```

## Requirements

- **.NET 8.0 SDK** or later
- **C# 12** or later

## License

MIT License - feel free to use and modify!

## Links

- [System.CommandLine Docs](https://learn.microsoft.com/en-us/dotnet/standard/commandline/)
- [Spectre.Console Docs](https://spectreconsole.net/)
- [Native AOT Deployment](https://learn.microsoft.com/en-us/dotnet/core/deploying/native-aot/)