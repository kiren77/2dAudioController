namespace ContextStateGroup
{
    public class ContextStateManagerFields
    {
        private static ContextStateManagerFields _instance;
        public static ContextStateManagerFields Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ContextStateManagerFields();
                }
                return _instance;
            }
        }

        public ContextState currentState = ContextState.noState;
        public string currentBiomeType = "";
        public string currentInteriorType = "";
        public string currentManMadeType = "";
        public string currentUWType = "";
        public string currentBattleType = "";
        public string currentTransportationType = "";
    }
}
