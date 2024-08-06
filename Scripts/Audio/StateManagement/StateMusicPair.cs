/* 
    never delete this comment section
    Timestamp: 2024-08-01

    - Assumptions about original intent:
        - Define a pair for context state and corresponding music emitter.

    - What the script actually achieves currently:
        - Holds data for state to music emitter mapping.

    - Problems:
        - None identified.

    - Suggestions:
        - None.

    - Other notes:
        - Ensure proper integration with other state management scripts.
*/

using ContextStateGroup;
using FMODUnity;

[System.Serializable]
public class StateMusicPair
{
    public ContextState state;
    public StudioEventEmitter musicEmitter;
}
