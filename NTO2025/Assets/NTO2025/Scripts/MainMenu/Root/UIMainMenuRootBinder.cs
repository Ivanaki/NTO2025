using R3;
using UnityEngine;

namespace Electrical.Scripts._3MainMenu.Root
{
    public class UIMainMenuBinder : MonoBehaviour
    {
        private Subject<Unit> _exitToResultsSignalSubj;

        public void Bind(Subject<Unit> exitToResultsSignalSubj)
        {
            _exitToResultsSignalSubj = exitToResultsSignalSubj;
        }
        
        public void HandleGoToGameplayButtonClick()
        {
            _exitToResultsSignalSubj?.OnNext(Unit.Default);
        }
        
    }
}
