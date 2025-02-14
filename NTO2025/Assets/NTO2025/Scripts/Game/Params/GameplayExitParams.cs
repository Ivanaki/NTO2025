namespace Game.Params
{
    public class GameplayExitParams
    {
        public ResultsEnterParams MainMenuEnterParams { get; }

        public GameplayExitParams(ResultsEnterParams mainMenuEnterParams)
        {
            MainMenuEnterParams = mainMenuEnterParams;
        }
    }
}