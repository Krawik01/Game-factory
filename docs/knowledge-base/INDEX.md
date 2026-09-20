# Studio Knowledge Base — INDEX

Living memory. Read this before designing a new game. Update it after every Stage 10 and every postmortem.

## Active pipeline

| Codename | Stage | Last decision | Artifact |
|----------|-------|---------------|----------|
| Dock Rush | 3 — First playable tested | KILL — fun 2/10; unclear core loop | [first-playtest lesson](lessons/2026-09-dock-rush-first-playtest.md) |

## Tested concepts

| Codename | Stage reached | Decision | Notes |
|----------|---------------|----------|--------|
| Dock Rush | 3 playable | KILL | Browser prototype: unclear goal, weak levels, fun 2/10. Do not polish or port. |
| Tilt Sort | 1 | Backup | See [2026-09-ideation.md](concepts/2026-09-ideation.md) |
| Fuse Line | 1 | Not recommended | |
| Stack Pulse | 1 | Not recommended | Fairness / tuning risk |

## Successful mechanics

| Mechanic | Where learned | Reuse? |
|----------|---------------|--------|
| Shared-graph hub rotation | Dock Rush first playable (2026-09-20) | Do not assume a routing rewire is inherently satisfying; prove immediate cause/effect before authoring content. |

## Failed mechanics

| Mechanic | Where learned | Do not repeat |
|----------|---------------|---------------|
| — | No playtests yet | |

## Retention results

| Product | D1 | D7 | Source | Date |
|---------|----|----|--------|------|
| — | | | No live data | |

## Monetization results

| Product | Rewarded accept | IAP | Source | Date |
|---------|-----------------|-----|--------|------|
| — | | | No live data | |

## Creative results

| Product | Concept | Result | Date |
|---------|---------|--------|------|
| — | | No live creatives | |

## Technical problems

| Problem | Product | Resolution |
|---------|---------|------------|
| Hand-authored Unity project without Editor | Kickoff draft | Rejected. Create projects in Editor only. |
| 13 empty Core managers on day one | Kickoff draft | Rejected. Build-when-needed roadmap. |

## Reusable modules

See [tech/GAMEFACTORY_CORE_ROADMAP.md](tech/GAMEFACTORY_CORE_ROADMAP.md). **No C# modules exist yet.**

## Lessons learned

| Date | Lesson | File |
|------|--------|------|
| 2026-09 | OS first. No unverified Unity skeleton. No framework-for-later. Differentiation is a rule, not a reskin. | [lessons/2026-09-kickoff-plan-correction.md](lessons/2026-09-kickoff-plan-correction.md) |
| 2026-09-20 | Dock Rush shared-graph loop was unclear and scored fun 2/10 in its first playable. Kill it rather than polishing it. | [lessons/2026-09-dock-rush-first-playtest.md](lessons/2026-09-dock-rush-first-playtest.md) |

## Metrics benchmarks

Fill after the first soft launch. Empty on purpose.

| Metric | Studio bar | Notes |
|--------|------------|-------|
| Founder proto fun | ≥7/10 at 10 min (interim) | Until live D1 exists |
| Voluntary restarts (proto) | ≥3 in a session | Interim |
| D1 retention | TBD | |
| Crash-free | TBD | |
