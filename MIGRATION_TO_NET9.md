# ONEHub Admin - .NET 9 Migration Plan

Hi ONEHub Developer! This plan follows our cursor rules and emphasizes Central Package Management.

## Overview

**Current:** .NET Framework 4.7.2 + ASP.NET MVC 5  
**Target:** .NET 9 + ASP.NET Core Minimal APIs  
**Estimated Time:** 10-14 weeks

---

## Phase 1: Setup & Central Package Management (Week 1)

### 1.1 Create New Solution Structure

```powershell
# Create new solution
cd D:\Nexdev
mkdir onehub-admin-net9
cd onehub-admin-net9

dotnet new sln -n ONEHub

# Create projects (following clean architecture)
dotnet new classlib -n ONEHub.Domain -o src\ONEHub.Domain -f net9.0
dotnet new classlib -n ONEHub.Application -o src\ONEHub.Application -f net9.0
dotnet new classlib -n ONEHub.Infrastructure -o src\ONEHub.Infrastructure -f net9.0
dotnet new webapi -n ONEHub.WebApi -o src\ONEHub.WebApi -f net9.0 --use-minimal-apis

# Add to solution
dotnet sln add src\ONEHub.Domain\ONEHub.Domain.csproj
dotnet sln add src\ONEHub.Application\ONEHub.Application.csproj
dotnet sln add src\ONEHub.Infrastructure\ONEHub.Infrastructure.csproj
dotnet sln add src\ONEHub.WebApi\ONEHub.WebApi.csproj

# Add project references
dotnet add src\ONEHub.Application reference src\ONEHub.Domain
dotnet add src\ONEHub.Infrastructure reference src\ONEHub.Domain
dotnet add src\ONEHub.WebApi reference src\ONEHub.Application
dotnet add src\ONEHub.WebApi reference src\ONEHub.Infrastructure
```

### 1.2 Setup Central Package Management

**Create `Directory.Build.props`:**
```xml
<Project>
  <PropertyGroup>
    <ManagePackageVersionsCentrally>true</ManagePackageVersionsCentrally>
    <TargetFramework>net9.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>disable</Nullable>
    <LangVersion>latest</LangVersion>
    <TreatWarningsAsErrors>false</TreatWarningsAsErrors>
  </PropertyGroup>
</Project>
```

**Create `Directory.Packages.props`:**
```xml
<Project>
  <ItemGroup>
    <!-- Core -->
    <PackageVersion Include="Newtonsoft.Json" Version="13.0.4" />
    <PackageVersion Include="Dapper" Version="2.1.66" />
    <PackageVersion Include="MySql.Data" Version="9.4.0" />
    
    <!-- Logging (Serilog) -->
    <PackageVersion Include="Serilog" Version="4.1.0" />
    <PackageVersion Include="Serilog.AspNetCore" Version="9.0.0" />
    <PackageVersion Include="Serilog.Enrichers.Environment" Version="3.0.1" />
    <PackageVersion Include="Serilog.Exceptions" Version="8.4.0" />
    <PackageVersion Include="Serilog.Settings.Configuration" Version="9.0.0" />
    <PackageVersion Include="Serilog.Sinks.Console" Version="6.0.0" />
    <PackageVersion Include="Serilog.Sinks.Debug" Version="3.0.0" />
    
    <!-- ASP.NET Core -->
    <PackageVersion Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="9.0.10" />
    <PackageVersion Include="Microsoft.AspNetCore.OpenApi" Version="9.0.10" />
    <PackageVersion Include="Swashbuckle.AspNetCore" Version="9.0.6" />
    
    <!-- CQRS & Patterns -->
    <PackageVersion Include="MediatR" Version="12.4.1" />
    <PackageVersion Include="Microsoft.Extensions.DependencyInjection.Abstractions" Version="9.0.10" />
    
    <!-- Caching & Redis -->
    <PackageVersion Include="StackExchange.Redis" Version="2.8.24" />
    <PackageVersion Include="MessagePack" Version="3.1.1" />
    
    <!-- Security -->
    <PackageVersion Include="BCrypt.Net-Next" Version="4.0.3" />
    <PackageVersion Include="System.IdentityModel.Tokens.Jwt" Version="8.14.0" />
    
    <!-- HTTP Clients -->
    <PackageVersion Include="Refit" Version="8.0.0" />
    
    <!-- Testing -->
    <PackageVersion Include="xunit" Version="2.9.3" />
    <PackageVersion Include="xunit.runner.visualstudio" Version="3.1.5" />
    <PackageVersion Include="FluentAssertions" Version="7.0.0" />
    <PackageVersion Include="NSubstitute" Version="5.3.0" />
    <PackageVersion Include="coverlet.collector" Version="6.0.4" />
    <PackageVersion Include="Microsoft.NET.Test.Sdk" Version="18.0.0" />
  </ItemGroup>
</Project>
```

**Benefits of Central Package Management:**
- Single source of truth for all package versions
- Easier to update packages across all projects
- Prevents version conflicts
- Better dependency management
- Follows .NET 9 best practices

### 1.3 Configure Projects

**ONEHub.Domain.csproj:**
```xml
<Project Sdk="Microsoft.NET.Sdk">
    <ItemGroup>
        <PackageReference Include="Newtonsoft.Json"/>
    </ItemGroup>
</Project>
```

**ONEHub.Application.csproj:**
```xml
<Project Sdk="Microsoft.NET.Sdk">
    <ItemGroup>
        <FrameworkReference Include="Microsoft.AspNetCore.App" />
        <PackageReference Include="MediatR" />
    </ItemGroup>
    <ItemGroup>
        <ProjectReference Include="..\ONEHub.Domain\ONEHub.Domain.csproj" />
    </ItemGroup>
</Project>
```

**ONEHub.Infrastructure.csproj:**
```xml
<Project Sdk="Microsoft.NET.Sdk">
    <ItemGroup>
        <PackageReference Include="Dapper" />
        <PackageReference Include="MySql.Data" />
        <PackageReference Include="StackExchange.Redis" />
        <PackageReference Include="MessagePack" />
    </ItemGroup>
    <ItemGroup>
        <ProjectReference Include="..\ONEHub.Domain\ONEHub.Domain.csproj" />
    </ItemGroup>
</Project>
```

**ONEHub.WebApi.csproj:**
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
    <ItemGroup>
        <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" />
        <PackageReference Include="Microsoft.AspNetCore.OpenApi" />
        <PackageReference Include="Serilog.AspNetCore" />
        <PackageReference Include="Serilog.Enrichers.Environment" />
        <PackageReference Include="Serilog.Exceptions" />
        <PackageReference Include="Serilog.Settings.Configuration" />
        <PackageReference Include="Serilog.Sinks.Console" />
        <PackageReference Include="Serilog.Sinks.Debug" />
        <PackageReference Include="Swashbuckle.AspNetCore" />
    </ItemGroup>
    <ItemGroup>
        <ProjectReference Include="..\ONEHub.Application\ONEHub.Application.csproj" />
        <ProjectReference Include="..\ONEHub.Infrastructure\ONEHub.Infrastructure.csproj" />
    </ItemGroup>
</Project>
```

**Note:** No version numbers in project files! All versions managed centrally in `Directory.Packages.props`.

### 1.4 Verify Build

```powershell
dotnet restore
dotnet build
# Should build successfully
```

**Commit:**
```
chore(config): Setup .NET 9 solution with Central Package Management
```

---

## Phase 2: Domain Layer - Following Cursor Rules (Week 2)

### 2.1 Create Domain Structure

**Following cursor rules:**
- Keep files under 250 lines
- No comments by default
- Clean, simple code

**Create:** `src/ONEHub.Domain/Entities/Affiliate.cs`
```csharp
namespace ONEHub.Domain.Entities;

public class Affiliate
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int Status { get; set; }
    public int AffType { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string? ContactApp { get; set; }
    public string? ContactId { get; set; }
    public bool IsFavorite { get; set; }
}
```

**Create:** `src/ONEHub.Domain/Entities/User.cs`
```csharp
namespace ONEHub.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
}
```

**Create:** `src/ONEHub.Domain/Constants/AffiliateStatus.cs`
```csharp
namespace ONEHub.Domain.Constants;

public static class AffiliateStatus
{
    public const int Pending = 0;
    public const int Approved = 1;
    public const int Rejected = 2;
    public const int Suspended = 3;
}
```

**Create:** `src/ONEHub.Domain/Constants/Roles.cs`
```csharp
namespace ONEHub.Domain.Constants;

public static class Roles
{
    public const string Admin = "Admin";
    public const string AffiliateManager = "AffiliateManager";
    public const string CustomerService = "CustomerService";
    public const string Finance = "Finance";
    public const string Marketing = "Marketing";
}
```

**Create:** `src/ONEHub.Domain/Results/ServiceResult.cs`
```csharp
namespace ONEHub.Domain.Results;

public class ServiceResult<T>
{
    public bool IsSuccess { get; init; }
    public T? Data { get; init; }
    public string? ErrorMessage { get; init; }
    public int? ErrorCode { get; init; }

    public static ServiceResult<T> Success(T data) => new()
    {
        IsSuccess = true,
        Data = data
    };

    public static ServiceResult<T> Failure(string errorMessage, int? errorCode = null) => new()
    {
        IsSuccess = false,
        ErrorMessage = errorMessage,
        ErrorCode = errorCode
    };
}
```

**Create:** `src/ONEHub.Domain/Repositories/IAffiliateRepository.cs`
```csharp
namespace ONEHub.Domain.Repositories;

public interface IAffiliateRepository
{
    Task<Affiliate?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Affiliate>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<int> CreateAsync(Affiliate affiliate, CancellationToken cancellationToken = default);
    Task<bool> UpdateAsync(Affiliate affiliate, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}
```

**Commit:**
```
feat(server): Add domain entities and repository interfaces
```

---

## Phase 3: Application Layer with MediatR (Week 3)

### 3.1 Setup MediatR Pattern

**Create:** `src/ONEHub.Application/DependencyInjection.cs`
```csharp
using Microsoft.Extensions.DependencyInjection;

namespace ONEHub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        return services;
    }
}
```

### 3.2 Create Commands (Following 250-line Rule)

**Create:** `src/ONEHub.Application/Commands/Affiliate/ApproveAffiliateCommand.cs`
```csharp
using MediatR;
using ONEHub.Domain.Results;

namespace ONEHub.Application.Commands.Affiliate;

public record ApproveAffiliateCommand(int AffiliateId, bool IsApproved)
    : IRequest<ServiceResult<bool>>;
```

**Create:** `src/ONEHub.Application/Handlers/Affiliate/ApproveAffiliateCommandHandler.cs`
```csharp
using MediatR;
using Microsoft.Extensions.Logging;
using ONEHub.Application.Commands.Affiliate;
using ONEHub.Domain.Constants;
using ONEHub.Domain.Repositories;
using ONEHub.Domain.Results;

namespace ONEHub.Application.Handlers.Affiliate;

public class ApproveAffiliateCommandHandler
    : IRequestHandler<ApproveAffiliateCommand, ServiceResult<bool>>
{
    private readonly IAffiliateRepository _repository;
    private readonly ILogger<ApproveAffiliateCommandHandler> _logger;

    public ApproveAffiliateCommandHandler(
        IAffiliateRepository repository,
        ILogger<ApproveAffiliateCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<ServiceResult<bool>> Handle(
        ApproveAffiliateCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var affiliate = await _repository.GetByIdAsync(
                request.AffiliateId,
                cancellationToken);

            if (affiliate == null)
            {
                return ServiceResult<bool>.Failure("Affiliate not found", 404);
            }

            affiliate.Status = request.IsApproved
                ? AffiliateStatus.Approved
                : AffiliateStatus.Rejected;
            affiliate.UpdatedDate = DateTime.UtcNow;

            var success = await _repository.UpdateAsync(affiliate, cancellationToken);

            if (success)
            {
                _logger.LogInformation(
                    "Affiliate {AffiliateId} {Action}",
                    request.AffiliateId,
                    request.IsApproved ? "approved" : "rejected");
            }

            return ServiceResult<bool>.Success(success);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,
                "Error approving affiliate {AffiliateId}",
                request.AffiliateId);
            return ServiceResult<bool>.Failure("Internal server error", 500);
        }
    }
}
```

**Commit:**
```
feat(server): Add MediatR commands and handlers for affiliate approval
```

---

## Phase 4: Infrastructure Layer (Week 4)

### 4.1 Database Connection Factory

**Create:** `src/ONEHub.Infrastructure/Database/Factories/IConnectionFactory.cs`
```csharp
using System.Data;

namespace ONEHub.Infrastructure.Database.Factories;

public interface IConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync(string connectionKey = "DefaultConnection");
}
```

**Create:** `src/ONEHub.Infrastructure/Database/Factories/MySqlConnectionFactory.cs`
```csharp
using System.Data;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace ONEHub.Infrastructure.Database.Factories;

public class MySqlConnectionFactory : IConnectionFactory
{
    private readonly IConfiguration _configuration;

    public MySqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<IDbConnection> CreateConnectionAsync(
        string connectionKey = "DefaultConnection")
    {
        var connectionString = _configuration.GetConnectionString(connectionKey);
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException(
                $"Connection string '{connectionKey}' not found");
        }

        var connection = new MySqlConnection(connectionString);
        await connection.OpenAsync();
        return connection;
    }
}
```

### 4.2 Repository Implementation (Following Best Practices)

**Create:** `src/ONEHub.Infrastructure/Repositories/BaseRepository.cs`
```csharp
using System.Data;
using ONEHub.Infrastructure.Database.Factories;

namespace ONEHub.Infrastructure.Repositories;

public abstract class BaseRepository
{
    private readonly IConnectionFactory _connectionFactory;

    protected BaseRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    protected async Task<IDbConnection> CreateConnectionAsync(
        string connectionKey = "DefaultConnection")
    {
        return await _connectionFactory.CreateConnectionAsync(connectionKey);
    }
}
```

**Create:** `src/ONEHub.Infrastructure/Repositories/AffiliateRepository.cs`
```csharp
using System.Data;
using Dapper;
using ONEHub.Domain.Entities;
using ONEHub.Domain.Repositories;
using ONEHub.Infrastructure.Database.Factories;

namespace ONEHub.Infrastructure.Repositories;

public class AffiliateRepository : BaseRepository, IAffiliateRepository
{
    public AffiliateRepository(IConnectionFactory connectionFactory)
        : base(connectionFactory)
    {
    }

    public async Task<Affiliate?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        await using var connection = await CreateConnectionAsync();

        var result = await connection.QueryFirstOrDefaultAsync<Affiliate>(
            new CommandDefinition(
                "sp_GetAffiliateById",
                new { AffiliateId = id },
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken
            )
        );

        return result;
    }

    public async Task<IEnumerable<Affiliate>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        await using var connection = await CreateConnectionAsync();

        var result = await connection.QueryAsync<Affiliate>(
            new CommandDefinition(
                "sp_GetAffiliates",
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken
            )
        );

        return result;
    }

    public async Task<int> CreateAsync(
        Affiliate affiliate,
        CancellationToken cancellationToken = default)
    {
        await using var connection = await CreateConnectionAsync();

        var id = await connection.ExecuteScalarAsync<int>(
            new CommandDefinition(
                "sp_InsertAffiliate",
                affiliate,
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken
            )
        );

        return id;
    }

    public async Task<bool> UpdateAsync(
        Affiliate affiliate,
        CancellationToken cancellationToken = default)
    {
        await using var connection = await CreateConnectionAsync();

        var rowsAffected = await connection.ExecuteAsync(
            new CommandDefinition(
                "sp_UpdateAffiliate",
                affiliate,
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken
            )
        );

        return rowsAffected > 0;
    }

    public async Task<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        await using var connection = await CreateConnectionAsync();

        var rowsAffected = await connection.ExecuteAsync(
            new CommandDefinition(
                "sp_DeleteAffiliate",
                new { AffiliateId = id },
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancellationToken
            )
        );

        return rowsAffected > 0;
    }
}
```

### 4.3 DependencyInjection

**Create:** `src/ONEHub.Infrastructure/DependencyInjection.cs`
```csharp
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ONEHub.Domain.Repositories;
using ONEHub.Infrastructure.Database.Factories;
using ONEHub.Infrastructure.Repositories;

namespace ONEHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IConnectionFactory, MySqlConnectionFactory>();
        services.AddScoped<IAffiliateRepository, AffiliateRepository>();

        return services;
    }
}
```

**Commit:**
```
feat(server): Add infrastructure layer with repositories
```

---

## Phase 5: WebApi Layer with Minimal APIs (Week 5)

### 5.1 Configuration Files

**Create:** `src/ONEHub.WebApi/appsettings.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=onehub;Uid=root;Pwd=password;"
  },
  "AppSettings": {
    "LicenseeId": 12,
    "Environment": "LOCAL"
  },
  "JwtSettings": {
    "SecretKey": "your-secret-key-minimum-32-characters-long-here",
    "Issuer": "ONEHub",
    "Audience": "ONEHub-Admin",
    "ExpirationMinutes": 60
  },
  "Serilog": {
    "Using": [ "Serilog.Sinks.Console" ],
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      { "Name": "Console" }
    ],
    "Enrich": [ "FromLogContext", "WithMachineName", "WithEnvironmentName" ]
  },
  "AllowedHosts": "*"
}
```

**Create:** `src/ONEHub.WebApi/appsettings.Development.json`
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=onehub_dev;Uid=root;Pwd=password;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "Microsoft.AspNetCore": "Information"
    }
  }
}
```

### 5.2 Minimal API Endpoints (Following 250-line Rule)

**Create:** `src/ONEHub.WebApi/Endpoints/IEndpoint.cs`
```csharp
namespace ONEHub.WebApi.Endpoints;

public interface IEndpoint
{
    static abstract void MapEndpoints(IEndpointRouteBuilder app);
}
```

**Create:** `src/ONEHub.WebApi/Extensions/EndpointExtensions.cs`
```csharp
using System.Reflection;

namespace ONEHub.WebApi.Extensions;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapEndpoints(
        this IEndpointRouteBuilder app,
        Assembly? assembly = null)
    {
        assembly ??= Assembly.GetExecutingAssembly();

        var endpointTypes = assembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract &&
                   t.GetInterfaces().Any(i => i.Name == "IEndpoint"));

        foreach (var type in endpointTypes)
        {
            var method = type.GetMethod("MapEndpoints",
                BindingFlags.Public | BindingFlags.Static);

            method?.Invoke(null, new object[] { app });
        }

        return app;
    }
}
```

**Create:** `src/ONEHub.WebApi/DTOs/Response/ApiResponse.cs`
```csharp
namespace ONEHub.WebApi.DTOs.Response;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string? Message { get; set; }
}
```

**Create:** `src/ONEHub.WebApi/DTOs/Request/ApproveAffiliateRequest.cs`
```csharp
namespace ONEHub.WebApi.DTOs.Request;

public class ApproveAffiliateRequest
{
    public int AffiliateId { get; set; }
    public bool IsApproved { get; set; }
}
```

**Create:** `src/ONEHub.WebApi/Endpoints/AffiliateEndpoint.cs` (Under 250 lines)
```csharp
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ONEHub.Application.Commands.Affiliate;
using ONEHub.WebApi.DTOs.Request;
using ONEHub.WebApi.DTOs.Response;

namespace ONEHub.WebApi.Endpoints;

public class AffiliateEndpoint : IEndpoint
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/affiliates")
            .WithTags("Affiliates");

        group.MapPost("/approve", ApproveAffiliate)
            .WithName("ApproveAffiliate")
            .Produces<ApiResponse<bool>>(200)
            .Produces<ProblemDetails>(400)
            .Produces<ProblemDetails>(404);
    }

    private static async Task<IResult> ApproveAffiliate(
        [FromBody] ApproveAffiliateRequest request,
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
    {
        var command = new ApproveAffiliateCommand(
            request.AffiliateId,
            request.IsApproved);

        var result = await mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return Results.BadRequest(new ProblemDetails
            {
                Title = "Approval Failed",
                Detail = result.ErrorMessage,
                Status = result.ErrorCode ?? 400
            });
        }

        return Results.Ok(new ApiResponse<bool>
        {
            Success = true,
            Data = result.Data,
            Message = $"Affiliate {(request.IsApproved ? "approved" : "rejected")} successfully"
        });
    }
}
```

### 5.3 Program.cs (Simple & Clean)

**Update:** `src/ONEHub.WebApi/Program.cs`
```csharp
using ONEHub.Application;
using ONEHub.Infrastructure;
using ONEHub.WebApi.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseCors();

app.MapGet("/", () => "ONEHub Admin API is running...")
    .ExcludeFromDescription();

app.MapEndpoints();

app.Run();
```

**Commit:**
```
feat(api): Add minimal API endpoints with clean architecture
```

---

## Migration Strategy for Existing Components

### Following Cursor Rules

**Rule: Keep files under 250 lines**
- Split large controllers into multiple endpoint files
- One endpoint file per domain area
- Example: `AffiliateEndpoint.cs` (approval, password, favorite)

**Rule: Avoid duplication**
- Create base classes/interfaces where needed
- Reuse existing patterns
- Don't recreate what already exists

**Rule: No new patterns without exhausting existing**
- Keep using stored procedures (don't switch to EF Core)
- Keep existing SpConfig approach
- Maintain current database structure

**Rule: No comments by default**
- Write self-documenting code
- Use clear naming conventions
- Only add comments for complex business logic

**Rule: Separate mock data**
- Test data in separate files
- No hardcoded test data in production code

### Component Migration Pattern

**For each controller (45+ controllers):**

1. **Analyze current controller** (e.g., AffiliateController)
2. **Create command/query** for each action
3. **Create handler** (under 250 lines)
4. **Create endpoint file** (under 250 lines)
5. **Test**
6. **Commit with proper format**

**Example commit sequence:**
```
feat(server): Add affiliate approval command and handler
feat(api): Add affiliate approval endpoint
test(server): Add tests for affiliate approval handler
```

---

## Frontend Migration Strategy

### Current Frontend Architecture

Your project has a **React SPA embedded in ASP.NET MVC**:

```
Areas/Portal/www/
├── source/js/           ← 1175+ React files (React 16, Redux, Webpack 4)
│   ├── pages/           ← 659 .js + 325 .jsx files
│   ├── components/      ← 16 .jsx components
│   ├── actions/         ← Redux actions
│   └── reducers/        ← Redux reducers
└── dist/                ← Webpack bundled output
```

### Recommended Approach: Keep Embedded (Phase 1)

**For initial .NET 9 migration, keep React embedded:**

1. Copy `Areas/Portal/www/` to `ONEHub.WebApi/wwwroot/`
2. Configure static file serving
3. Update React API calls from MVC endpoints to Web API endpoints
4. Test all features

**Migration later (Phase 2 - Optional):**
- Move to separate frontend repo (like onehub-adminsite)
- Upgrade to React 18 + Vite
- Add TypeScript
- Independent deployment

### Frontend Update Tasks

```bash
# Copy frontend to WebApi
cp -r src/Fanex.ONEHub.Host/Areas/Portal/www/dist/* src/ONEHub.WebApi/wwwroot/

# Update API calls in React code
# OLD: axios.post('/Affiliate/ApproveAffiliate', data)
# NEW: axios.post('/api/affiliates/approve', data)

# Build React app
cd src/ONEHub.WebApi/ClientApp
npm install
npm run build
```

---

## Critical Dependencies Check

### ⚠️ MUST VERIFY BEFORE PROCEEDING

**Fanex Internal Packages:**
1. Fanex.Caching.2.2.8 → .NET 9 version?
2. Fanex.Caching.Redis.2.2.8 → .NET 9 version?
3. Fanex.Data.ProxyClient.1.0.3.6 → .NET 9 version?
4. Fanex.Logging.2.0.1.1 → .NET 9 version?
5. Fanex.Logging.Sentry.RavenClient.1.0.1.2 → .NET 9 version?
6. Fanex.MailDeliver.MailClient.2.0.11 → .NET 9 version?

**ONEbook.UM Authentication:**
1. ONEbook.UM.3.1.6 → .NET 9 version?
2. Alternative: Migrate to standard JWT?

**Action:** Contact teams IMMEDIATELY to get .NET 9 versions or plan alternatives.

---

## Testing Strategy

### Unit Tests (Following Cursor Rules)

**Setup test project:**
```powershell
dotnet new xunit -n ONEHub.Application.UnitTests -o tests\ONEHub.Application.UnitTests -f net9.0
dotnet sln add tests\ONEHub.Application.UnitTests
dotnet add tests\ONEHub.Application.UnitTests reference src\ONEHub.Application
```

**Update test packages in Directory.Packages.props** (already included)

**Example test (under 250 lines, no unnecessary comments):**
```csharp
using FluentAssertions;
using NSubstitute;
using ONEHub.Application.Commands.Affiliate;
using ONEHub.Application.Handlers.Affiliate;
using ONEHub.Domain.Constants;
using ONEHub.Domain.Entities;
using ONEHub.Domain.Repositories;
using Xunit;

namespace ONEHub.Application.UnitTests.Handlers.Affiliate;

public class ApproveAffiliateCommandHandlerTests
{
    private readonly IAffiliateRepository _repository;
    private readonly ApproveAffiliateCommandHandler _handler;

    public ApproveAffiliateCommandHandlerTests()
    {
        _repository = Substitute.For<IAffiliateRepository>();
        var logger = Substitute.For<ILogger<ApproveAffiliateCommandHandler>>();
        _handler = new ApproveAffiliateCommandHandler(_repository, logger);
    }

    [Fact]
    public async Task Handle_ValidAffiliate_ReturnsSuccess()
    {
        var affiliate = new Domain.Entities.Affiliate { Id = 1, Username = "test" };
        _repository.GetByIdAsync(1, Arg.Any<CancellationToken>())
            .Returns(affiliate);

        var command = new ApproveAffiliateCommand(1, true);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await _repository.Received(1).UpdateAsync(
            Arg.Is<Domain.Entities.Affiliate>(a => a.Status == AffiliateStatus.Approved),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_AffiliateNotFound_ReturnsFailure()
    {
        _repository.GetByIdAsync(999, Arg.Any<CancellationToken>())
            .Returns((Domain.Entities.Affiliate?)null);

        var command = new ApproveAffiliateCommand(999, true);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.ErrorMessage.Should().Be("Affiliate not found");
        result.ErrorCode.Should().Be(404);
    }
}
```

**Commit:**
```
test(server): Add unit tests for affiliate approval handler
```

---

## Git Workflow Following Commit Rules

### Branch Strategy
```
main (production)
├── develop (development)
    ├── feature/net9-domain
    ├── feature/net9-application
    ├── feature/net9-infrastructure
    ├── feature/net9-webapi
    └── feature/net9-testing
```

### Commit Message Examples (Following Cursor Rules)

**Good commits:**
```bash
# Feature
git commit -m "feat(server): Add domain entities for affiliates"
git commit -m "feat(api): Add affiliate endpoints with MediatR"
git commit -m "feat(config): Setup Central Package Management"

# Fix
git commit -m "fix(server): Resolve null reference in affiliate repository"
git commit -m "fix(api): Correct response format in error handling"

# Refactor
git commit -m "refactor(server): Simplify affiliate approval logic"

# Test
git commit -m "test(server): Add tests for affiliate handlers"

# Build/Config
git commit -m "build(config): Update package versions"
git commit -m "chore(deps): Update Dapper to version 2.1.66"

# Docs
git commit -m "docs: Update migration plan for .NET 9"
```

**Breaking changes:**
```bash
git commit -m "feat(api)!: Change affiliate endpoint response structure"
```

---

## Next Steps & Timeline

### Week 1: Setup
- [ ] Create new solution structure
- [ ] Setup Central Package Management
- [ ] Verify all packages compatible
- [ ] Initial build successful

### Week 2: Domain
- [ ] Migrate all entities (100+)
- [ ] Create repository interfaces
- [ ] Setup constants and enums
- [ ] Add result patterns

### Week 3-4: Application
- [ ] Setup MediatR
- [ ] Migrate commands (70+)
- [ ] Create handlers
- [ ] Add validators

### Week 5-6: Infrastructure
- [ ] Implement repositories
- [ ] Setup database connections
- [ ] Migrate caching layer
- [ ] Configure logging

### Week 7-8: WebApi
- [ ] Create minimal API endpoints
- [ ] Migrate controllers (45+)
- [ ] Setup authentication
- [ ] Configure Swagger

### Week 9-10: Testing
- [ ] Migrate unit tests (96+)
- [ ] Add integration tests
- [ ] Performance testing
- [ ] Security audit

### Week 11-12: Deployment
- [ ] Docker setup
- [ ] CI/CD pipeline
- [ ] Deploy to UAT
- [ ] Production deployment

---

## Memory Update

After completing this migration plan:
- Project upgraded from .NET Framework 4.7.2 to .NET 9
- Implemented Central Package Management
- Followed ONEHub cursor rules (250-line limit, no comments, clean code)
- Used Clean Architecture pattern
- Applied MediatR for CQRS
- Minimal APIs for endpoints
- Proper conventional commits

---

## Questions to Answer Before Starting

1. **Are Fanex packages compatible with .NET 9?**
2. **Is ONEbook.UM available for .NET 9?**
3. **Should we keep React embedded or separate?**
4. **Timeline approval from stakeholders?**
5. **Team allocation confirmed?**

**Document Version:** 1.0  
**Created:** 2025-01-20  
**Status:** Ready for Review

