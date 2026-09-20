# Dock Rush — first playable result

**Date:** 2026-09-20  
**Build:** browser mechanic prototype (`8951c28`)  
**Decision:** ITERATE

## Evidence

- First-player feedback: the goal was not immediately understandable.
- Levels felt weak.
- Fun score: **2/10**.

## Lesson

The shared-graph pitch did not become an obvious, satisfying decision loop in its smallest playable form. A rotating hub changed routes, but the player did not get a clear enough before/after payoff to make the action feel good. Do not rescue the current auto-run version with more levels, art, progression, ads, or a Unity port.

## Decision

```text
DECISION: ITERATE
WHY: The core may fit a logic-strategy game, but the auto-run version hid cause/effect and scored 2/10.
BUILD: One turn-based shared-graph board: visible route previews, a small action budget, then an explicit Run step.
MEASURE: Player can state the goal before acting and rates the new board >=7/10 after a short test.
KILL CONDITION: Fun <=5/10 or unclear goal again in the first session.
NEXT ACTION: Build one browser decision-board prototype, not a new six-level pack.
```
