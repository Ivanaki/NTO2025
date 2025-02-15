using BaCon;
using Electrical.Scripts._3MainMenu.Root;
using Game.Params;
using R3;
using UnityEngine;

namespace MainMenu.Root
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIMainMenuBinder _binder;
        private bool _isExam = false;
        
        public Observable<MainMenuExitParams> Run(DIContainer gameplayContainer)
        {
            
            //bind UI
            var exitToResultsSignalSubj = new Subject<Unit>();
            _binder.Bind(exitToResultsSignalSubj);
            
            Debug.Log($"MainMenu ENTRY POINT: save file name = , level to load = ");
            
            
            var exit = exitToResultsSignalSubj.Select(isExam => new MainMenuExitParams(new GameplayEnterParams(_isExam)));
            
            return exit;
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}