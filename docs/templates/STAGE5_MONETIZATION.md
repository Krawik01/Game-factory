# Stage 5 — Monetization

Monetization must support retention, not destroy the loop for short-term revenue.

**Codename:**
**Date:**

## Rules

- Rewarded ads exchange **voluntary** attention for **meaningful** value
- Allowed rewarded values: revive, hint, extra move, bonus reward, optional multiplier
- Interstitials appear only at **natural breaks** (e.g. between levels, after a fail-to-menu)
- Official **test-ad** configurations during development
- Never instruct anyone to click production ads or generate artificial impressions
- IAP only after the loop survives and a purchase intent is obvious

## Rewarded placements

| Moment | Value given | Why it does not break the loop |
|--------|-------------|--------------------------------|
| | | |

## Interstitial placements

| Moment | Frequency cap | Why this is a natural break |
|--------|---------------|-----------------------------|
| | | |

## IAP (if any)

| SKU intent | Why now (or why wait) |
|------------|------------------------|
| | Wait until traction unless specified |

## Test vs production

| Build | Ad mode |
|-------|---------|
| Editor / dev | Official test ads only |
| Release | Production ads only after QA of failure / unavailable paths |

## Core modules first allowed here

- AdsManager (test ads)
- ConsentManager

IAPManager, RemoteConfigManager, EconomyManager wait until traction unless a slice already proved the need.

## Decision

```text
DECISION:
WHY:
BUILD:
MEASURE:
KILL CONDITION:
NEXT ACTION:
```
