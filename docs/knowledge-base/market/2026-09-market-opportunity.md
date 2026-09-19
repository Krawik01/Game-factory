# Stage 0 — Market opportunity (2026-09)

## Thesis (one paragraph)

Hybrid-casual puzzles still look like the highest-velocity mobile lane for a one-person Android studio: instant rules, short sessions, data-driven levels, and natural fail/retry rewarded ads. Sort, block, and screw are **crowded**. GAME FACTORY should only sit next to those lanes if the concept has a **rule** a reskin cannot steal. Otherwise prefer line/sequence or physics-lite niches that still read in a two-second mute video.

## Signals

| Signal | What we observed | Date |
|--------|------------------|------|
| Hybrid-casual share | Industry writing describes a shift from hyper-casual CPI games toward hybrid-casual (deeper loop, IAA + light IAP, higher LTV pressure) | 2025–2026 coverage |
| Puzzle subgenres | Sort / block / screw described as the center of gravity; sort called out as pulling ahead in 2026 commentary | 2026 |
| Breakout patterns | Conveyor / auto-resolve (Pixel Flow-like), static designed boards (Color Block Jam-like), traffic-out (Car Jam-like), screw/layer discovery | 2025–2026 |
| Creative | One-board-many-goals and one-tap near-miss payoffs show up repeatedly as Shorts grammar | 2026 |
| Studio constraint | Typical publisher hybrid-casual is described as long (many months). We cannot copy that scope. We need days-to-proto. | Standing rule |

## Sources and evidence grade

Grade: **playtest** · **store dashboard / first-party** · **industry blog** (directional) · **anecdote**.

| Source | URL or name | Grade | Used for |
|--------|-------------|-------|----------|
| Gamigion — Hybridcasual Puzzles 2026 | https://www.gamigion.com/hybridcasual-puzzles-the-next-big-battleground-in-2026/ | Industry blog | Lane heat, sort/block/screw consolidation |
| Gamigion — Sort, Block, Screw | https://www.gamigion.com/new-puzzle-order-the-rise-of-sort-block-and-screw/ | Industry blog | Subgenre shift; IAP vs IAA mix |
| Friendly GameDev — State of hybrid casual puzzles | https://friendlygamedev.com/blog/state-of-hybrid-casual-puzzles/ | Industry blog | Named hits (Pixel Flow, Color Block Jam, Screwdom); conveyor trend |
| Unity — Hyper to hybrid-casual | https://unity.com/blog/mobile-gaming-shift-hyper-hybrid-casual | Industry blog | LTV/CPI pressure; hybrid definition |
| Naavik — Mobile convergence | https://naavik.co/weekly-digest/the-great-mobile-convergence/ | Industry blog | Instant core + light meta; not full midcore |

**Honesty:** this report is **directional**. We do not have first-party Play Console or Sensor Tower dashboards in-repo. Treat numbers in those articles as unverified. Stage 3 playtests are our first primary evidence.

## Emerging mechanics

- Conveyor / auto-resolve around a visible goal (Pixel Flow-like)
- Designed static boards with constraints (Color Block Jam-like), not endless score-attack
- Traffic routing + color matching (Car Jam / jam hybrids)
- Line / arrow / sequence puzzles (Arrows, Jigsolitaire-adjacent commentary)
- Screw / layered 3D discovery (heavy content and 3D cost — poor studio fit)

## Fast-growing competitors

| Title | Mechanic | Why it matters | Clone-risk to us |
|-------|----------|----------------|------------------|
| Color Block Jam | Slide/block on a designed board | Proved “level is the puzzle” | High if we only retheme sliding |
| Pixel Flow | Conveyor / sort-adjacent auto flow | Strong IAP commentary; video-readable | High if we only retheme conveyor sort |
| Screwdom / screw-likes | Layered fasteners | Deep content, 3D | High cost; do not chase |
| Car Jam-likes | Extract vehicles / route traffic | One-hand, Shorts-native | High if Dock Rush is only “2D cars” |
| Hexa Sort-likes | Sort on a board | Saturated sort | High if we ship another tube/board sort |

## Repeated creative patterns

- Overhead or 3/4 board, mute-readable colors
- Near-jam / near-fail, then one input saves the run
- Cascade / ASMR settle as payoff
- “One more try” fail card that implies a rewarded revive

## Underserved variations

- **Shared-graph routing:** one tap rewires many paths (not extract-one-car)
- **Line/sequence** with a single stroke and a snap-fail (not match-3)
- **Physics-lite tilt sort** that is not water-sort tubes (device and fairness risk)

## Video-first mechanics

A stranger on mute should see: goal colors, a jam forming, one input, a cascade. If the first second is a menu or a lore card, it fails this studio.

## Clone-risk list

If the unique sentence still works after swapping art onto these hits, **reject** or add a rule they cannot steal by reskin.

| Hit | Sentence that must NOT still fit our concept |
|-----|----------------------------------------------|
| Car Jam | “Slide pieces out of a jammed lot before the board locks.” |
| Color Sort | “Pour or tap to stack matching colors in tubes.” |
| Pixel Flow | “Let a conveyor auto-sort colors into a picture / bin.” |
| Color Block Jam | “Slide colored blocks on a static grid until the board is empty.” |
| Screwdom | “Remove screws in order to drop layers.” |

## Studio-fit

| Filter | Pass / Fail | Note |
|--------|-------------|------|
| Understood in seconds | PASS (lane) | Puzzle goals are color + clear |
| One hand | PASS | Tap / swipe / tilt |
| Short sessions | PASS | Level = session |
| Satisfying juice | PASS | Slot-in / cascade |
| Hundreds of levels or procedural | PASS | Data-driven boards |
| Natural rewarded-ad moments | PASS | Fail / hint / extra slot |
| Shorts-readable | PASS | Near-miss grammar exists |
| Little / no backend | PASS | Local save only |
| One-dev + AI in days-to-proto | PASS only if we refuse 3D screw / LiveOps | |
| No LiveOps required | PASS | Streaks later, not at proto |

**Studio-fit overall:** PASS for the **lane**, not for a clone of a named hit.

## Opportunity thesis

Pursue **underserved fusions** next to sort/block **only with a distinct rule**. Do not spend a prototype sprint on another color-sort or car-jam reskin. Preferred first experiment: a **shared-graph dock** (one rotation changes many belts; color + facing; deadlock loss). Fallbacks: physics-lite tilt or one-stroke fuse sequence.

## Recommended lanes (max 2)

1. Shared-graph routing / factory docks (distinct rule required)
2. Line-sequence or physics-lite sort (if graph routing fails gut-check or proto speed)

## Decision

```text
DECISION: CONTINUE
WHY: The lane fits Android hybrid-casual and one-dev constraints; clones do not. We need a scored concept with a real unique rule.
BUILD: Stage 1 scorecards for four concepts; Stage 2 only for the winner.
MEASURE: Winner fails clone-risk filter and scores highest on production speed + simplicity when overall scores are close.
KILL CONDITION: Every concept fails clone-risk or studio-fit — return to Stage 0, do not force Dock Rush.
NEXT ACTION: Fill Stage 1 ideation and the Dock Rush spec with the shared-graph rule.
```
