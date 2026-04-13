# EnterpriseMvcApp .NET 8 Upgrade Tasks

## Overview

This document tracks the execution of EnterpriseMvcApp solution upgrade from .NET 10.0 to .NET 8.0 LTS. All 4 SDK-style projects will be retargeted simultaneously in a single coordinated operation, followed by testing and validation.

**Progress**: 3/4 tasks complete (75%) ![0%](https://progress-bar.xyz/75)

---

## Tasks

### [✓] TASK-001: Verify prerequisites and environment *(Completed: 2026-04-09 17:03)*
**References**: Plan §Phase 1 Iteration 1.1

- [✓] (1) Verify .NET 8 SDK installed and available for net8.0 target
- [✓] (2) SDK supports net8.0 target (**Verify**)
- [✓] (3) Check for global.json file in repository root
- [✓] (4) If global.json exists, verify SDK pin does not block .NET 8 (**Verify**)

---

### [✓] TASK-002: Atomic framework retarget with build stabilization *(Completed: 2026-04-09 17:06)*
**References**: Plan §Phase 2 Iteration 2.1, Plan §Phase 2 Iteration 2.2

- [✓] (1) Update TargetFramework from net10.0 to net8.0 in all 4 project files per Plan §1 Goal and Scope (EnterpriseMvcApp.Domain, EnterpriseMvcApp.Application, EnterpriseMvcApp.Infrastructure, EnterpriseMvcApp.Web)
- [✓] (2) All project TargetFramework properties updated to net8.0 (**Verify**)
- [✓] (3) Check for Directory.Build.props and Directory.Build.targets files under repository root and align target framework if present
- [✓] (4) Shared build files aligned with net8.0 if present (**Verify**)
- [✓] (5) Restore dependencies for entire solution
- [✓] (6) All dependencies restored successfully (**Verify**)
- [✓] (7) Build solution and fix all compilation errors and NuGet compatibility issues per Plan §Phase 2 Iteration 2.2 (use net8.0-compatible package versions for EntityFrameworkCore, MediatR, AutoMapper, FluentValidation as needed)
- [✓] (8) Solution builds with 0 errors (**Verify**)

---

### [✓] TASK-003: Execute tests and validate upgrade *(Completed: 2026-04-09 14:06)*
**References**: Plan §Phase 2 Iteration 2.3, Plan §Phase 3 Iteration 3.2

- [✓] (1) Run test projects per Plan §Phase 2 Iteration 2.3 (if present in solution)
- [✓] (2) Fix any test failures from framework changes
- [✓] (3) Re-run tests after fixes
- [✓] (4) All tests pass with 0 failures (**Verify**)

---

### [▶] TASK-004: Final commit
**References**: Plan §6 Definition of Done

- [ ] (1) Commit all changes with message: "TASK-004: Complete upgrade from net10.0 to net8.0"

---





