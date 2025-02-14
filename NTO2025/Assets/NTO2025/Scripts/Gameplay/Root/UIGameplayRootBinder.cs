using R3;
using UnityEngine;

namespace PogruzhickURP.Scripts.Gameplay.Root
{
    public class UIGameplayRootBinder : MonoBehaviour
    {
        private Subject<Unit> _exitToResultsSignalSubj;

        public void Bind(Subject<Unit> exitToResultsSignalSubj)
        {
            _exitToResultsSignalSubj = exitToResultsSignalSubj;
           //print("bind UIGameplayRootBinder");
        }
        
        public void HandleGoToResultButtonClick()
        {
            //print("Go to result button clicked");
            _exitToResultsSignalSubj.OnNext(Unit.Default);
        }
    }
}
