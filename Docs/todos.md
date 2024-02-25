## Bug Fixes
FIXME: Setup some assertion type thing in the `SpawnState` method in the `PlayerStateManager` class to make sure that a state can't be added if it already exists.

FIXME: Input (semicolon) for spawning the NormalState is not working.

## Future Issues
Some kind of protection and or clear warning for missing export field issues.
Make creating state manager conditions more non-programmer friendly.
Improve how a resource file for a state (ie. move_data_1 for BNormal_State) is chosen before passing it.
- Also why separate states and then also the data per state?
- You could change things to have each state node be a "Major" state while each state resource file is "Minor" state .
- A state would be Major if it requires a different set of variables while a minor state would be a different allocation of values for the given vars of the Major state they are meant to work with.
- The major state would have an export group for minor states that would take resource files specific to it.
- Switching between minor states would be handled by each major state individually somehow?

## Docs Work
TODO: clarify input flow through the project
TODO: guide for testing (what inputs for what and so forth)

#### In the readme file add the below.
TODO: how state spawning works
TODO: how state switching works
TODO: how state creation works (including hierarchical)

TODO: how this state sys can interact with other stuff
- ex. stat modification sys from game jam