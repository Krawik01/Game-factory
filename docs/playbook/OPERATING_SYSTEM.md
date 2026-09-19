# Operating System

How GAME FACTORY runs. Inspiration lives in the [charter](../STUDIO_CHARTER.md). This file is the rulebook.

## Stack

| Topic | Rule |
|-------|------|
| Platform | Android first |
| Engine | Unity + C# **when coding** |
| iOS | Only after traction justifies the extra platform work |
| Backend | None by default. No accounts, no obligatory servers, no LiveOps stack |
| Ads in development | Official **test-ad** configurations only |
| Production ads | Never instruct anyone to click production ads or generate artificial impressions |
| Repo language | English for committed docs and code |

## Hard product filters

A concept must be able to:

- Be understood in seconds
- Play with one hand
- Run in short sessions
- Deliver satisfying juice
- Scale to hundreds of levels or procedural content
- Offer natural rewarded-ad moments
- Read on TikTok / Reels / Shorts in under two seconds
- Ship with little or no backend
- Be built by one developer plus AI

**Clone-risk filter:** if the unique sentence still works after swapping art onto Car Jam, Color Sort, Pixel Flow, or a similar hit, reject the concept or add a **rule** those games cannot steal by reskinning.

## Business filter

Before implementing any feature, ask whether it materially improves:

- Retention
- Monetization
- Acquisition
- Production speed
- Learning

If the answer is no, challenge the feature.

Do not confuse polish with product validation.

## Kill philosophy

Most prototypes will fail. That is cheap market research.

Never continue because time was already spent.

When results are poor:

1. Name the bottleneck
2. Ask whether **one** high-value experiment could change the conclusion
3. Run it if justified
4. Otherwise **KILL** and archive useful technology

Stage 10 must end with exactly one of: **KILL** / **ITERATE** / **CONTINUE** / **SCALE**.

## Portfolio memory

Before a new game: read [knowledge-base/INDEX.md](../knowledge-base/INDEX.md) and the last postmortems.

After KILL or SCALE: write a [POSTMORTEM](../templates/POSTMORTEM.md) and update the INDEX.

## Stage gates 0–10

| Stage | Entry | Deliverable | Exit question | Allowed | Forbidden |
|-------|-------|-------------|----------------|---------|-----------|
| **0 Market** | Intent to find a lane | `knowledge-base/market/` from [STAGE0_MARKET](../templates/STAGE0_MARKET.md) | Is there a studio-fit lane with a distinct rule, not just a large hit? | Signals, clone-risk, studio-fit, thesis | Picking the biggest chart game as the plan |
| **1 Ideation** | Stage 0 thesis | `knowledge-base/concepts/` from [STAGE1_IDEATION](../templates/STAGE1_IDEATION.md) | Which concept is the strongest bet? | ≥3 distinct scored concepts | Recommending a pack of equals |
| **2 Concept** | Winner + backup named | Spec from [STAGE2_CONCEPT](../templates/STAGE2_CONCEPT.md) | Is v1 small enough and is the unique rule real? | Loop, win/lose, controls, kill question | Large feature lists, meta, store, LiveOps |
| **3 Prototype** | Founder approves spec **and** Unity LTS exists | Playable loop + [PLAYTEST_LOG](../templates/PLAYTEST_LOG.md) | **Is this mechanic worth continuing?** | Core gameplay, juice, win/lose, restart, rough levels | Store, settings, polished menus, hundreds of levels, economy, production ads, unused Core managers |
| **4 Vertical slice** | Prototype CONTINUE | Slice checklist from [STAGE4_VERTICAL_SLICE](../templates/STAGE4_VERTICAL_SLICE.md) | Is the product shippable enough to learn retention? | UI, tutorial, progression, save, analytics, audio, haptics, content pipeline, basic monetization, balance, polish | New core mechanic, iOS, LiveOps |
| **5 Monetization** | Slice loop is stable | [STAGE5_MONETIZATION](../templates/STAGE5_MONETIZATION.md) | Do ads/IAP support the loop instead of breaking it? | Test ads; rewarded value; interstitials at natural breaks | Forced ads mid-action; fake impressions; production clicks |
| **6 Analytics** | Events needed to reconstruct the funnel | [STAGE6_ANALYTICS](../templates/STAGE6_ANALYTICS.md) + [ANALYTICS_SCHEMA](../templates/ANALYTICS_SCHEMA.md) | Can we rebuild first_open → level → ad → IAP without PII? | Charter event set + level properties | Unnecessary personal data |
| **7 QA** | Release candidate | [STAGE7_QA](../templates/STAGE7_QA.md) + [QA_CHECKLIST](QA_CHECKLIST.md) | Would we ship this build to strangers? | Device, consent, ads-fail, aspect, save, offline | “It compiles” as SDK proof |
| **8 Store** | QA sign-off | [STAGE8_ASO](../templates/STAGE8_ASO.md) | Do icon + first screenshot communicate the core experience? | Names, descriptions, icon, screenshots, privacy, classification | Misleading creatives that the game cannot deliver |
| **9 Creative** | Playable footage exists | [STAGE9_CREATIVE](../templates/STAGE9_CREATIVE.md) | Do we have gameplay-first hooks we can vary? | Hook, setup, challenge, payoff, caption, CTA, variations | Constantly inventing unrelated ads instead of multiplying winners |
| **10 Decision** | Data or honest playtest notes | [STAGE10_DECISION](../templates/STAGE10_DECISION.md) | KILL, ITERATE, CONTINUE, or SCALE? | One recommendation; name missing metrics | “It depends” when evidence supports a call |

## Flywheel

```text
RESEARCH → IDEA → PROTOTYPE → TEST → DATA → DECISION → REUSABLE KNOWLEDGE → BETTER NEXT GAME
```

Open the next bet with [NEW_PRODUCT.md](NEW_PRODUCT.md).
