using BaCon;
using Game.Params;
using PogruzhickURP.Scripts.Gameplay.Root;
using R3;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Gameplay.Root
{
    public class GameplayEntryPoint : MonoBehaviour
    {
        [SerializeField] private UIGameplayRootBinder _binder;
        
        private Camera _playerCamera;
        
        public Observable<Unit> Run(DIContainer gameplayContainer, GameplayEnterParams enterParams)
        {
            var player = FindObjectOfType<Player>();
            if (player != null)
            {
                _playerCamera = player.hmdTransforms[0].GetComponent<Camera>();
            }
            else
            {
                new EventSystemCreator();
            }
            
            var exitToResultsSignalSubj = new Subject<Unit>();
            _binder.Bind(exitToResultsSignalSubj);
            
            Debug.Log($"GAMEPLAY ENTRY POINT: vr is {enterParams.IsExam}");
            
            return exitToResultsSignalSubj;
        }
    }
}