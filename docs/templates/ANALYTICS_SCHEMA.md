# Analytics schema (studio contract)

This markdown contract comes **before** C# constants. When code exists, names must match exactly.

Do not collect unnecessary personal information.

## Events

| Event | When | Required properties |
|-------|------|---------------------|
| `first_open` | First successful launch of this install | `app_version` |
| `session_start` | Each cold or warm session start | `session_id`, `app_version` |
| `tutorial_start` | Tutorial begins | `tutorial_id` |
| `tutorial_complete` | Tutorial finished | `tutorial_id`, `duration` |
| `level_start` | A level attempt begins | `level_number`, `attempt_number` |
| `level_complete` | Win | `level_number`, `attempt_number`, `duration` |
| `level_fail` | Lose | `level_number`, `attempt_number`, `duration`, `failure_reason` |
| `level_restart` | Player restarts the current attempt | `level_number`, `attempt_number` |
| `rewarded_offer` | Rewarded placement shown as a choice | `placement` |
| `rewarded_accept` | Player accepts | `placement` |
| `rewarded_complete` | Reward granted after a successful official callback | `placement` |
| `interstitial_shown` | Interstitial displayed | `placement` |
| `iap_view` | Purchase UI opened | `sku` |
| `iap_purchase` | Purchase confirmed by official store callback | `sku` |

## Level property notes

| Property | Meaning |
|----------|---------|
| `level_number` | 1-based content index |
| `attempt_number` | Attempts on this level in this install (or session — pick one and keep it) |
| `duration` | Seconds from `level_start` to outcome |
| `failure_reason` | Stable enum string, e.g. `deadlock`, `backlog`, `quit` — not free text from the player |

## Sinks

| Stage | Sink |
|-------|------|
| Stage 3 | Editor / file log only |
| Stage 4+ | Add a third-party SDK only after reading **current** vendor docs |

## Forbidden

- Email, real name, phone
- Precise GPS
- Clipboard or contact access
- Anything not required to reconstruct the funnel or run a named experiment
