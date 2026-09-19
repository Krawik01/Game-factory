# Decision template

Use this footer on every substantial studio decision (stage exits, kill/continue, infra that might become a framework).

Copy and fill:

```text
DECISION:
WHY:
BUILD:
MEASURE:
KILL CONDITION:
NEXT ACTION:
```

## Field rules

| Field | Rule |
|-------|------|
| **DECISION** | Exactly one of **KILL**, **ITERATE**, **CONTINUE**, **SCALE**. One word. No hedges. |
| **WHY** | The single most important reason, not a list of maybes. |
| **BUILD** | What will actually be produced next. |
| **MEASURE** | The metric or observation that decides success. |
| **KILL CONDITION** | The result that means stop. Write it before building. |
| **NEXT ACTION** | One highest-value next step. Not a backlog. |

## Missing evidence

If the data cannot support a call:

1. Do **not** write “it depends”
2. Name the **exact metric** that is missing
3. Name the **experiment** that will collect it
4. Decision may be **ITERATE** (run that experiment) — still one word

## Allowed decision words

| Word | Means |
|------|--------|
| KILL | Stop the product. Archive useful tech. Write a postmortem. |
| ITERATE | One (or a tightly scoped set of) high-value experiment(s). Then decide again. |
| CONTINUE | Advance to the next pipeline stage. |
| SCALE | Spend more on UA / content / platform (e.g. iOS) because evidence supports it. |
