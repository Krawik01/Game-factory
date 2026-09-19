# Engineering rules

Apply when code exists. This PR has no Unity project. Do not invent one.

## Prefer

- Composition over unnecessary inheritance
- Small components with clear ownership
- Serialized configuration
- ScriptableObjects or JSON for level definitions
- Data-driven content over hardcoded level logic

## Avoid

- God objects
- Duplicate managers
- Hidden global state
- Premature abstraction
- Unnecessary external dependencies
- Frameworks added “for later”

## GameFactoryCore

- Before writing infrastructure, read [../knowledge-base/tech/GAMEFACTORY_CORE_ROADMAP.md](../knowledge-base/tech/GAMEFACTORY_CORE_ROADMAP.md)
- Implement a module **the sprint it is first used**
- Game mechanics stay in `Assets/Games/<Codename>/`
- Common UI enters Core only when a **second** game needs the same control
- One `GameFactoryCore.asmdef`
- Explicit composition root — no hidden service locators unless a single documented exception is required for prototype speed

## APIs and SDKs

- Preserve existing public APIs unless there is a strong reason to change them
- Never invent SDK classes, methods, or package behavior
- Verify current official vendor documentation (ads, consent, IAP, analytics) at integration time
- Official **test-ad** configurations during development
- Compiling is not proof that an SDK works

## Unity project creation

When the founder has a Unity LTS installed:

1. Create the project **in the Editor** (File → New Project)
2. Do not hand-author `ProjectSettings` in git beforehand
3. Android-first player settings configured in the Editor
4. Then add folders and the first allowed Core modules for Stage 3

## Business filter still applies

If a class does not improve retention, monetization, acquisition, production speed, or learning, do not write it.
