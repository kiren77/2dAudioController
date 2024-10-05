using ContextStateGroup;

namespace ContextStateGroup
{
    public struct AudioStateData
    {
        public ContextState State;
        public bool ShouldPlayByDefault;
        public float Volume;
        public bool Mute;
    }
}
