# QA checklist

Use before any store build. Compiling is **not** proof that a third-party SDK works. Verify current official documentation for ads, consent, and store requirements.

Copy to the game’s Stage 7 sign-off (see [../templates/STAGE7_QA.md](../templates/STAGE7_QA.md)).

## Install and save

- [ ] Fresh install starts without crash
- [ ] Upgrade from previous build keeps or migrates save as designed
- [ ] Save persistence survives kill-from-recents
- [ ] Offline behavior: game plays; failed network calls do not crash

## Devices and layout

- [ ] Tall phone aspect (e.g. 20:9)
- [ ] Wide / tablet-ish aspect
- [ ] Notch / cutout does not hide controls
- [ ] Low-end device: stable frame rate on the core loop, no thermal surprise in a short session

## Lifecycle

- [ ] Pause / resume
- [ ] Background / foreground
- [ ] Rapid app switch during a level

## Gameplay edges

- [ ] Rapid tapping does not break input or double-spend a move
- [ ] Level restart is clean
- [ ] Level skip / debug cheats are **disabled** in the release build
- [ ] Win and lose both return to a stable flow

## Ads and money (when Stage 5+ is wired)

- [ ] Official **test** ads only in development builds
- [ ] Ad failure: UI recovers, no soft-lock
- [ ] Ad unavailable: offer is skipped or deferred, no crash
- [ ] Rewarded: grant only after a completed rewarded view (per official SDK callbacks)
- [ ] Interstitial only at a natural break

## Privacy

- [ ] Consent / privacy flow appears where required
- [ ] No collection of unnecessary personal information
- [ ] Store privacy disclosures match actual behavior

## Analytics sanity

- [ ] `first_open` / `session_start` fire once per expected moment
- [ ] Level events include `level_number`, `attempt_number`, `duration`, `failure_reason` where relevant

## Crash-free startup

- [ ] Cold start on a clean device
- [ ] Cold start with existing save
- [ ] First scene has no missing-reference exceptions

## Sign-off

| Field | Value |
|-------|--------|
| Build | |
| Devices tested | |
| Signed by | |
| Date | |
| Ship? | YES / NO |
