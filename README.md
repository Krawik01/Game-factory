# GAME FACTORY

This repository is the operating system of a one-person mobile game studio.

It is not a single game. It is the machine that repeatedly discovers, prototypes, validates, publishes, measures, improves, and kills mobile game concepts until a scalable winner appears.

The founder is the final decision-maker.

## Start here

| Need | Open |
|------|------|
| Rules of the studio | [docs/STUDIO_CHARTER.md](docs/STUDIO_CHARTER.md) |
| How the machine runs | [docs/playbook/OPERATING_SYSTEM.md](docs/playbook/OPERATING_SYSTEM.md) |
| Open a new product bet | [docs/playbook/NEW_PRODUCT.md](docs/playbook/NEW_PRODUCT.md) |
| Decision format | [docs/playbook/DECISION_TEMPLATE.md](docs/playbook/DECISION_TEMPLATE.md) |
| Portfolio memory | [docs/knowledge-base/INDEX.md](docs/knowledge-base/INDEX.md) |

## Current active bet

| Field | Value |
|-------|--------|
| Codename | **Dock Rush** |
| Stage | 2 — Concept spec |
| Status | Pending founder gut-check of the unique shared-graph rule |
| Spec | [docs/knowledge-base/concepts/dock-rush-spec.md](docs/knowledge-base/concepts/dock-rush-spec.md) |
| Backup | Tilt Sort (see [ideation](docs/knowledge-base/concepts/2026-09-ideation.md)) |

If the unique rule fails founder review, do not protect Dock Rush. Run the backup through the same OS.

## When Unity is allowed

Unity is **not** in this repository yet. The founder has not installed an editor.

When a Unity LTS is installed:

1. Create the project **inside the Editor** (File → New Project). Do not hand-author `ProjectSettings`.
2. Add GameFactoryCore modules only in the sprint they are first used.
3. Follow [docs/knowledge-base/tech/GAMEFACTORY_CORE_ROADMAP.md](docs/knowledge-base/tech/GAMEFACTORY_CORE_ROADMAP.md).
4. Start Stage 3 only after Stage 2 is approved.

Default platform is Android. iOS waits for traction. No backend by default.

## Default decision format

Substantial studio decisions end with:

**DECISION** · **WHY** · **BUILD** · **MEASURE** · **KILL CONDITION** · **NEXT ACTION**

See [docs/playbook/DECISION_TEMPLATE.md](docs/playbook/DECISION_TEMPLATE.md).

## Language

Committed docs and future code are English. Founder conversation may be Polish.
