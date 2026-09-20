# Dock Rush — Stage 3 prototype pack (Candidate #001)

**Status:** HYPOTHESIS — not an approved product.  
**Purpose:** Cheapest playable experiment to learn if the shared-graph mechanic is fun.  
**Stage 2 source:** [../../docs/knowledge-base/concepts/dock-rush-spec.md](../../docs/knowledge-base/concepts/dock-rush-spec.md)

No Unity project exists in this repo yet. The small browser proof at [../../prototype/dock-rush-web/](../../prototype/dock-rush-web/) is allowed solely to test the mechanic before the Editor is available; it is not a second product or a replacement for the Unity prototype.

---

## 1. Final minimal gameplay specification

### Core mechanic

- Crates enter from a **spawn queue** onto the belt graph one at a time (or when the previous crate has cleared the spawn cell — pick one rule in code and keep it consistent).
- **Hubs** are tap targets. Each tap rotates that hub **90° clockwise**.
- A hub defines which **edges** are active for its current rotation (0–3). Rotating the hub can change **every** route that passes through it in one action.
- Crates **auto-advance** along active directed edges at a fixed tick (e.g. every 0.25–0.4s). No drag-to-move.
- A **bay** removes a crate only when **color** and the crate's visible **facing mark** match `acceptColor` and `acceptFacing`.

### Board representation (prototype)

Use a **graph**, not a full physics sim.

| Entity | Data |
|--------|------|
| `nodes` | `spawn`, `hub`, `bay`, `sink` (dead-end), optional `junction` |
| `edges` | Directed links `from → to`, each with `activeWhenHubRotation` (list of 0–3) for edges that leave a hub; static edges elsewhere |
| `hubs` | `id`, screen `position`, `rotation` (0–3), referenced by edges |
| `bays` | `id`, `position`, `acceptColor`, `acceptFacing` |
| `spawn` | `position`, `queue` of `{ color, facing }` |
| `crates` | Runtime: `color`, `facing`, `currentNode` or edge progress |

Placeholder visuals: colored squares/circles, hub = larger square, arrow on crate for facing, bay = outlined slot with color label.

### Input

- **One finger tap** on a hub → rotate 90° CW.
- **Restart** button (UI) after win/lose.
- Optional: tap **Next level** after win (proto only; no save).

### Win condition

- Spawn `queue` is empty **and** no crates remain on the graph (all delivered or none spawned).

### Lose condition (prototype — locked for Stage 3)

Primary: **deadlock**.

After any hub rotation (and after each movement tick if you batch checks), evaluate:

1. If any crate can move along an active edge, **not** deadlocked.
2. If spawn has a crate and the spawn node can accept/release it onto an active edge, **not** deadlocked.
3. Otherwise, if there exists **any** hub rotation (including current) that would allow (1) or (2) within the next **one** movement step, **not** deadlocked. (Limit search to one rotation per hub for performance — sufficient for 1–2 hubs and 5–10 levels.)
4. If none of the above: **lose — deadlock**.

**Do not** use “queue full” as the only fail state in this prototype. A small `maxOnBoard` cap (e.g. 4 crates) is allowed only as a safety valve against logic bugs, not as the designed player-facing fail.

### Level data format

Author in JSON under [levels/](levels/). Copy into Unity `StreamingAssets/DockRush/Levels/` or `Resources` when implementing.

**File:** `level_XX.json`

```json
{
  "id": 1,
  "name": "string",
  "hubs": [
    { "id": "h0", "position": [2, 2], "rotation": 0 }
  ],
  "nodes": [
    { "id": "spawn", "type": "spawn", "position": [0, 2] },
    { "id": "b0", "type": "bay", "position": [4, 2], "acceptColor": "Red", "acceptFacing": "West" },
    { "id": "pit", "type": "sink", "position": [2, 4] }
  ],
  "edges": [
    { "from": "spawn", "to": "h0", "hub": null },
    { "from": "h0", "to": "b0", "hub": "h0", "activeAtRotation": [0, 1] },
    { "from": "h0", "to": "pit", "hub": "h0", "activeAtRotation": [2, 3] }
  ],
  "queue": [
    { "color": "Red", "facing": "East" }
  ],
  "tickSeconds": 0.35
}
```

**Rules:**

- `position` is grid coordinates for layout only (portrait-friendly aspect).
- Edges that leave a hub must set `hub` + `activeAtRotation` (which hub rotations enable this exit).
- `facing` is a fixed directional mark carried by a crate from spawn; it does not change while the crate moves (`North` | `East` | `South` | `West`). A bay must show the same mark.
- Colors: `Red`, `Green`, `Blue`, `Yellow` (proto palette).

See committed [levels/](levels/) for six authored levels.

### Minimum required systems (in Unity, when it exists)

| System | Responsibility |
|--------|----------------|
| `LevelLoader` | Load JSON by id, validate schema |
| `DockGraph` | Nodes, edges, hub rotations, active edge query |
| `CrateSimulator` | Spawn queue, movement tick, bay delivery |
| `DeadlockChecker` | Rule in [lose condition](#lose-condition-prototype--locked-for-stage-3) |
| `HubInput` | Tap → rotate |
| `ProtoUI` | Level label, restart, win/lose overlay, next level |
| `PlaceholderView` | Sprites or `SpriteRenderer` quads — no art pipeline |

**~6–8 small MonoBehaviours or one controller + 2–3 plain C# classes.** No scene manager framework.

### Explicitly out of scope (Stage 3)

- GameFactoryCore (no AdsManager, AnalyticsManager, SaveManager, etc.)
- Monetization, ads, IAP, economy
- Analytics SDKs (optional `Debug.Log` only)
- Backend, accounts, remote config
- Progression save, level map, stars, meta
- Tutorial beyond level 1 layout
- Audio/haptics (optional single click — skip if it delays)
- Multiple scenes, addressables, asmdef splits (single assembly is fine)
- Polish menus, settings, store assets
- Automated tests (manual playtest only)

---

## 2. Smallest playable prototype

| Item | Choice |
|------|--------|
| Scenes | **1** — `DockRushProto` |
| Levels | **6** in JSON (expand to 10 if first session is &lt;3 min) |
| Interaction | Tap hubs |
| Flow | Load level → play → win/lose → restart or next |
| Visuals | Placeholder shapes + TextMeshPro labels |
| Platform target | Android mindset; Editor play mode is enough for Stage 3 gate |

### Suggested level arc (committed JSON)

1. One hub, one color — learn tap rewires path  
2. Wrong rotation sends crate to sink — learn fail  
3. Two crates, same hub — order matters  
4. Color + facing — select the bay with the matching mark
5. Two hubs — one tap affects multiple routes  
6. Near-deadlock — one rotation saves run  

---

## 3. Implementation plan (execute only after Unity project exists)

**Order of work (single session goal: playable in Editor):**

1. Founder creates Unity project (see below). Add folder `Assets/Games/DockRush/`.
2. Run `pwsh -File tools/Validate-DockRushLevels.ps1` from the repository root. Fix schema errors before copying data.
3. Copy `design/dock-rush/levels/*.json` → `Assets/StreamingAssets/DockRush/Levels/`.
4. Implement `DockGraph` + JSON loader; draw gizmos or quads at `position`.
5. Implement movement tick + bay acceptance; verify level 1 win manually.
6. Implement hub tap + rotation + edge activation.
7. Implement deadlock checker; verify level 2 lose.
8. Wire win/lose/restart/next UI.
9. Play through all 6 levels; log notes in [PLAYTEST_LOG](../../docs/templates/PLAYTEST_LOG.md) (one file, do not expand OS).

**Estimated code size:** &lt;400–600 lines C# total if kept disciplined.

**Do not start step 1 in CI/cloud** — no Unity in agent environment.

---

## 4. Prototype success / kill gate

Answer after founder playtest (~10 minutes, all levels). Use [PLAYTEST_LOG](../../docs/templates/PLAYTEST_LOG.md).

| Gate | PASS signal | FAIL signal |
|------|-------------|-------------|
| **Meaningful decisions** | Rotations clearly trade routes; at least one level needs planning two taps ahead | Any tap works; or graph is opaque |
| **Immediately understandable** | Founder explains goal in one sentence without reading docs | Needs explanation of facing or hubs after level 3 |
| **Retry urge** | Voluntary restarts after fail; fun ≥7/10 | “Done” after one fail; fun ≤5 |
| **Near-miss / deadlock moments** | At least 2 moments where almost locked, one tap fixes | No tension; fails feel random |
| **Level scalability** | You believe 50+ levels could be authored in JSON without new code | Every level needs new code |

**KILL (hypothesis rejected):** FAIL on **retry urge** OR **meaningful decisions**, OR loop feels like Car Jam / slide puzzle. Run Tilt Sort per OS — do not polish Dock Rush.

**CONTINUE (hypothesis survives):** PASS on retry + decisions + near-miss; scalability plausible. Then Stage 4 planning only — still not production.

**DECISION block after playtest:**

```text
DECISION: KILL | ITERATE | CONTINUE
WHY:
BUILD:
MEASURE:
KILL CONDITION:
NEXT ACTION:
```

---

## Unity setup (founder)

1. Install **Unity 2022.3 LTS** (or your chosen LTS; record version in root README).
2. **File → New Project** → 2D (Core) or 2D URP — either is fine for placeholders.
3. Project location: e.g. `unity/GameFactory/` inside this repo **or** separate repo; if inside, commit only `Assets/Games/DockRush/` + `StreamingAssets` — let Editor own `ProjectSettings/`.
4. Player: portrait, default resolution 1080×1920 reference; Android module installed when you want device build (not required for first Editor playtest).
5. Copy level JSON from `design/dock-rush/levels/`.
6. Implement per [§3](#3-implementation-plan-execute-only-after-unity-project-exists).

**Do not** hand-edit `ProjectSettings` in git before opening the project once in Editor.

---

## OS coherence note (2026-09)

The Stage 2 spec mentions optional GameFactoryCore managers for Stage 3. **This prototype pack overrides that:** zero Core until a vertical slice is justified. Aligns with [kickoff lesson](../../docs/knowledge-base/lessons/2026-09-kickoff-plan-correction.md).
