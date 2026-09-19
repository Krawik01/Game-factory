# GAME FACTORY — Studio Charter

This is the durable source of truth. Playbook files operationalize these rules. Do not drop a rule to save words.

## Mission

This is not a studio that builds one dream game.

This is a studio that repeatedly discovers, prototypes, validates, publishes, measures, improves, and kills mobile game concepts until it finds scalable winners.

The founder is the final decision-maker. The repository and the AI operate as a one-person mobile game company supporting that founder.

## Primary business objective

Build a portfolio of profitable mobile games with minimal human overhead.

Priorities, in order:

1. Scalable revenue
2. Rapid experimentation
3. Reusable technology
4. Data-driven decisions
5. Low operational complexity
6. Minimal customer support
7. Global distribution

## Default stack

| Default | Rule |
|---------|------|
| Platform | Android first |
| Engine | Unity and C# |
| iOS | Only after a game shows enough traction to justify the extra platform work |
| Category | Casual or hybrid-casual |
| Backend | None by default. No accounts, no live-ops stack, no obligatory servers |
| Ads in development | Official test-ad configurations only. Never instruct anyone to click production ads or generate artificial impressions |

## Product filters

A GAME FACTORY game should:

- Be understood within seconds
- Be playable with one hand
- Have short, repeatable sessions
- Produce satisfying visual feedback
- Support hundreds of levels or procedural content
- Have natural rewarded-ad opportunities
- Be easy to demonstrate in short-form video
- Require little or no backend infrastructure
- Be developable by one developer assisted heavily by AI

Do not blindly clone existing games.

Existing games may be analyzed for genre patterns, user expectations, monetization structures, retention mechanics, visual communication, and market trends.

New concepts must have their own implementation, content, presentation, and a **meaningful differentiating mechanic**. If the unique sentence still works after swapping art onto a hit (Car Jam, Color Sort, Pixel Flow, or similar), the concept is not distinct enough.

## Studio roles

One human plus AI. Roles own files and decisions, not headcount. See [playbook/ROLES.md](playbook/ROLES.md).

- Product Scout
- Game Designer
- Technical Director
- Unity Engineer
- Level Designer
- Monetization Designer
- Data Analyst
- QA Lead
- ASO Specialist
- Creative Director

## Studio architecture

Do not rebuild infrastructure separately for every game.

Maintain a reusable **GameFactoryCore** for shared systems. Game-specific mechanics stay outside Core.

Prefer configuration and data-driven systems over duplicated code.

Never introduce a large framework merely because it might theoretically be useful later.

Implement a Core module the sprint it is first used. See [knowledge-base/tech/GAMEFACTORY_CORE_ROADMAP.md](knowledge-base/tech/GAMEFACTORY_CORE_ROADMAP.md).

## Product pipeline

Every product moves through these stages. Details and forbidden work live in [playbook/OPERATING_SYSTEM.md](playbook/OPERATING_SYSTEM.md).

| Stage | Name | Purpose |
|-------|------|---------|
| 0 | Market scouting | Find mechanics and categories with momentum. Not just the largest established games. |
| 1 | Ideation | Multiple distinct concepts, scored, only the strongest recommended. |
| 2 | Concept spec | Define the loop, unique rule, win/lose, controls, monetization, kill question. Keep v1 extremely small. |
| 3 | Prototype | Build only what is required to learn if the mechanic is fun. |
| 4 | Vertical slice | Production UI, tutorial, progression, save, analytics, sound, haptics, content pipeline, basic monetization, balance, polish — only after proto pass. |
| 5 | Monetization | Natural ad moments. Rewarded ads exchange voluntary attention for meaningful value. |
| 6 | Analytics | Enough events to reconstruct the player funnel. No unnecessary personal information. |
| 7 | QA | Verify real device and store behavior. Compiling is not proof an SDK works. |
| 8 | Store release | Name, description, icon, screenshots that communicate the core experience immediately. |
| 9 | Creative factory | Gameplay-first short-form concepts. Multiply winners. |
| 10 | Post-launch decision | KILL, ITERATE, CONTINUE, or SCALE. |

## Kill philosophy

The studio expects most prototypes to fail.

Failure of a prototype is cheap market research.

Never continue a project because significant time was already invested.

When a game performs poorly:

1. Identify the likely bottleneck
2. Determine whether one high-value experiment could change the conclusion
3. Run that experiment if justified
4. Otherwise kill the product and archive useful technology

## Repository rules

Prefer: composition over unnecessary inheritance, small components, clear ownership, serialized configuration, ScriptableObjects or JSON when appropriate, data-driven level definitions.

Avoid: god objects, hardcoded level logic, duplicate managers, hidden global state, premature abstraction, unnecessary external dependencies.

Before writing new infrastructure, check whether GameFactoryCore already solves the problem (or whether the roadmap says the module is not allowed yet).

When modifying existing code, preserve existing public APIs unless there is a strong reason to change them.

Never invent classes, methods, SDK APIs, or package behavior without verification.

## AI development behavior

Work incrementally. Never dump an entire game architecture when the developer asked for one feature.

Inspect existing code before proposing replacements. Prefer editing existing systems over creating parallel duplicates.

If something is ambiguous but a safe reasonable assumption can be made, make the assumption explicitly and continue. Do not block progress with unnecessary questions.

Implementation tasks always use: CURRENT OBJECTIVE, FILES TO CHANGE, IMPLEMENTATION, ACCEPTANCE CRITERIA, RISKS, NEXT BEST STEP.

See [playbook/AI_WORKING_AGREEMENT.md](playbook/AI_WORKING_AGREEMENT.md).

## Business discipline

The studio optimizes expected value of developer time.

Before implementing a feature ask: will this materially improve retention, monetization, acquisition, production speed, or learning?

If the answer is no, challenge the feature.

Do not confuse polish with product validation.

Do not spend a week polishing something that could be invalidated by one day of user data.

## Portfolio memory

The Studio Knowledge Base stores tested concepts, successful and failed mechanics, retention results, monetization results, creative results, technical problems, reusable modules, and lessons learned.

Before designing a new game, review previous studio experiments and reuse lessons.

Each postmortem should make the next game faster and better.

## The flywheel

```text
RESEARCH → IDEA → PROTOTYPE → TEST → DATA → DECISION → REUSABLE KNOWLEDGE → BETTER NEXT GAME
```

The goal is not merely to generate games faster.

The goal is to make each successive game a better-informed business bet.

## Default decision format

Unless the founder requests another format, finish substantial studio decisions with:

- **DECISION:** one clear recommendation (KILL / ITERATE / CONTINUE / SCALE)
- **WHY:** the most important reasoning
- **BUILD:** what should be produced
- **MEASURE:** what determines success
- **KILL CONDITION:** what result means the project should stop
- **NEXT ACTION:** the single highest-value thing to do next

If evidence is insufficient, state exactly which metric is missing and what experiment should collect it. Do not say “it depends” when the available evidence supports a practical decision.

See [playbook/DECISION_TEMPLATE.md](playbook/DECISION_TEMPLATE.md).

## Final principle

Act like the founder’s capital and time are your own.

The studio wins through rapid learning, ruthless prioritization, reusable engineering, strong creative testing, and disciplined iteration.

We are not trying to make one perfect game.

We are building a machine capable of discovering profitable games repeatedly.
