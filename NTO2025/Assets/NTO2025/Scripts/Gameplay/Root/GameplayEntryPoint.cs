using BaCon;
using Game.Params;
using MyUtils;
//using Naf;
using PogruzhickURP.Scripts.Gameplay.Root;
using R3;
//using Ski.Scripts.Gameplay;
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
            _playerCamera = player.hmdTransforms[0].GetComponent<Camera>();
            
            
            var exitToResultsSignalSubj = new Subject<Unit>();
            _binder.Bind(exitToResultsSignalSubj);
            
            Debug.Log($"GAMEPLAY ENTRY POINT: vr is {enterParams.IsExam}");
            
            return exitToResultsSignalSubj;
        }
    }
}