# .NET 8 Upgrade Plan — EnterpriseMvcApp

## 1. Goal and scope

- **Goal**: retarget the full solution from `net10.0` to `net8.0` and keep the solution building and tests passing.
- **Scope**: all 4 SDK-style projects in `EnterpriseMvcApp.slnx`.
- **Out of scope**: feature refactors not required for framework compatibility.

Projects in dependency order:
1. `EnterpriseMvcApp.Domain/EnterpriseMvcApp.Domain.csproj`
2. `EnterpriseMvcApp.Application/EnterpriseMvcApp.Application.csproj`
3. `EnterpriseMvcApp.Infrastructure/EnterpriseMvcApp.Infrastructure.csproj`
4. `EnterpriseMvcApp.Web/EnterpriseMvcApp.Web.csproj`

## 2. Assessment summary used for planning

- Target selected: `net8.0` (.NET 8 LTS).
- Analysis results: **0 issues** across projects, packages, and API compatibility.
- Estimated code impact from analysis: **0 LOC** (framework retarget expected to be mostly project-file changes).

## 3. Upgrade strategy

Use a **single coordinated retarget** across all projects, then validate restore/build/tests and only apply package downgrades if validation reveals incompatibilities.

Reasoning:
- Small solution (4 projects) with clean assessment.
- All projects already SDK-style.
- Clear project dependency chain allows fast root-cause isolation if build breaks.

## 4. Detailed phased plan

### Phase 1 — Iterative pre-upgrade preparation

#### Iteration 1.1 — Baseline and environment validation
**Actions**
1. Confirm current branch and clean working tree for upgrade isolation.
2. Validate installed SDK support for `net8.0`.
3. Detect `global.json` and verify SDK pin does not block .NET 8.
4. Capture baseline build/test state before edits.

**Validation outputs**
- SDK validation result for `net8.0`.
- Current `global.json` compatibility status.
- Baseline build and baseline test summary.

#### Iteration 1.2 — Upgrade impact baseline capture
**Actions**
1. Reconfirm topological project order for troubleshooting.
2. Record current package graph per project (especially EF Core and MediatR ecosystem packages).
3. Keep a snapshot of current `TargetFramework` values for rollback traceability.

**Validation outputs**
- Dependency snapshot and pre-change framework map.

---

### Phase 2 — Iterative upgrade execution

#### Iteration 2.1 — Retarget project frameworks
**Actions**
1. Update `<TargetFramework>net10.0</TargetFramework>` to `<TargetFramework>net8.0</TargetFramework>` in:
   - `EnterpriseMvcApp.Domain/EnterpriseMvcApp.Domain.csproj`
   - `EnterpriseMvcApp.Application/EnterpriseMvcApp.Application.csproj`
   - `EnterpriseMvcApp.Infrastructure/EnterpriseMvcApp.Infrastructure.csproj`
   - `EnterpriseMvcApp.Web/EnterpriseMvcApp.Web.csproj`
2. Check for shared props/targets (`Directory.Build.props`, `Directory.Build.targets`) and align target framework if present.

**Validation outputs**
- Diff confirms all intended framework updates applied once.

#### Iteration 2.2 — Restore/build stabilization
**Actions**
1. Run restore/build for the solution.
2. If NuGet compatibility errors occur, resolve per failing package using supported versions for `net8.0`:
   - `Microsoft.EntityFrameworkCore*`
   - `MediatR*`
   - `AutoMapper*`
   - `FluentValidation*`
3. Rebuild after each package adjustment set.

**Validation outputs**
- Successful solution build.
- Final package versions compatible with `net8.0`.

#### Iteration 2.3 — Runtime and Razor Pages sanity checks
**Actions**
1. Execute test projects (if present).
2. Validate startup and key Razor Pages flows for the web app.
3. Verify EF Core migrations tooling behavior for the infrastructure project if used in this solution.

**Validation outputs**
- Test run summary.
- Basic application startup and page navigation confirmation.

---

### Phase 3 — Iterative post-upgrade hardening

#### Iteration 3.1 — Cleanup and consistency
**Actions**
1. Ensure no stale framework references remain (`net10.0`, preview-specific SDK expectations).
2. Confirm nullable/implicit usings settings remain unchanged unless explicitly required.
3. Regenerate lock files/assets as needed via restore.

**Validation outputs**
- Workspace consistency check passes.

#### Iteration 3.2 — Final verification and readiness
**Actions**
1. Final full solution build.
2. Final test execution.
3. Prepare change summary with modified files and any package version adjustments.

**Validation outputs**
- Build green.
- Tests green (or documented known non-upgrade-related failures).
- Ready-for-review summary.

## 5. Risks and mitigations

1. **Package-to-framework mismatch after retarget**
   - Mitigation: resolve using supported package versions for `net8.0`, then rebuild.

2. **SDK pinning blocks build**
   - Mitigation: adjust `global.json` to a compatible SDK band for .NET 8.

3. **Hidden runtime behavior differences**
   - Mitigation: execute smoke tests on critical Razor Pages endpoints and data access paths.

## 6. Definition of done

Upgrade is complete when:
- All projects target `net8.0`.
- Solution restore/build succeeds.
- Tests run successfully (or failures are documented as pre-existing/non-upgrade issues).
- No unresolved compatibility blockers remain.
