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
| Codename | **Dock Rush** (Candidate #001, **hypothesis**) |
| Stage | **3 — Prototype prep** (no Unity in repo yet) |
| Stage 2 spec | [docs/knowledge-base/concepts/dock-rush-spec.md](docs/knowledge-base/concepts/dock-rush-spec.md) |
| Stage 3 pack | [design/dock-rush/PROTO.md](design/dock-rush/PROTO.md) + [levels](design/dock-rush/levels/) |
| Backup | Tilt Sort (see [ideation](docs/knowledge-base/concepts/2026-09-ideation.md)) |

Gameplay code starts only after you create a Unity project in the Editor. See **Unity setup** in `design/dock-rush/PROTO.md`.

## When Unity is allowed

There is still **no** Unity project in this repository. Stage 3 Dock Rush uses **no GameFactoryCore** — only `Assets/Games/DockRush/` after you create the project.

When Unity LTS is installed: follow [design/dock-rush/PROTO.md](design/dock-rush/PROTO.md) → **Unity setup (founder)**. Do not hand-author `ProjectSettings` before opening the project once in the Editor.

Default platform is Android. iOS waits for traction. No backend by default.

## Default decision format

Substantial studio decisions end with:

**DECISION** · **WHY** · **BUILD** · **MEASURE** · **KILL CONDITION** · **NEXT ACTION**

See [docs/playbook/DECISION_TEMPLATE.md](docs/playbook/DECISION_TEMPLATE.md).

## Language

Committed docs and future code are English. Founder conversation may be Polish.
