namespace Game.Params
{
    public class GameplayEnterParams
    {
        public bool IsExam {get;} 

        public GameplayEnterParams(bool isExam)
        {
            IsExam = isExam;
        }
    }
}