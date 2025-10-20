# ONEHub Admin - .NET 9 Migration

Hi ONEHub Developer! Complete migration package for upgrading to .NET 9.

---

## 🚀 READ THIS FIRST!

### **[START_HERE.md](START_HERE.md)** ⭐ ONE-PAGE GUIDE (10 min read)
Everything you need to know:
- Current architecture analysis
- Simple 2-phase migration plan
- Critical Week 0 verification tasks
- Decision tree
- Quick checklist

**→ Start with this document!**

---

## 📚 Other Documents

### 1. **[MIGRATION_TO_NET9.md](MIGRATION_TO_NET9.md)** - Complete Technical Guide
   - Phase 1: Backend migration (10 weeks)
   - Phase 2: Frontend modernization (6 weeks)
   - Week-by-week detailed tasks
   - Code examples & commands
   - Configuration mappings
   - Architecture patterns

### 2. **[CHECKLIST.md](CHECKLIST.md)** - Progress Tracker
   - Printable checklist
   - Track Week 0 verification
   - Track Phase 1 tasks
   - Track Phase 2 tasks (optional)

---

## 🎯 Key Highlights

### Central Package Management
**All package versions managed in one place:**
- `Directory.Build.props` - Common properties
- `Directory.Packages.props` - All package versions
- Project files **never** contain version numbers
- Single source of truth for dependencies

**Example:**
```xml
<!-- Directory.Packages.props -->
<PackageVersion Include="Dapper" Version="2.1.66" />

<!-- Project file - NO version! -->
<PackageReference Include="Dapper" />
```

### Cursor Rules Compliance

**✅ Keep files under 250 lines**
- Split large controllers into multiple endpoints
- One endpoint per domain area
- Example: AffiliateEndpoint.cs handles approval, password, favorite

**✅ No comments by default**
- Self-documenting code
- Clear naming conventions
- Comments only for complex business logic

**✅ Simple solutions**
- Don't introduce new patterns unnecessarily
- Keep using stored procedures
- Maintain existing SpConfig approach

**✅ Conventional commits**
```bash
feat(server): Add domain entities for affiliates
fix(api): Resolve null reference in repository
chore(deps): Update Dapper to version 2.1.66
```

**✅ Avoid duplication**
- Base classes where needed
- Reuse existing patterns
- Check for existing similar code

---

## 🏗️ Architecture Transformation

### From (Current)
```
.NET Framework 4.7.2
├── Fanex.ONEHub.Core (Business + Controllers)
├── Fanex.ONEHub.Host (Web + React)
└── Fanex.ONEHub.Infrastructure
```

### To (Target)
```
.NET 9
├── ONEHub.Domain (Entities, Interfaces)
├── ONEHub.Application (Commands, Queries, Handlers)
├── ONEHub.Infrastructure (Repositories, External services)
└── ONEHub.WebApi (Minimal APIs, Endpoints)
```

---

## ⚡ Quick Start

### Prerequisites
```powershell
dotnet --version  # Should show 9.0.x
```

### Create Solution (5 minutes)
```powershell
cd D:\Nexdev
mkdir onehub-admin-net9
cd onehub-admin-net9

dotnet new sln -n ONEHub
dotnet new classlib -n ONEHub.Domain -o src\ONEHub.Domain -f net9.0
dotnet new classlib -n ONEHub.Application -o src\ONEHub.Application -f net9.0
dotnet new classlib -n ONEHub.Infrastructure -o src\ONEHub.Infrastructure -f net9.0
dotnet new webapi -n ONEHub.WebApi -o src\ONEHub.WebApi -f net9.0 --use-minimal-apis

dotnet sln add src\ONEHub.Domain\ONEHub.Domain.csproj
dotnet sln add src\ONEHub.Application\ONEHub.Application.csproj
dotnet sln add src\ONEHub.Infrastructure\ONEHub.Infrastructure.csproj
dotnet sln add src\ONEHub.WebApi\ONEHub.WebApi.csproj
```

**Continue with [MIGRATION_TO_NET9.md](MIGRATION_TO_NET9.md) for complete setup.**

---

## 📊 Migration Scope

### Components to Migrate

| Component | Count | Priority |
|-----------|-------|----------|
| Controllers | 45+ | HIGH |
| Services | 60+ | HIGH |
| Commands | 70+ | MEDIUM |
| DTOs/Entities | 100+ | HIGH |
| Unit Tests | 96+ | MEDIUM |

### Estimated Timeline: 10-14 weeks

---

## ⚠️ Critical Action Items

### MUST VERIFY BEFORE STARTING

1. **Fanex Internal Packages**
   - Contact Fanex team for .NET 9 versions
   - Packages: Caching, Logging, MailDeliver, Data.ProxyClient
   - Get source code if versions not available

2. **ONEbook.UM Authentication**
   - Check .NET 9 compatibility
   - Alternative: Migrate to standard JWT
   - Timeline for migration?

3. **Stakeholder Approval**
   - Present migration plan
   - Get resource allocation
   - Confirm timeline

---

## 🔄 Migration Strategy

### Phase-by-Phase Approach

**Week 1: Setup**
- Create solution structure
- Setup Central Package Management
- Verify all dependencies

**Week 2-3: Domain & Application**
- Migrate entities
- Create repository interfaces
- Setup MediatR commands/handlers

**Week 4-5: Infrastructure**
- Implement repositories
- Setup database connections
- Configure caching & logging

**Week 6-8: WebApi**
- Create minimal API endpoints
- Migrate controllers one by one
- Setup authentication

**Week 9-10: Testing**
- Migrate unit tests
- Add integration tests
- Performance testing

**Week 11-12: Deployment**
- Docker containerization
- CI/CD pipeline
- Production deployment

---

## 📝 Commit Message Guidelines

Following cursor rules for conventional commits:

### Format
```
<type>(scope): Description
```

### Good Examples
```bash
feat(server): Add domain entities for affiliates
feat(api): Add affiliate approval endpoint
fix(server): Resolve null reference in repository
refactor(server): Simplify affiliate approval logic
test(server): Add tests for affiliate handlers
chore(deps): Update Dapper to version 2.1.66
docs: Update migration documentation
```

### Bad Examples
```bash
feat[server]: add entities  # Wrong brackets, wrong case
Fix bug.  # No type, has period, too short
update code  # Too vague, no type
```

### Breaking Changes
```bash
feat(api)!: Redesign affiliate endpoint structure
```

---

## 🛠️ Tech Stack

### Current (.NET Framework 4.7.2)
- ASP.NET MVC 5
- Autofac for DI
- packages.config
- Web.config
- Old csproj format

### Target (.NET 9)
- ASP.NET Core Minimal APIs
- Built-in DI
- Central Package Management
- appsettings.json
- SDK-style csproj
- MediatR for CQRS
- Serilog for logging

---

## 📦 Package Management Strategy

### Central Package Management Benefits

1. **Single Source of Truth**
   - All versions in Directory.Packages.props
   - No version conflicts between projects

2. **Easy Updates**
   - Update version once, affects all projects
   - Clear visibility of all dependencies

3. **Better Control**
   - Lock specific versions
   - Consistent across solution

4. **Follows .NET 9 Best Practices**
   - Microsoft recommended approach
   - Industry standard

### Key Packages

```xml
<!-- Core -->
<PackageVersion Include="Newtonsoft.Json" Version="13.0.4" />
<PackageVersion Include="Dapper" Version="2.1.66" />
<PackageVersion Include="MySql.Data" Version="9.4.0" />

<!-- Logging -->
<PackageVersion Include="Serilog" Version="4.1.0" />
<PackageVersion Include="Serilog.AspNetCore" Version="9.0.0" />

<!-- CQRS -->
<PackageVersion Include="MediatR" Version="12.4.1" />

<!-- Caching -->
<PackageVersion Include="StackExchange.Redis" Version="2.8.24" />
<PackageVersion Include="MessagePack" Version="3.1.1" />
```

---

## 🧪 Testing Strategy

### Unit Tests
```csharp
// Following cursor rules: no comments, clean code
public class ApproveAffiliateCommandHandlerTests
{
    [Fact]
    public async Task Handle_ValidAffiliate_ReturnsSuccess()
    {
        var affiliate = new Affiliate { Id = 1 };
        _repository.GetByIdAsync(1, default).Returns(affiliate);

        var result = await _handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
    }
}
```

### Test Projects
```powershell
dotnet new xunit -n ONEHub.Application.UnitTests -f net9.0
dotnet add reference src\ONEHub.Application
```

---

## 📂 Project Structure

```
onehub-admin-net9/
├── Directory.Build.props          # Common properties
├── Directory.Packages.props       # All package versions
├── ONEHub.sln
├── src/
│   ├── ONEHub.Domain/             # Entities, Interfaces, Constants
│   ├── ONEHub.Application/        # Commands, Queries, Handlers
│   ├── ONEHub.Infrastructure/     # Repositories, External services
│   └── ONEHub.WebApi/            # Endpoints, DTOs
└── tests/
    ├── ONEHub.Domain.UnitTests/
    ├── ONEHub.Application.UnitTests/
    └── ONEHub.Infrastructure.UnitTests/
```

---

## 🔍 Code Quality Rules

### From Cursor Rules

1. **File Size:** Under 250 lines
2. **Comments:** No comments by default (self-documenting code)
3. **Simplicity:** Prefer simple solutions
4. **No Duplication:** Check for existing patterns
5. **Conventional Commits:** Follow strict format
6. **Clean Code:** Eliminate redundant code
7. **Separation:** Mock data separate from source

---

## 🚀 Next Steps

### Immediate (This Week)
1. ✅ Review migration documents
2. ⬜ Contact Fanex team about packages
3. ⬜ Contact ONEbook team about authentication
4. ⬜ Get stakeholder approval
5. ⬜ Allocate team resources

### Week 1
1. ⬜ Follow [MIGRATION_TO_NET9.md](MIGRATION_TO_NET9.md)
2. ⬜ Create proof of concept
3. ⬜ Validate approach
4. ⬜ Begin domain layer migration

---

## 📞 Support

### Questions About Migration?
- Check [START_HERE.md](START_HERE.md) for quick overview
- Check [MIGRATION_TO_NET9.md](MIGRATION_TO_NET9.md) for detailed plan
- Reference project: `D:\Nexdev\onehub-adminapi`

### Questions About Dependencies?
- Contact Fanex team for internal packages
- Contact ONEbook team for UM authentication
- Review Directory.Packages.props for all versions

---

## ✅ Success Criteria

- [ ] All components migrated
- [ ] Test coverage > 70%
- [ ] Performance targets met
- [ ] Zero data loss
- [ ] Team trained
- [ ] Documentation complete
- [ ] Production deployment successful

---

## 🎯 Migration Summary

### Current Project Analysis
- **Framework:** .NET Framework 4.7.2 + ASP.NET MVC 5
- **Code:** 636 C# files (45+ controllers, 60+ services, 70+ commands)
- **Frontend:** React 16 + Redux + Webpack 4 (1,175 files, embedded in MVC)
- **Database:** Dapper with 100+ stored procedures
- **Auth:** ONEbook.UM 3.1.6 + Forms Authentication
- **Tests:** 96 unit tests (xUnit + Moq)

### Migration Approach: Two-Phase
**Phase 1 (10 weeks, REQUIRED):** Backend to .NET 9 + Clean Architecture + CQRS
**Phase 2 (6 weeks, OPTIONAL):** Frontend to React 18 + TypeScript + Vite

### Critical Dependencies (BLOCKERS)
⚠️ **MUST VERIFY FIRST:**
- Fanex.Caching.Redis 2.2.8 - .NET 9 compatibility UNKNOWN
- Fanex.Logging.Sentry.RavenClient 1.0.1.2 - .NET 9 compatibility UNKNOWN
- ONEbook.UM 3.1.6 - ASP.NET Core support UNKNOWN

### 🚨 CRITICAL FIRST STEP
**Before any development work:**
1. Read [START_HERE.md](START_HERE.md) (10 minutes)
2. Complete Week 0 verification tasks
3. Verify Fanex packages + ONEbook.UM compatibility
4. Create Proof of Concept
5. Hold Go/No-Go meeting

### Timeline
- **Week 0:** Verification & PoC (CRITICAL)
- **Weeks 1-10:** Phase 1 (Backend migration)
- **Weeks 11-16:** Phase 2 (Frontend modernization, optional)

### Resources Needed
- **Team:** 3-4 backend developers
- **Duration:** 10 weeks (Phase 1) + 6 weeks (Phase 2 if needed)
- **Skills:** .NET 9, ASP.NET Core, Clean Architecture, CQRS, MediatR

---

**Version:** 2.0  
**Last Updated:** October 20, 2025  
**Status:** Comprehensive Review Complete ✅

**🚀 Ready to start? Read [START_HERE.md](START_HERE.md) first!**
