# GameFactoryCore — build-when-needed roadmap

**No C# exists in this repository yet.** This file says *when* a module may be written. It is not permission to stub unused managers.

## Creation rule

When Unity LTS is installed:

1. Create the project **in the Unity Editor** (File → New Project).
2. Do not hand-author `ProjectSettings` in git before that.
3. Add `Assets/GameFactoryCore/` with one `GameFactoryCore.asmdef`.
4. Add an explicit composition root.
5. Put game mechanics in `Assets/Games/<Codename>/` only.
6. Implement a module **the sprint it is first used**.

## Module gates

| Module | First allowed stage | Why wait |
|--------|---------------------|----------|
| GameFlowManager | Stage 3 | Boot → playing → win/lose → pause. Needed to answer “is it fun?” |
| AnalyticsManager (editor / file sink) | Stage 3 | Playtest events; names from [../../templates/ANALYTICS_SCHEMA.md](../../templates/ANALYTICS_SCHEMA.md) |
| LevelManager (data-driven) | Stage 3 | Only if the prototype has more than one level |
| SaveManager | Stage 4 | Persistence is a slice concern |
| AudioManager | Stage 4 | |
| HapticsManager | Stage 4 | |
| DebugMenu (`DEVELOPMENT_BUILD`) | Stage 4 | Skip level, clear save, test hooks — **off** in release |
| AdsManager (official **test** ads) | Stage 5 | Verify current vendor docs at integration time |
| ConsentManager | Stage 5 | Same |
| IAPManager | After traction | Premature before a surviving loop |
| RemoteConfigManager | After traction | Local defaults in JSON until then |
| EconomyManager | After traction | Soft currency is not a proto lesson |
| SceneLoader | Only if multi-scene loading is real pain | No framework-for-later |
| Common UI | When a **second** game needs the same control | Avoid one-game widgets in Core |

## Forbidden on day one

- Thirteen empty manager classes
- `_GameTemplate` fake win/lose as a substitute for a real proto
- Production ad SDKs “just to have them”
- Service locator forests
- Invented SDK APIs

## Boundary

`GameFactoryCore` = studio infrastructure.  
`Assets/Games/<Codename>/` = mechanics, levels, art.

If a class name contains the game’s unique rule (hubs, fuses, tilt), it does not belong in Core.

## Next engineering action (later engagement)

Founder installs Unity LTS → new 3D/2D URP or Built-in project (founder choice; document the version in the root README) → Android player settings in Editor → add only Stage 3 modules actually used by Dock Rush (or the backup).
