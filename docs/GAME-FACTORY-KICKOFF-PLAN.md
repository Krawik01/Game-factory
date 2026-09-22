# GAME FACTORY — Full Kickoff Plan

Plik do czytania na telefonie. **Najłatwiejsze kopiowanie:** użyj wersji [GAME-FACTORY-KICKOFF-PLAN.txt](./GAME-FACTORY-KICKOFF-PLAN.txt) (plain text, bez tabel).

---

## Current state

- Repo: README only, single commit on `main`
- No Unity project, no GameFactoryCore, no portfolio memory yet

## Target repository layout

```
/workspace/
  README.md
  docs/
    knowledge-base/
      INDEX.md
      market/
      concepts/
  unity/
    GameFactory/
      Packages/manifest.json
      ProjectSettings/
      Assets/
        GameFactoryCore/
        _GameTemplate/
        Games/
```

- **Unity:** 2022.3 LTS, Android-first
- **Boundary:** Core = infrastructure; `Games/<Codename>` = game mechanics

---

## Part A — Technical bootstrap

See `.txt` file for full module list and acceptance criteria.

---

## Part C–F — Market, ideation, Dock Rush spec, execution

Full text in [GAME-FACTORY-KICKOFF-PLAN.txt](./GAME-FACTORY-KICKOFF-PLAN.txt).

**DECISION:** CONTINUE to Stage 3 prototype **Dock Rush** after bootstrap.

**NEXT ACTION:** Merge kickoff PR → `Assets/Games/DockRush/` core loop.
