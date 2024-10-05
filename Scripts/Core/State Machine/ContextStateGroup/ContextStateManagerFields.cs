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

        public ContextState currentState = ContextState.StateMenu;
       
    }
}