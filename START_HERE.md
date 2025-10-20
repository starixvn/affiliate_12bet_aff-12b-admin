# 🚀 START HERE - ONEHub Admin to .NET 9 Migration

Hi ONEHub Developer! This is your **ONE-PAGE** guide.

---

## 📊 What You Have Now

- **Framework:** .NET Framework 4.7.2 + ASP.NET MVC 5
- **Code:** 636 C# files (45+ controllers, 60+ services, 70+ commands)
- **Frontend:** React 16 + Redux (embedded in MVC, 1,175 files)
- **Database:** Dapper + 100+ stored procedures ✅ (no changes needed)
- **Auth:** ONEbook.UM 3.1.6 + Forms Authentication

---

## 🎯 Migration Plan: Two Phases

### Phase 1: Backend Only (10 weeks, REQUIRED)
- Migrate backend → .NET 9 + Clean Architecture + CQRS
- **Keep React embedded** (no frontend changes!)
- Users see NO difference
- Update API URLs only

### Phase 2: Frontend Modernization (6 weeks, OPTIONAL)
- **Only if you want:** React 16 → React 18 + TypeScript
- Can skip if budget/time limited

---

## 🚨 CRITICAL: Do This FIRST (Week 0)

**Before starting any migration work, verify these dependencies:**

### 1. Contact Fanex Team (2-3 days)
Ask: "Are these compatible with .NET 9?"
- Fanex.Caching.Redis 2.2.8
- Fanex.Logging.Sentry.RavenClient 1.0.1.2
- Fanex.MailDeliver.MailClient 2.0.11
- Fanex.Data.ProxyClient 1.0.3.6

**If NO:** Find alternatives (StackExchange.Redis, Serilog, MailKit)

### 2. Contact ONEbook.UM Team (2-3 days)
Ask: "Does ONEbook.UM 3.1.6 support .NET 9 / ASP.NET Core?"

**If NO:** Plan custom middleware or alternative auth

### 3. Create Proof of Concept (3-5 days)
```powershell
# Create test solution
dotnet new sln -n ONEHub.Test
dotnet new webapi -n ONEHub.Test.Api -f net9.0

# Test Fanex packages (if verified compatible)
# Migrate ONE simple service (AuthService)
# Should take ≤ 2 days (if longer = more complex than expected)
```

### 4. Go/No-Go Meeting (Day 10)
**✅ GO if:** Fanex packages work + ONEbook.UM works + PoC successful  
**❌ NO-GO if:** Critical dependencies blocked

---

## ✅ After GO Decision: Phase 1 (10 weeks)

### Weeks 1-2: Setup
```powershell
# Create solution
dotnet new sln -n ONEHub
dotnet new classlib -n ONEHub.Domain -o src/ONEHub.Domain -f net9.0
dotnet new classlib -n ONEHub.Application -o src/ONEHub.Application -f net9.0
dotnet new classlib -n ONEHub.Infrastructure -o src/ONEHub.Infrastructure -f net9.0
dotnet new webapi -n ONEHub.WebApi -o src/ONEHub.WebApi -f net9.0

# Add Central Package Management
# Create Directory.Packages.props
```

### Weeks 2-3: Domain Layer
- Migrate 100+ DTOs → Domain entities
- Create enums & constants

### Weeks 3-5: Application Layer
- Convert 60+ Services → MediatR Handlers
- Convert 70+ Commands → CQRS Commands

### Weeks 5-6: Infrastructure Layer
- Configure Dapper repositories
- Integrate Redis, Serilog, Vault

### Weeks 6-8: WebApi Layer
- Create 45+ Minimal API endpoints
- Replace Forms Auth → JWT

### Weeks 8-9: Frontend Integration
- Copy React to `ONEHub.WebApi/wwwroot/`
- Update 200+ API calls (e.g., `/Affiliate/ApproveAffiliate` → `/api/affiliates/approve`)

### Weeks 9-10: Testing & Deploy
- Migrate 96 unit tests
- Integration testing
- Deploy to production

---

## 📚 Need More Details?

**Read these only when needed:**

1. **[MIGRATION_TO_NET9.md](MIGRATION_TO_NET9.md)** - Complete Phase 1 & 2 guide
   - Week-by-week tasks
   - Code examples & commands
   - Architecture patterns

2. **[CHECKLIST.md](CHECKLIST.md)** - Track your progress
   - Printable checklist
   - Week-by-week tracking

---

## 💰 Resources Needed

- **Team:** 3-4 backend developers
- **Time:** 10 weeks (Phase 1) + 6 weeks (Phase 2 if needed)
- **Skills:** .NET 9, ASP.NET Core, Clean Architecture, CQRS

---

## 📞 Decision Tree

```
START
  ↓
Week 0: Verify Dependencies
  ↓
Are Fanex packages compatible?
  ├─ YES → Continue
  └─ NO → Find alternatives or STOP
  ↓
Is ONEbook.UM compatible?
  ├─ YES → Continue
  └─ NO → Custom middleware or STOP
  ↓
PoC successful in ≤ 2 days?
  ├─ YES → GO to Phase 1
  └─ NO → Re-evaluate complexity
  ↓
Phase 1: 10 weeks backend migration
  ↓
Backend working?
  ├─ YES → Deploy to production
  └─ NO → Fix issues
  ↓
Want to modernize frontend?
  ├─ YES → Phase 2 (6 weeks)
  └─ NO → DONE! ✅
```

---

## 🎯 Bottom Line

**What:** Migrate .NET Framework 4.7.2 → .NET 9  
**How:** Two phases (backend first, frontend optional)  
**When:** 10-16 weeks total  
**Risk:** Medium (depends on Fanex/ONEbook.UM)  

**FIRST STEP:** Verify dependencies (Week 0) 🚨

---

## ✅ Quick Checklist

- [ ] Read this document (10 minutes) ✅ You're here!
- [ ] Contact Fanex team about packages
- [ ] Contact ONEbook.UM team about ASP.NET Core
- [ ] Create Proof of Concept (≤ 2 days)
- [ ] Hold Go/No-Go meeting
- [ ] Start Phase 1 (if GO)

---

**Questions?**
- Phase 1 details → [MIGRATION_TO_NET9.md](MIGRATION_TO_NET9.md)
- Track progress → [CHECKLIST.md](CHECKLIST.md)

**Ready? Contact Fanex team now!** 📞

---

**Version:** 1.0  
**Created:** October 20, 2025  
**This is the ONLY document you need to read first!**

