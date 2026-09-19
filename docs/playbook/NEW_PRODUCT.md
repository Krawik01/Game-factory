# How to open a new product bet

Copy this checklist. Do not invent a new process.

## 1. Read memory

- [ ] Open [../knowledge-base/INDEX.md](../knowledge-base/INDEX.md)
- [ ] Read the last **three** lessons / postmortems
- [ ] Note failed mechanics to avoid and reusable modules to keep

## 2. Stage 0 — Market

- [ ] Copy [../templates/STAGE0_MARKET.md](../templates/STAGE0_MARKET.md) to `docs/knowledge-base/market/YYYY-MM-<slug>.md`
- [ ] Fill signals, sources + evidence grade, clone-risk, studio-fit
- [ ] Write one opportunity thesis
- [ ] Update INDEX active pipeline

## 3. Stage 1 — Ideation

- [ ] Copy [../templates/STAGE1_IDEATION.md](../templates/STAGE1_IDEATION.md) to `docs/knowledge-base/concepts/YYYY-MM-ideation.md` (or append)
- [ ] Score **at least three** distinct concepts on all ten dimensions
- [ ] Recommend only the strongest
- [ ] Name an explicit **backup** concept
- [ ] Tie-break with production speed + technical simplicity when overall scores are close

## 4. Stage 2 — Concept spec

- [ ] Copy [../templates/STAGE2_CONCEPT.md](../templates/STAGE2_CONCEPT.md) to `docs/knowledge-base/concepts/<codename>-spec.md`
- [ ] Fill every field
- [ ] State the unique rule so it **fails** the clone-risk filter
- [ ] Keep v1 extremely small
- [ ] Write the prototype kill question

## 5. Founder decision

- [ ] Fill [DECISION_TEMPLATE.md](DECISION_TEMPLATE.md)
- [ ] Founder is the final decision-maker
- [ ] If the unique rule fails gut-check, **do not protect** the concept — run the backup through this same list

## 6. Stage 3 — Prototype (only if Unity exists)

- [ ] Unity LTS is installed
- [ ] Project was created **in the Editor** (see [../knowledge-base/tech/GAMEFACTORY_CORE_ROADMAP.md](../knowledge-base/tech/GAMEFACTORY_CORE_ROADMAP.md))
- [ ] Copy [../templates/STAGE3_PROTOTYPE.md](../templates/STAGE3_PROTOTYPE.md)
- [ ] Build only: core gameplay, juice, win/lose, restart, rough levels
- [ ] Log sessions with [../templates/PLAYTEST_LOG.md](../templates/PLAYTEST_LOG.md)

If Unity is not installed, **stop here**. Do not hand-author a Unity project.

## 7. After playtests

- [ ] Stage 10 decision: KILL / ITERATE / CONTINUE / SCALE
- [ ] On KILL or SCALE: write [../templates/POSTMORTEM.md](../templates/POSTMORTEM.md) and update INDEX
- [ ] CONTINUE / ITERATE: next single highest-value action only

## Decision footer for this bet

```text
DECISION:
WHY:
BUILD:
MEASURE:
KILL CONDITION:
NEXT ACTION:
```
