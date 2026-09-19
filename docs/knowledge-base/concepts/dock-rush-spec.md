# Stage 2 — Dock Rush concept spec

**Codename:** Dock Rush  
**Date:** 2026-09-19  
**Stage 1 source:** [2026-09-ideation.md](2026-09-ideation.md)

## One-sentence pitch

Tap a hub to rewire the whole dock graph and send crates into bays that match **color and facing** before the factory deadlocks.

## Core mechanic

Crates spawn onto belts. Hubs are nodes in a **shared graph**. Rotating one hub by 90° reconnects every belt attached to that hub, which can change several paths at once. Bays accept a crate only when color **and** entry direction match. A full clear of the level’s crate quota wins. A state with no legal rotation sequence that can clear the jam is a **deadlock** loss.

## Unique rule (clone-risk)

This rule must fail the clone-risk filter. If you can swap our art onto Car Jam / Color Sort / Pixel Flow and the sentence still works, stop and redesign.

1. **Shared graph:** one hub rotation reroutes **all** connected belts at once (one tap, many paths).
2. **Color + facing:** a crate has a color **and** a facing; a bay checks both.
3. **Deadlock loss:** you lose when the graph has **no legal rotation** that can still sort remaining crates — not only when a queue is “full.”

Not unique (do not ship these as the pitch): “2D docks instead of 3D screws,” “cars but top-down,” “color sort on a belt.”

## Player objective

Sort every crate in the level into a legal bay. Optional later: a star if the player never entered a near-deadlock (not required for prototype).

## Loss condition

**Graph deadlock.** Prototype may also fail-fast if a belt backlog exceeds a visible slot limit *as a teaching cue*, but the designed lose state is deadlock (no useful rotation remains). Queue-full alone is not the unique fail.

## Win condition

All required crates are accepted by legal bays.

## Control scheme

One hand, one finger: **tap a hub to rotate it 90°**. No multi-touch. Swipe-to-rotate is a polish option in Stage 4, not a prototype requirement.

## 30-second gameplay loop

See incoming crate colors and facings → tap a hub → whole factory rewires → crates flow → a near-miss jam forms → one more tap opens a path → slot-in juice → next crate. If the player waits too long or rotates blindly, paths fight each other and the graph deadlocks.

## Progression

Integer level index. Every ~5 levels introduce one new constraint: extra color, a second hub, a one-way belt, a bay that only accepts from one side, a hidden facing (later — not in proto).

## Difficulty scaling

- More hubs (larger graph)
- Faster spawn **only after** the graph is readable (do not hide bad levels behind speed)
- Tighter facing constraints
- Deadlock windows that require planning two rotations ahead

## Visual identity

Toy factory, overhead or shallow 3/4. High-contrast crate colors. Facings as a clear chevron or arrow on the crate. Hubs chunky and tappable. Slot-in: bounce + particles. Deadlock: belts freeze, not a lecture.

## Monetization opportunities (not in prototype)

- Rewarded: **undo last rotation** or **+3 backlog slots** (teaching cue) or **reveal a safe hub** (hint)
- Interstitial: between levels or after fail-to-restart, never mid-flow
- IAP: wait for traction

## Why a player would play another level

The next graph is a new toy. A fail is “I see the rotation I missed.” Stars / no-deadlock (later) are optional.

## Why a TikTok viewer would stop scrolling

Mute overhead shot: factory about to lock → one tap rewires **everything** → cascade of crates seating. The rewire is the hook, not a UI caption.

## Prototype scope (Stage 3)

**Allowed:** core gameplay, basic juice, win/lose, restart, 5–10 hand-authored levels in JSON or ScriptableObjects.

**Forbidden:** store, settings, polished menus, hundreds of levels, economy, production ads, unused Core managers.

**Unity:** only after LTS is installed and the project is created **in the Editor**. Game code in `Assets/Games/DockRush/`. **First playable (Stage 3):** follow [../../../design/dock-rush/PROTO.md](../../../design/dock-rush/PROTO.md) — **no GameFactoryCore** until a vertical slice is justified.

## Kill question

After 5–10 rough levels: is rotating a **shared graph** tense and satisfying for **3+ minutes**, with a **retry urge** after deadlock? If rotation feels fiddly, opaque, or like “slide cars,” the mechanic is not worth continuing.

## Founder gut-check

- [ ] Unique rule is real, not presentation (all three clauses)
- [ ] v1 is small enough
- [ ] If gut-check fails: run **Tilt Sort** through [../../playbook/NEW_PRODUCT.md](../../playbook/NEW_PRODUCT.md). Do not protect Dock Rush.

## Decision

```text
DECISION: CONTINUE
WHY: Spec is small, video-readable, and clone-resistant only if we keep shared-graph + facing + deadlock.
BUILD: Nothing in Unity until LTS exists. After gut-check: Stage 3 proto (5–10 levels, win/lose/restart).
MEASURE: Founder playtest fun ≥7/10 at 10 minutes; ≥3 voluntary restarts; visible near-miss rewires.
KILL CONDITION: By level 5, no tension, no retry urge, or the loop is indistinguishable from Car Jam.
NEXT ACTION: Founder gut-check this spec. If pass, install Unity LTS and open Stage 3. If fail, spec Tilt Sort.
```
