# Stage 6 — Analytics

Enough events to reconstruct the player funnel. No unnecessary personal information.

**Codename:**
**Date:**
**Schema:** [ANALYTICS_SCHEMA.md](ANALYTICS_SCHEMA.md) (same contract)

## Implementation checklist

- [ ] Event names match the schema exactly
- [ ] Level events include `level_number`, `attempt_number`, `duration`, `failure_reason` where relevant
- [ ] Editor sink works before any third-party SDK
- [ ] No PII (name, email, precise location, advertising id beyond what a later official SDK requires and consent allows)

## Funnel we must reconstruct

```text
first_open → session_start → tutorial_start → tutorial_complete
  → level_start → (level_complete | level_fail | level_restart)
  → rewarded_offer → rewarded_accept → rewarded_complete
  → interstitial_shown
  → iap_view → iap_purchase
```

## Game-specific extra events (optional)

| Name | Why it is required | Properties |
|------|--------------------|------------|
| | Must improve a decision | |

If an extra event does not change a decision, do not add it.

## Decision

```text
DECISION:
WHY:
BUILD:
MEASURE:
KILL CONDITION:
NEXT ACTION:
```
