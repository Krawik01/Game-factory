# Stage 1 — Ideation (2026-09)

**Lane:** [../market/2026-09-market-opportunity.md](../market/2026-09-market-opportunity.md)

Scores are **hypotheses** (1–10). Overall = average of the ten dimensions, one decimal. Tie-break: production speed, then technical simplicity.

## Concepts

### Concept A — Dock Rush

- **Pitch:** One tap rotates a shared dock hub and rewires every connected belt. Crates need matching **color and entry facing**. You lose when the graph **deadlocks**.
- **Unique rule (must fail clone-risk filter):** Shared-graph rewire + facing + deadlock — not “slide cars out of a lot,” not “pour colors into tubes,” not “conveyor auto-sorts a picture.”
- **Why it is not a reskin:** Changing the art to trucks or balls does not remove the multi-path rewire or facing check. Car Jam still works if you only extract one piece at a time.

### Concept B — Tilt Sort

- **Pitch:** Tilt the tray so balls roll into color gates. One-thumb tilt, physics-lite, no tubes.
- **Unique rule:** Continuous analog tilt into gates, not tap-to-pour stacks.
- **Why it is not a reskin:** Water-sort / ball-sort sentences fail (there is no stack inventory). Risk: low-end physics and “unfair” rolls.

### Concept C — Fuse Line

- **Pitch:** Draw one continuous fuse that must visit charges in order. A wrong link snaps the fuse.
- **Unique rule:** Single stroke + ordered nodes + snap-fail. Not match-3, not slide-block.
- **Why it is not a reskin:** Color Sort / Car Jam sentences do not describe a fuse path. Level authoring is slower than a graph of hubs.

### Concept D — Stack Pulse

- **Pitch:** Drop blocks on a beat. A mistimed drop wobbles the tower toward collapse.
- **Unique rule:** Rhythm gate on a stack — timing is the puzzle.
- **Why it is not a reskin:** Sort/jam sentences fail. Risk: fairness, headphone-off play, weaker rewarded-ad “hint” story.

## Scorecard (hypothesis)

| Dimension | Dock Rush (A) | Tilt Sort (B) | Fuse Line (C) | Stack Pulse (D) |
|-----------|---------------|---------------|---------------|-----------------|
| Instant understandability | 9 | 8 | 7 | 7 |
| Core-loop satisfaction | 8 | 8 | 7 | 7 |
| Replayability | 8 | 7 | 7 | 6 |
| Level scalability | 9 | 7 | 7 | 6 |
| Short-form-video potential | 9 | 8 | 8 | 9 |
| Rewarded-ad compatibility | 8 | 7 | 8 | 6 |
| Technical simplicity | 7 | 6 | 7 | 6 |
| Production speed | 8 | 6 | 6 | 5 |
| Visual distinctiveness | 7 | 7 | 7 | 8 |
| Expansion potential | 7 | 6 | 7 | 5 |
| **Overall (average)** | **8.0** | **7.0** | **7.1** | **6.5** |

Note: an earlier draft listed Tilt Sort at 7.4. Recalculating the ten dimensions above yields **7.0**. Fuse Line edges Tilt Sort on overall (7.1 vs 7.0), but **backup remains Tilt Sort** because the Stage 0 fallback lane was physics-lite and Tilt Sort is closer to a one-sitting proto if graph routing dies — **unless** founder prefers Fuse Line after gut-check. Tie-break does not apply to the winner (Dock Rush leads clearly).

## Recommendation

- **Winner:** Dock Rush
- **Backup:** Tilt Sort (Fuse Line is the alternate backup if tilt physics is rejected)
- **Why winner (one sentence):** Highest overall, strongest level scalability and video near-miss, and a unique rule that survives the clone-risk filter if we keep all three parts (graph, facing, deadlock).
- **Why not the others:** Tilt Sort is device-fragile; Fuse Line authors more slowly; Stack Pulse is unfair-risk and weaker rewarded design.

## Decision

```text
DECISION: CONTINUE
WHY: Dock Rush is the only concept that is both studio-fast and clone-resistant if the shared-graph rule is kept intact.
BUILD: Full Stage 2 spec for Dock Rush.
MEASURE: Spec lists all three unique-rule clauses; founder gut-check passes or we switch to Tilt Sort without debate.
KILL CONDITION: Unique rule is reduced to “2D docks / cars” — that is a clone; kill the spec, do not prototype.
NEXT ACTION: Write docs/knowledge-base/concepts/dock-rush-spec.md
```
