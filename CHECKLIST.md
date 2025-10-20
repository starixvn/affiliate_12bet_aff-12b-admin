# ONEHub Admin - Migration Checklist

Hi ONEHub Developer! Use this checklist to track your migration progress.

---

## 📋 Week 0: Pre-Migration Verification (CRITICAL)

### Day 1-3: Dependency Verification

#### Fanex Packages
- [ ] Contact Fanex development team
- [ ] Request .NET 9 compatibility status for:
  - [ ] Fanex.Caching.Redis 2.2.8
  - [ ] Fanex.Logging.Sentry.RavenClient 1.0.1.2
  - [ ] Fanex.MailDeliver.MailClient 2.0.11
  - [ ] Fanex.Data.ProxyClient 1.0.3.6
- [ ] Document findings
- [ ] Identify alternatives if needed

#### ONEbook.UM Authentication
- [ ] Contact ONEbook.UM team
- [ ] Verify .NET 9 / ASP.NET Core support
- [ ] Check JWT token compatibility
- [ ] Get migration guide from version 3.1.6
- [ ] Document authentication migration path

### Day 3-8: Proof of Concept

#### Setup
- [ ] Install .NET 9 SDK
- [ ] Create test solution structure
```bash
dotnet new sln -n ONEHub.PoC
dotnet new webapi -n ONEHub.PoC.WebApi -f net9.0
dotnet sln add ONEHub.PoC.WebApi
```

#### Test Dependencies
- [ ] Add Dapper 2.1.35 (should work ✅)
- [ ] Add Fanex packages (if verified compatible)
- [ ] Test database connection
- [ ] Verify stored procedure calls work

#### Migrate One Service
- [ ] Pick simple service (e.g., AuthService)
- [ ] Create MediatR command/handler
- [ ] Create Minimal API endpoint
- [ ] Test with Swagger
- [ ] Time effort (should be ≤ 2 days)

### Day 9-10: Go/No-Go Meeting

#### Preparation
- [ ] Compile findings document
- [ ] Prepare PoC demo
- [ ] List identified risks
- [ ] Estimate timeline adjustments
- [ ] Calculate resource needs

#### Meeting Agenda
- [ ] Present Fanex package status
- [ ] Present ONEbook.UM status
- [ ] Demo working PoC
- [ ] Review risk assessment
- [ ] Confirm timeline (10-14 weeks)
- [ ] Confirm team allocation (3-4 devs)
- [ ] Make GO/NO-GO decision

---

## ✅ GO Decision Made? Proceed Below

---

## 📦 Phase 1A: Foundation (Weeks 1-2)

### Week 1: Solution Structure

#### Create Projects
- [ ] Create solution folder
```bash
cd D:\Nexdev
mkdir onehub-admin-net9
cd onehub-admin-net9
```

- [ ] Create solution
```bash
dotnet new sln -n ONEHub
```

- [ ] Create Domain project
```bash
dotnet new classlib -n ONEHub.Domain -o src/ONEHub.Domain -f net9.0
```

- [ ] Create Application project
```bash
dotnet new classlib -n ONEHub.Application -o src/ONEHub.Application -f net9.0
```

- [ ] Create Infrastructure project
```bash
dotnet new classlib -n ONEHub.Infrastructure -o src/ONEHub.Infrastructure -f net9.0
```

- [ ] Create WebApi project
```bash
dotnet new webapi -n ONEHub.WebApi -o src/ONEHub.WebApi -f net9.0 --use-minimal-apis
```

- [ ] Add projects to solution
```bash
dotnet sln add src/ONEHub.Domain/ONEHub.Domain.csproj
dotnet sln add src/ONEHub.Application/ONEHub.Application.csproj
dotnet sln add src/ONEHub.Infrastructure/ONEHub.Infrastructure.csproj
dotnet sln add src/ONEHub.WebApi/ONEHub.WebApi.csproj
```

#### Central Package Management
- [ ] Create Directory.Build.props
- [ ] Create Directory.Packages.props
- [ ] Add all package versions
- [ ] Enable ManagePackageVersionsCentrally
- [ ] Test build

#### Git Setup
- [ ] Initialize Git repository
- [ ] Create .gitignore
- [ ] Create initial commit
```bash
git init
git add .
git commit -m "feat(setup): Initialize .NET 9 solution structure"
```

### Week 2: Infrastructure Setup

#### Development Environment
- [ ] Set up connection strings
- [ ] Configure Redis
- [ ] Configure Vault
- [ ] Set up appsettings.Development.json
- [ ] Set up appsettings.Staging.json
- [ ] Set up appsettings.Production.json

#### CI/CD Pipeline
- [ ] Create build pipeline
- [ ] Create deployment pipeline
- [ ] Set up automated tests
- [ ] Configure SonarQube (if used)

---

## 🏗️ Phase 1B: Domain Layer (Weeks 2-3)

### Domain Entities
- [ ] Create base entity classes
- [ ] Migrate DTOs → Domain entities (100+)
  - [ ] Affiliate entities
  - [ ] Administrator entities
  - [ ] Domain entities
  - [ ] Customer entities
  - [ ] Campaign entities
  - [ ] Operation entities
  - [ ] Report entities
- [ ] Create domain enums
- [ ] Create domain constants
- [ ] Create domain interfaces

### Validation
- [ ] Set up FluentValidation
- [ ] Create validation rules

---

## 🎯 Phase 1C: Application Layer (Weeks 3-5)

### MediatR Setup
- [ ] Install MediatR
- [ ] Configure in Program.cs
- [ ] Create base command/query classes

### Commands (70+)
- [ ] Migrate Administrator commands
- [ ] Migrate Affiliate commands
- [ ] Migrate Auth commands
- [ ] Migrate Campaign commands
- [ ] Migrate Customer commands
- [ ] Migrate Domain commands
- [ ] Migrate LandingPage commands
- [ ] Migrate MediaGallery commands
- [ ] Migrate Message commands
- [ ] Migrate Operation commands
- [ ] Migrate UserPanel commands

### Queries
- [ ] Create query handlers for all Criterias
- [ ] Implement pagination logic
- [ ] Implement sorting logic

### AutoMapper
- [ ] Install AutoMapper
- [ ] Create mapping profiles
- [ ] Test mappings

---

## 🔧 Phase 1D: Infrastructure Layer (Weeks 5-6)

### Database
- [ ] Configure connection strings
- [ ] Implement repository pattern
- [ ] Migrate SpName.xml → JSON configuration
- [ ] Test all 100+ stored procedures

### External Services
- [ ] Integrate Redis (Fanex or StackExchange)
- [ ] Integrate Serilog + Sentry
- [ ] Integrate VaultSharp
- [ ] Integrate Mail service

### Caching
- [ ] Configure distributed cache
- [ ] Implement cache service
- [ ] Test cache invalidation

---

## 🌐 Phase 1E: WebApi Layer (Weeks 6-8)

### Minimal APIs
- [ ] Create endpoint groups
- [ ] Migrate all 45+ controllers → endpoints
  - [ ] Affiliate endpoints
  - [ ] Administrator endpoints
  - [ ] Auth endpoints
  - [ ] Domain endpoints
  - [ ] Customer endpoints
  - [ ] Campaign endpoints
  - [ ] Operation endpoints
  - [ ] Report endpoints
  - [ ] UserPanel endpoints

### Authentication
- [ ] Implement JWT authentication
- [ ] Migrate ONEbook.UM integration
- [ ] Configure authorization policies
- [ ] Test authentication flow

### Middleware
- [ ] Add exception handling middleware
- [ ] Add logging middleware
- [ ] Add request/response logging
- [ ] Add CORS policy
- [ ] Add health checks

### Documentation
- [ ] Configure Swagger/OpenAPI
- [ ] Add XML documentation
- [ ] Test all endpoints in Swagger

---

## 🎨 Phase 1F: Frontend Integration (Weeks 8-9)

### Static Files
- [ ] Copy Areas/Portal/www/dist/ → ONEHub.WebApi/wwwroot/
- [ ] Configure static file serving
- [ ] Test static file access

### API Updates (200+ endpoints)
- [ ] Document all old endpoint URLs
- [ ] Document all new endpoint URLs
- [ ] Update React API calls
  - [ ] Affiliate pages
  - [ ] Administrator pages
  - [ ] Auth pages
  - [ ] Domain pages
  - [ ] Customer pages
  - [ ] Campaign pages
  - [ ] Operation pages
  - [ ] Report pages
  - [ ] UserPanel pages

### Authentication Flow
- [ ] Update login component
- [ ] Implement JWT storage
- [ ] Update API interceptor
- [ ] Test authentication persistence

### Testing
- [ ] Manual test all React pages
- [ ] Test all CRUD operations
- [ ] Test file uploads
- [ ] Test report generation

---

## 🧪 Phase 1G: Testing & Deployment (Weeks 9-10)

### Unit Tests
- [ ] Create test projects
```bash
dotnet new xunit -n ONEHub.Application.UnitTests -f net9.0
dotnet new xunit -n ONEHub.Infrastructure.UnitTests -f net9.0
```
- [ ] Migrate all 96+ existing tests
- [ ] Add new tests for handlers
- [ ] Achieve > 70% code coverage

### Integration Tests
- [ ] Create integration test project
- [ ] Test database operations
- [ ] Test API endpoints
- [ ] Test authentication flow

### Performance Testing
- [ ] Load test critical endpoints
- [ ] Compare performance vs old system
- [ ] Optimize slow queries
- [ ] Test concurrent users

### Security Audit
- [ ] Run security scan
- [ ] Review authentication implementation
- [ ] Check SQL injection protection
- [ ] Verify CORS configuration
- [ ] Test authorization rules

### Deployment
- [ ] Deploy to staging
- [ ] Run smoke tests on staging
- [ ] User acceptance testing (UAT)
- [ ] Fix staging issues
- [ ] Deploy to production
- [ ] Monitor for 24-48 hours
- [ ] Rollback plan ready

---

## 🚀 Phase 2: Frontend Modernization (Optional, Weeks 11-16)

### Phase 2A: Separate Frontend (Weeks 11-12)
- [ ] Create onehub-adminsite repository
- [ ] Initialize with Vite + React 18
- [ ] Set up TypeScript
- [ ] Configure ESLint + Prettier
- [ ] Set up folder structure

### Phase 2B: Migrate Components (Weeks 12-14)
- [ ] Convert .jsx → .tsx
- [ ] Convert class components → function components
- [ ] Replace Redux → Zustand
- [ ] Implement TanStack Query
- [ ] Update styling (Tailwind/Material UI)

### Phase 2C: Deploy (Weeks 14-16)
- [ ] Configure Vite build
- [ ] Set up CI/CD for frontend
- [ ] Configure CDN
- [ ] Deploy frontend
- [ ] Test production build

---

## 📊 Post-Migration

### Documentation
- [ ] Update technical documentation
- [ ] Create deployment guide
- [ ] Document new architecture
- [ ] Update team wiki

### Training
- [ ] Train team on .NET 9
- [ ] Train team on Clean Architecture
- [ ] Train team on CQRS + MediatR
- [ ] Train team on new deployment process

### Monitoring
- [ ] Set up application insights
- [ ] Configure alerts
- [ ] Monitor performance
- [ ] Track errors

### Retrospective
- [ ] Hold team retrospective
- [ ] Document lessons learned
- [ ] Update migration checklist for future use

---

## ✅ Done!

Congratulations! You've successfully migrated ONEHub Admin to .NET 9! 🎉

---

**Version:** 1.0  
**Created:** October 20, 2025  
**Status:** Ready to Use

